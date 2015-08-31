// ***********************************************************************
// Assembly         : XamarinIncidentAppiOS
// Author           : Ken Ross
// Created          : 08-07-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 08-08-2015
// ***********************************************************************

using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.ViewModels;

/// <summary>
/// The Cells namespace.
/// </summary>
namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    /// <summary>
    /// Class IncidentQueueCell.
    /// </summary>
    public partial class IncidentQueueCell : MvxTableViewCell
    {
        public static readonly NSString Identifier = new NSString("IncidentQueueCell");

        private string _incidentImage;
        private string _ownerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncidentQueueCell"/> class.
        /// </summary>
        public IncidentQueueCell()
        {
            InitializeBindings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IncidentQueueCell"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public IncidentQueueCell(IntPtr handle) : base(handle)
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
                this.imgIncidentPicture.Image = LoadImage(value);
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

        /// <summary>
        /// Gets or sets the name of the owner. 
        /// When setting the property the associated control text is set with an "Owner" prefix.
        /// </summary>
        /// <value>The name of the owner.</value>
        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                this.lblOwner.Text = string.Concat("Owner: ", value);
                _ownerName = value;
            }
        }

        /// <summary>
        /// Initializes the cell bindings.
        /// </summary>
        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblSubject).For(c => c.Text).To((WorkerQueueItemViewModel property) => property.Subject).Apply();
                this.CreateBinding().For(c => c.OwnerName).To((WorkerQueueItemViewModel property) => property.FullName).Apply();
                this.CreateBinding(lblIncidentDate).For(c => c.Text).To((WorkerQueueItemViewModel property) => property.DateOpened).Apply();
                this.CreateBinding(lblIncidentDate).For(c => c.Text).To((WorkerQueueItemViewModel property) => property.DateOpened).WithConversion("Date").Apply();
                this.CreateBinding(imgIncidentPicture).For(c => c.ImageUrl).To((WorkerQueueItemViewModel property) => property.ImageLink).Apply();
            });
        }
    }
}