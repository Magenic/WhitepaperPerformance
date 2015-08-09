using System;
using System.Globalization;
using System.IO;
using Android.Graphics;
using Android.Graphics.Drawables;
using Cirrious.CrossCore.Converters;
using Java.IO;

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
            //var bitmap = BitmapFactory.DecodeByteArray(value, 0, value.Length);
            var stream = new MemoryStream(value);
            return Drawable.CreateFromStream(stream, "convertImage");
            //return Drawable.CreateFromStream(new memory);
        }
    }
}