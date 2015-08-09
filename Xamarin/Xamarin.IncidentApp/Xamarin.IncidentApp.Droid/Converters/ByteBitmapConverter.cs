using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Android.Graphics;
using Android.Graphics.Drawables;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class ByteBitmapConverter : MvxValueConverter<byte[], Drawable>
    {
        private static IDictionary<long, Drawable> drawableCache = new Dictionary<long, Drawable>();

        public static void ClearCache()
        {
            drawableCache = new Dictionary<long, Drawable>();
        }

        protected override Drawable Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }
            var checksum = ComputeAdditionChecksum(value);

            var drawable = drawableCache.SingleOrDefault(c => c.Key == checksum).Value;
            if (drawable != null)
            {
                return drawable;
            }

            var rawBitmap = BitmapFactory.DecodeByteArray(value, 0, value.Length);
            var scaleFactor = 480D / rawBitmap.Width;
            var scaledBitmap = Bitmap.CreateScaledBitmap(rawBitmap, 480, System.Convert.ToInt32(rawBitmap.Height*scaleFactor), false);

            //var stream = new MemoryStream(value);
            //drawable =  Drawable.CreateFromStream(stream, "convertImage");
            drawable = new BitmapDrawable(scaledBitmap);
            drawableCache.Add(checksum, drawable);
            return drawable;
            //return Drawable.CreateFromStream(new memory);
        }

        private long ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);
            return unchecked(longSum);
        }
    }
}