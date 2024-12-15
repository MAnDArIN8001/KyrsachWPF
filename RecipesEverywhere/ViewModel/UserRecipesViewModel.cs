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
        
        private NavigationService _navigationService;
        private RecipeService _recipeService;
        
        public ObservableCollection<Recipe> Recipes { get; private set; } = new();

        public UserRecipesViewModel()
        {
            _recipeService = RecipeService.Instance;
            _navigationService = NavigationService.Instance;
        }
    }
}