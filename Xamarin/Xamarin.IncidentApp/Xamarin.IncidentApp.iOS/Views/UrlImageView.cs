using System;
using Foundation;
using SDWebImage;
using UIKit;

namespace Xamarin.IncidentApp.iOS.Views
{
    public partial class UrlImageView : UIImageView
    {
        public UrlImageView(IntPtr handle) : base (handle)
		{

		}

        private string _imageUrl;
        public string ImageUrl
        {
            set
            {
                _imageUrl = value;
                if (!string.IsNullOrEmpty(value))
                {
                    ContentMode = UIViewContentMode.ScaleAspectFill;
                    ClipsToBounds = true;
                    var url = new NSUrl(value);
                    this.SetImage(url);                 
                }
                else
                {
                    Image = null;
                }
            }
            get { return _imageUrl; }
        }
    }
}
