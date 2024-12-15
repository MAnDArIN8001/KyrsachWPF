using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;

namespace RecipesEverywhere.ViewModel
{
    public class HomeViewModel : Utilites.ViewModelBase
    {
        public ObservableCollection<RecipeViewModel> Recipes { get; set; } = new();

        private RecipeService _recipeService;
        
        public HomeViewModel()
        {
            _recipeService = RecipeService.Instance;

            LoadRecipes();
        }

        private async void LoadRecipes()
        {
            var recipes = _recipeService.LoadAllRecipes();

            if (recipes is null)
            {
                return;
            }

            foreach (var recipe in recipes)
            {
                var recipeVM = new RecipeViewModel(recipe);
                
                Recipes.Add(recipeVM);
            }
        }
    }
}