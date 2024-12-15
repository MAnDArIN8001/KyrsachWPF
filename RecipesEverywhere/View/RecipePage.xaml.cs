using System.Windows.Controls;
using RecipesEverywhere.Model;
using RecipesEverywhere.ViewModel;

namespace RecepiesEverywhere.View
{
    public partial class RecipePage : UserControl
    {
        public RecipePageViewModel _recipeViewModel;
        
        public RecipePage(RecipeModel recipeModel)
        {
            InitializeComponent();

            _recipeViewModel = new RecipePageViewModel(recipeModel);

            DataContext = _recipeViewModel;
        }
    }
}