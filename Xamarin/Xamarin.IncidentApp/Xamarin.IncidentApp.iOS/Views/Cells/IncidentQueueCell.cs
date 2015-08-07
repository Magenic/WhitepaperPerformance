using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.Models;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("IncidentQueueCell")]
    public partial class IncidentQueueCell : MvxTableViewCell
    {
        public static readonly NSString Identifier = new NSString("IncidentQueueCell");
        private string _incidentImage;

        public IncidentQueueCell()
        {
            InitializeBindings();
        }

        public IncidentQueueCell(IntPtr handle) : base(handle)
        {
            InitializeBindings();
        }

        public string IncidentImage
        {
            get { return _incidentImage; }
            set
            {
                this.imgIncidentPicture.Image = LoadImage(value);
                _incidentImage = value;
            }
        }

        private UIImage LoadImage(string imgUrl)
        {
            using (var url = new NSUrl(imgUrl))
            using (var data = NSData.FromUrl(url))
            return UIImage.LoadFromData(data);
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblSubject).For(c => c.Text).To((Incident property) => property.Subject).Apply();
                this.CreateBinding(lblOwner).For(c => c.Text).To((Incident property) => property.Id).Apply();
                this.CreateBinding(lblIncidentDate).For(c => c.Text).To((Incident property) => property.DateOpened).Apply();
                this.CreateBinding().For(c => c.IncidentImage).To((Incident property) => property.ImageLink).Apply();

            });
        }

    }
}