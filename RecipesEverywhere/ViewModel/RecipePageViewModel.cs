using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using System.Windows.Input;

namespace RecipesEverywhere.ViewModel
{
    public class RecipePageViewModel : Utilites.ViewModelBase
    {
        private readonly Recipe _recipeModelModel;
        private NavigationService _navigationService;

        public Recipe RecipeModel => _recipeModelModel;
        public ICommand SearchCommand { get; private set; }

        public RecipePageViewModel(Recipe recipeModelModel)
        {
            _recipeModelModel = recipeModelModel;
            _recipeModelModel.Text += _recipeModelModel.Picture;
            _navigationService = NavigationService.Instance;

        }
    }
}