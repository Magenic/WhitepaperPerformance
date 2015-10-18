using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class ByteBitmapHiddenHeightConverter : MvxValueConverter<Byte[], nfloat>
    {
        protected override nfloat Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            int parValue;
            if (!Int32.TryParse(System.Convert.ToString(parameter), out parValue))
            {
                parValue = 0;
            }

            return (value == null || value.Length == 0) ? 0 : parValue;
        }
    }
}
