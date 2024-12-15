using System.Windows;
using System.Windows.Controls;

namespace RecipesEverywhere.Utilites.Controls
{
    public class NavigationButton : RadioButton
    {
        static NavigationButton()
        {
            var typeOfNavButton = typeof(NavigationButton);
            
            DefaultStyleKeyProperty.OverrideMetadata(typeOfNavButton, new FrameworkPropertyMetadata(typeOfNavButton));
        }
        
    }
}