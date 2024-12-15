using System.Collections.ObjectModel;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites;

namespace RecipesEverywhere.ViewModel
{
    public class UserRecipesViewModel : ViewModelBase
    {
        private int _userId = UserService.Instance.User.Id;
        
        private RecipeService _recipeService;
        public ObservableCollection<RecipeViewModel> Recipes { get; set; } = new();
        public UserRecipesViewModel()
        {
            _recipeService = RecipeService.Instance;
            var recipes = _recipeService.LoadPostsFromUser(_userId);
            foreach (var recipe in recipes)
            {
                var recipeVM = new RecipeViewModel(recipe);

                Recipes.Add(recipeVM);
            }
        }
    }
}