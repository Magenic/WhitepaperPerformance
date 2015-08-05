using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using Android.Views;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class BinaryAssetExistsConverter : MvxValueConverter<byte[], ViewStates>
    {
        protected override ViewStates Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null && value.Length > 0) ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}
