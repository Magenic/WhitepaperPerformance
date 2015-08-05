using System;
using System.Globalization;
using Android.Graphics;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class ByteBitmapConverter : MvxValueConverter<byte[], Bitmap>
    {
        protected override Bitmap Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }
            var bitmap = BitmapFactory.DecodeByteArray(value, 0, value.Length);
            return bitmap;
        }
    }
}