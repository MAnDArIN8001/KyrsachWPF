using RecipesEverywhere.ViewModel;
using System.Windows.Controls;

namespace RecepiesEverywhere.View
{
    public partial class UserRecipes : UserControl
    {
        public UserRecipes()
        {
            InitializeComponent();
            DataContext = new UserRecipesViewModel();
        }
    }
}