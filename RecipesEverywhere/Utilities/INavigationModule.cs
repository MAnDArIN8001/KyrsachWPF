using System.Windows.Navigation;

namespace RecipesEverywhere.Utilites
{
    public interface INavigationModule
    {
        public void InitializeNavigation(NavigationService navigationService);
    }
}