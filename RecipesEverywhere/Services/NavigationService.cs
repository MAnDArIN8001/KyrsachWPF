

using RecepiesEverywhere.Annotations;
using RecepiesEverywhere.Models;
using RecepiesEverywhere.View;
using RecipesEverywhere.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

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

        private readonly Dictionary<string, Func<object?, UserControl>> _pageFactories;

        private NavigationService()
        {
            _pageFactories = new Dictionary<string, Func<object?, UserControl>>
            {
                { nameof(Registration), _ => new Registration() },
                { nameof(Authorization), _ => new Authorization() },
                { nameof(Home), _ => new Home() },
                { nameof(UserView), _ => new UserView() },
                { nameof(Search), _ => new Search() },
                { nameof(CreateRecipe), _ => new CreateRecipe() },
                { nameof(RecipePage), CreateRecipePage },
                { nameof(UpdateRecipe), (param) => new UpdateRecipe((Recipe)param) },
                { nameof(UserRecipes), _ => new UserRecipes() },
            };
        }

        public UserControl TryChangePage(string pageName, object? pageParams = null)
        {
            if (_pageFactories.TryGetValue(pageName, out var factory))
            {
                CurrentPage = factory(pageParams);
                return CurrentPage;
            }

            throw new Exception("Invalid page");
        }

        private UserControl CreateRecipePage(object? pageParams)
        {
            if (pageParams is not Recipe recipe)
            {
                throw new Exception("To load a recipe page you have to implement a recipe");
            }

            return new RecipePage(recipe);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
