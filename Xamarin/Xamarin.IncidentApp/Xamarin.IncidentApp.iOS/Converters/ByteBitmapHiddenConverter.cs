using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class ByteBitmapHiddenConverter : MvxValueConverter<Byte[], Boolean>
    {
        protected override bool Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || value.Length == 0;
        }
    }
}