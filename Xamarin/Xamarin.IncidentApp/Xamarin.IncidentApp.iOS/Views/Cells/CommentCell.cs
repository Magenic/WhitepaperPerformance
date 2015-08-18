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
    //[Register("CommentCell")]
    public partial class CommentCell : MvxTableViewCell
    {
        private string _incidentImage;
        private string _ownerName;
        private string _ownerInfo;

        public CommentCell()
        {
            InitializeBindings();
        }

        public CommentCell(IntPtr handle) : base(handle)
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
                    this.imgCommentImage.Image = LoadImage(value);
                    imgCommentImage.Hidden = false;
                }
                else
                {
                    imgCommentImage.Hidden = true;
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
                this.lblOwnerInfo.Text = string.Concat(value, " - ", OwnerInfo);
                _ownerName = value;
            }
        }

        public string OwnerInfo
        {
            get { return _ownerInfo; }
            set
            {
                this.lblOwnerInfo.Text = string.Concat(OwnerName, " - ", value);
                _ownerInfo = value;
            }
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblComment).For(c => c.Text).To((DisplayIncidentDetailViewModel property) => property.DetailText).Apply();
                this.CreateBinding().For(c => c.IncidentImage).To((DisplayIncidentDetailViewModel property) => property.ImageLink).Apply();
                this.CreateBinding().For(c => c.OwnerName).To((DisplayIncidentDetailViewModel property) => property.FullName).Apply();
                this.CreateBinding().For(c => c.OwnerInfo).To((DisplayIncidentDetailViewModel property) => property.DateOpened).Apply();
            });
        }
    }
}