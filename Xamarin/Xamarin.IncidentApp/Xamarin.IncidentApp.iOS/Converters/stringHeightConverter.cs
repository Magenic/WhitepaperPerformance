using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class StringHeightConverter : MvxValueConverter<string, nfloat>
    {
        protected override nfloat Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !GetnFloat(parameter).HasValue)
            {
                throw new ArgumentException();
            }
            return string.IsNullOrEmpty(value)? 0: GetnFloat(parameter).Value;
        }

        private nfloat? GetnFloat(object parameter)
        {
            nfloat result;
            if (!nfloat.TryParse(parameter.ToString(), out result))
            {
                return null;
            }
            return result;
        }
    }
}