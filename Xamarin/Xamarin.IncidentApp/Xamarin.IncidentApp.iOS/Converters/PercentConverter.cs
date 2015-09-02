using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class PercentConverter : MvxValueConverter<int, float>
    {
        protected override float Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return value / 100f;
        }
    }
}
