using RecepiesEverywhere.Models;
using RecepiesEverywhere.ViewModel;
using System.Windows.Controls;

namespace RecepiesEverywhere.View
{
    public partial class UpdateRecipe : UserControl
    {
        public UpdateRecipe(Recipe recipe)
        {
            InitializeComponent();
            DataContext = new UpdateRecipeViewModel(recipe);
        }
    }
}