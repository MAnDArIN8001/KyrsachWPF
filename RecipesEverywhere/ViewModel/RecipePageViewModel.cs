using RecipesEverywhere.Model;

namespace RecipesEverywhere.ViewModel
{
    public class RecipePageViewModel : Utilites.ViewModelBase
    {
        private readonly RecipeModel _recipeModelModel;

        public RecipeModel RecipeModel => _recipeModelModel;

        public RecipePageViewModel(RecipeModel recipeModelModel)
        {
            _recipeModelModel = recipeModelModel;
        }
    }
}