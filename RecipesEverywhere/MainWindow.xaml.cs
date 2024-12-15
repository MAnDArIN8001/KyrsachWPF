using System.Windows;
using RecipesEverywhere.Services;
using RecipesEverywhere.ViewModel;

namespace RecepiesEverywhere
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainPageViewModel();
        }
    }
}