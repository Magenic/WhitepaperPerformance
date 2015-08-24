using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("IncidentHeaderCell")]
    public partial class IncidentHeaderCell : MvxTableViewCell
    {
        private string _incidentImage;
        private string _ownerName;
        private string _ownerInfo;

        public IncidentHeaderCell()
        {
            InitializeBindings();
        }

        public IncidentHeaderCell(IntPtr handle) : base(handle)
        {
            InitializeBindings();
        }

        /// <summary>
        /// Gets or sets the incident image.
        /// </summary>
        /// <value>The incident image.</value>
        public string IncidentImage
        {
            get { return _incidentImage; }
            set
            {
                if (value.Length > 0)
                {
                    this.imgIncidentImage.Image = LoadImage(value);
                    imgIncidentImage.Hidden = false;
                }
                else
                {
                    imgIncidentImage.Hidden = true;
                }
                _incidentImage = value;
            }
        }

        /// <summary>
        /// Loads the image based on the provided URL.
        /// </summary>
        /// <param name="imgUrl">The img URL.</param>
        /// <returns>UIImage.</returns>
        private UIImage LoadImage(string imgUrl)
        {
            using (var url = new NSUrl(imgUrl))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }


        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                this.lblOwner.Text = string.Concat(value, " - ", OwnerInfo);
                _ownerName = value;
            }
        }

        public string OwnerInfo
        {
            get { return _ownerInfo; }
            set
            {
                this.lblOwner.Text = string.Concat(OwnerName, " - ", value);
                _ownerInfo = value;
            }
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblSubject).For(c => c.Text).To((DisplayIncidentViewModel property) => property.Subject).Apply();
                this.CreateBinding(lblDescription).For(c => c.Text).To((DisplayIncidentViewModel property) => property.Description).Apply();
                this.CreateBinding(lblDescription).For(c => c.Hidden).To((DisplayIncidentViewModel property) => property.Description).WithConversion("InvertedVisibility").Apply();
                //this.CreateBinding().For(c => c.IncidentImage).To((DisplayIncidentViewModel property) => property.ImageLink).Apply();
                this.CreateBinding().For(c => c.OwnerName).To((DisplayIncidentViewModel property) => property.FullName).Apply();
                this.CreateBinding().For(c => c.OwnerInfo).To((DisplayIncidentViewModel property) => property.DateOpened).Apply();
                this.CreateBinding(imgIncidentImage).For(c => c.Image).To((DisplayIncidentViewModel property) => property.ImageBytes).WithConversion("ByteBitmap").Apply();
            });
        }

    }
}