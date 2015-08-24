using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Cirrious.CrossCore.Converters;
using Foundation;
using UIKit;

namespace Xamarin.IncidentApp.iOS.Converters
{
    public class ByteBitmapConverter : MvxValueConverter<byte[], UIImage>
    {
        private static IDictionary<long, UIImage> _imageCache = new Dictionary<long, UIImage>();

        public static void ClearCache()
        {
            _imageCache = new Dictionary<long, UIImage>();
        }

        protected override UIImage Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }
            var checksum = ComputeAdditionChecksum(value);

            var image = _imageCache.SingleOrDefault(c => c.Key == checksum).Value;
            if (image != null)
            {
                return image;
            }


            var data = NSData.FromArray(value);
            var rawImage = UIImage.LoadFromData(data);

            var scaleFactor = 480D / rawImage.CGImage.Width;
            var scaledImage = MaxResizeImage(rawImage, 480, System.Convert.ToInt32(rawImage.CGImage.Height * scaleFactor));

            _imageCache.Add(checksum, scaledImage);
            return scaledImage;
            //return Drawable.CreateFromStream(new memory);
        }

        private UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Min(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
            if (maxResizeFactor > 1) return sourceImage;
            var width = System.Convert.ToSingle(maxResizeFactor * sourceSize.Width);
            var height = System.Convert.ToSingle(maxResizeFactor * sourceSize.Height);
            
            UIGraphics.BeginImageContextWithOptions(new SizeF(width, height), false, 2.0f); 
            sourceImage.Draw(new RectangleF(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        private long ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);
            return unchecked(longSum);
        }
    }

}
