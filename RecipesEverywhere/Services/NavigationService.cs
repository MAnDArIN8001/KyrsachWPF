using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using RecepiesEverywhere.Annotations;
using RecepiesEverywhere.Models;
using RecepiesEverywhere.View;
using RecipesEverywhere.Model;
using RecipesEverywhere.View;

namespace RecipesEverywhere.Services
{
    public class NavigationService : INotifyPropertyChanged
    {
        #region Singleton

        private static NavigationService _instance;

        public static NavigationService Instance => _instance ??= new NavigationService();

        #endregion
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private UserControl _currentPage;

        public UserControl CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private NavigationService() {}

        public UserControl TryChangePage(string pageName, object? pageParams = null)
        {
            UserControl response = null;

            switch (pageName)
            {
                case nameof(Registration):
                    CurrentPage = new Registration();
                    break;
                
                case nameof(Authorization):
                    CurrentPage = new Authorization();
                    break;
                
                case nameof(Home):
                    CurrentPage = new Home();
                    break;
                
                case nameof(User):
                    CurrentPage = new UserView();
                    break;

                case nameof(Search):
                    CurrentPage = new Search();
                    break;
                case nameof(CreateRecipe):
                    CurrentPage = new CreateRecipe();
                    break;

                case nameof(RecipePage):
                    if (pageParams is not Recipe)
                    {
                        throw new Exception("To load a recipe page you have to implement a recipe");
                    }
                    
                    var recipePage = new RecipePage(pageParams as Recipe);
                    
                    CurrentPage = recipePage;
                    break;
                    
                default:
                    throw new Exception("Invalid page");
                    break;
            }

            return CurrentPage;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}