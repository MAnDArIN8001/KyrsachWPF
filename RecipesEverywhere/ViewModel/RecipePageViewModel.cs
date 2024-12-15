using Microsoft.EntityFrameworkCore.Update.Internal;
using RecepiesEverywhere.Models;
using RecepiesEverywhere.View;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites.Commands;
using System.Windows.Input;

namespace RecipesEverywhere.ViewModel
{
    public class RecipePageViewModel : Utilites.ViewModelBase
    {
        private readonly Recipe _recipeModelModel;
        private NavigationService _navigationService;

        public Recipe RecipeModel => _recipeModelModel;
        public ICommand NavigateCommand { get; private set; }

        public RecipePageViewModel(Recipe recipeModelModel)
        {
            _recipeModelModel = recipeModelModel;
            _navigationService = NavigationService.Instance;
            NavigateCommand = new RelayCommand(Navigate);
        }

        private void Navigate(object sender)
        {
            _navigationService.TryChangePage(nameof(UpdateRecipe), RecipeModel);
        }
    }
}