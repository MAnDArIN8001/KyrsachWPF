using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using RecepiesEverywhere;
using RecipesEverywhere.Services;
using RecepiesEverywhere.Annotations;

using Authorization = System.Net.Authorization;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Utilites.Commands;
using RecipesEverywhere.View;

namespace RecipesEverywhere.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private NavigationService _navigationService;
        private UserService _userService;

        private MainWindow _mainWindow;

        public bool IsAuthorized => _userService.IsAuthorized;
        public bool IsUnauthorized => !_userService.IsAuthorized;

        public ICommand ChangeCurrentPageCommand { get; private set; }

        public UserControl CurrentPage => _navigationService.CurrentPage;

        public User CurrentUser => _userService.User;
        
        public MainPageViewModel()
        {
            ChangeCurrentPageCommand = new RelayCommand(ChangeCurrentPage);

            _navigationService = NavigationService.Instance;
            _userService = UserService.Instance;
            
            _navigationService.PropertyChanged += OnPageChanged;
            _userService.PropertyChanged += OnUserChanged;

            if (CurrentUser is null)
            {
                _navigationService.TryChangePage(nameof(Authorization));
            }
        }
        
        private void ChangeCurrentPage(object param)
        {
            if (param is string)
            {
                _navigationService.TryChangePage(param as string);
            }
        }
        
        private void OnPageChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_navigationService.CurrentPage))
            {
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private void OnUserChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_userService.User))
            {
                _navigationService.TryChangePage(nameof(Home));
                
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsAuthorized));
                OnPropertyChanged(nameof(IsUnauthorized));
            }
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}