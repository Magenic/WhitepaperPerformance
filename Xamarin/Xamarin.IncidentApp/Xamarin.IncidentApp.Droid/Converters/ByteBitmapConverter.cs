using System;
using System.Globalization;
using Android.Graphics;
using Android.Graphics.Drawables;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class ByteBitmapConverter : MvxValueConverter<byte[], Drawable>
    {
        protected override Drawable Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }

            using (var rawBitmap = BitmapFactory.DecodeByteArray(value, 0, value.Length))
            {
                var scaleFactor = 480D/rawBitmap.Width;
                using (var scaledBitmap = Bitmap.CreateScaledBitmap(rawBitmap, 480,
                    System.Convert.ToInt32(rawBitmap.Height*scaleFactor), false))
                {
                    return new BitmapDrawable(scaledBitmap);
                }
            }
        }
    }
}