using System;
using System.Linq;
using System.Windows.Controls;
using RecipesEverywhere.ViewModel;

namespace RecepiesEverywhere.View
{
    public partial class Authorization : UserControl
    {
        public Authorization()
        {
            InitializeComponent();

            DataContext = new AuthorizationViewModel();
        }
    }
}