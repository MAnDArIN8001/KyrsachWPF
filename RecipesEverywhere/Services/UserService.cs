using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecepiesEverywhere.Annotations;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Model.DTO;
using RecipesEverywhere.Model.Enum;
using RecipesEverywhere.Utilites;
using RecipesEverywhere.View;

namespace RecipesEverywhere.Services
{
    public class UserService : INotifyPropertyChanged
    {
        #region Singleton

        private static UserService _instance;

        public static UserService Instance => _instance ??= new UserService();

        #endregion
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private User _user;
        public bool IsAuthorized => User.Name != null;
        public bool IsAdmin => User.RoleId == (int)RoleEnum.Admin;
        public User User
        {
            get => _user ??= new User();
            private set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private UserService() { }

        public async Task<Response<bool>> LogIn(UserAuthorizationDTO authorizationData)
        {
            Response<bool> response = new Response<bool>();
            
            try
            {
                using (var context = new RecipeDbContext())
                {
                    var userWithName = await context.Users.FirstOrDefaultAsync(user => user.Name == authorizationData.Name);

                    if (userWithName is null)
                    {
                        throw new Exception("Name or password is wrong");
                    }

                    if (HashPassword(authorizationData.Password) != userWithName.PasswordHash)
                    {
                        throw new Exception("Name or password is wrong");
                    }

                    User = userWithName!;

                    response.Success = true;
                }
            }
            catch (Exception exception)
            {
                response.Message = "Something was wrong";
                response.Success = false;
                response.Data = false;
                
                return response;
            }
            
            return response;
        }

        public async Task<Response<bool>> SignIn(UserAuthorizationDTO authorizationDto)
        {
            Response<bool> response = new Response<bool>();
            
            try
            {
                using (var context = new RecipeDbContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(temp => temp.Name == authorizationDto.Name);

                    if (user is not null)
                    {
                        response.Message = "There's already exist a user with the same name";
                        response.Data = false;
                        response.Success = false;

                        return response;
                    }

                    var newUser = new User()
                    {
                        Name = authorizationDto.Name,
                        PasswordHash = HashPassword(authorizationDto.Password)
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    response.Data = true;
                    response.Success = true;
                }
            }
            catch (Exception exception)
            {
                response.Message = "Something was wrong";
                response.Data = false;
                response.Success = false;
                
                return response;
            }

            return response;
        }

        public bool LogOut()
        {
            _user = null;
            
            OnPropertyChanged(nameof(User));
            
            return true;
        }
        
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}