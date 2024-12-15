using System.Net;
using System.Windows.Input;
using RecipesEverywhere.Model.DTO;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites;
using RecipesEverywhere.Utilites.Commands;

namespace RecipesEverywhere.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _name = "";
        private string _password = "";
        private string _confirmPassword = "";
        private string _exceptionMessage = "";

        private UserService _userService;

        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string ExceptionMessage
        {
            get => _exceptionMessage;
            set
            {
                _exceptionMessage = value;
                
                OnPropertyChanged(nameof(ExceptionMessage));
            }
        }
        
        public RegistrationViewModel()
        {
            _userService = UserService.Instance;

            AuthorizationCommand = new RelayCommand(SwapOnAuthorization);
            RegistrationCommand = new RelayCommand(RegisterNewUser);
        }

        private async void RegisterNewUser(object? sender)
        {
            if (_name.Trim().Length == 0 || _password.Trim().Length == 0 || _confirmPassword.Trim().Length == 0)
            {
                ExceptionMessage = "You have to enter all fields";
                
                return;
            }

            if (_password != _confirmPassword)
            {
                ExceptionMessage = "Password and confirm password must be same";

                return;
            }
            
            var authorizationDto = new UserAuthorizationDTO() { Name = _name, Password = _password };

            var response = await _userService.SignIn(authorizationDto);
            
            if (response.Success)
            {
                ExceptionMessage = response.Message;
            }
        }

        private void SwapOnAuthorization(object? sender)
        {
            NavigationService.Instance.TryChangePage(nameof(Authorization));
        }
    }
}