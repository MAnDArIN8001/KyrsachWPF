using System;
using System.Globalization;
using System.Windows.Data;

namespace RecipesEverywhere.Utilites
{
    public class BoolToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is bool and true ? "Visible" : "Collapsed";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}