using System.Collections.ObjectModel;
using System.Windows.Input;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites.Commands;
using RecipesEverywhere.View;

namespace RecipesEverywhere.ViewModel
{
    public class HomeViewModel : Utilites.ViewModelBase
    {
        public ObservableCollection<RecipeViewModel> Recipes { get; set; } = new();

        private RecipeService _recipeService;
        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }


        public HomeViewModel()
        {
            _recipeService = RecipeService.Instance;
            SearchCommand = new RelayCommand(SearchRecipes);
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            var recipes = _recipeService.LoadAll();

            UpdateRecipesView(recipes);
        }

        private void UpdateRecipesView(List<Recipe>? recipes)
        {
            if (recipes is null)
            {
                return;
            }
            Recipes.Clear();
            foreach (var recipe in recipes)
            {
                var recipeVM = new RecipeViewModel(recipe);

                Recipes.Add(recipeVM);
            }
        }

        private void SearchRecipes(object sender) 
        {
            var recipes = _recipeService.Search(_query);
            UpdateRecipesView(recipes);
        }
    }
}