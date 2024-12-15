using System.Windows.Controls;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.ViewModel;

namespace RecepiesEverywhere.View
{
    public partial class RecipePage : UserControl
    {
        public RecipePageViewModel _recipeViewModel;
        
        public RecipePage(Recipe recipeModel)
        {
            InitializeComponent();

            _recipeViewModel = new RecipePageViewModel(recipeModel);

            DataContext = _recipeViewModel;
        }
    }
}