using System.Net;
using System.Windows.Input;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites.Commands;

namespace RecipesEverywhere.ViewModel
{
    public class UserViewModel : Utilites.ViewModelBase
    {
        private UserService _userService;

        public string Name => _userService.User.Name;

        public ICommand LogOutCommand { get; private set; }

        public UserViewModel()
        {
            _userService = UserService.Instance;

            LogOutCommand = new RelayCommand(LogOut);
        }

        private void LogOut(object? sender)
        {
            _userService.LogOut();

            NavigationService.Instance.TryChangePage(nameof(Authorization));
        }
    }
}