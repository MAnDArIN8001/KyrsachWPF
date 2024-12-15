using System.Windows.Controls;
using System.Windows.Input;
using RecepiesEverywhere.View;
using RecipesEverywhere.Model.DTO;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites;
using RecipesEverywhere.Utilites.Commands;

namespace RecipesEverywhere.ViewModel
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private string _name = "";
        private string _password = "";
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
                
                OnPropertyChanged(_password);
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
        
        public AuthorizationViewModel()
        {
            _userService = UserService.Instance;

            AuthorizationCommand = new RelayCommand(Authorize);
            RegistrationCommand = new RelayCommand(SwapOnRegistration);
        }

        private async void Authorize(object? param = null)
        {
            if (_name.Trim().Length == 0 || _password.Trim().Length == 0)
            {
                ExceptionMessage = "You have to enter all fields";
                
                return;
            }

            var autharizationDto = new UserAuthorizationDTO() { Name = _name, Password = _password };

            var response = await _userService.LogIn(autharizationDto);
            
            if (!response.Success)
            {
                ExceptionMessage = response.Message;
            }
        }

        private void SwapOnRegistration(object? param = null)
        {
            NavigationService.Instance.TryChangePage(nameof(Registration));
        }
    }
}