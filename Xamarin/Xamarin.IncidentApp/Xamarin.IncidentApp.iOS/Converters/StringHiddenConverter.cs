using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class StringHiddenConverter : MvxValueConverter<string, Boolean>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
