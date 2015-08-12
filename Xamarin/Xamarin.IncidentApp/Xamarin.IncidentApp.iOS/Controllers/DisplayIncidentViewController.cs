using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Views.Cells;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    //[Register("DisplayIncidentViewController")]    
    [MvxViewFor(typeof(DisplayIncidentViewModel))]
    public partial class DisplayIncidentViewController : BaseViewController
    {
        private string _ownerName;
        private string _ownerInfo;
        private string _incidentImage;
        private UIActionSheet _actionSheet;

        public DisplayIncidentViewController(IntPtr p) : base(p)
        {
            SetupActionSheet();
        }

        public DisplayIncidentViewController()
        {
            SetupActionSheet();
        }

        private void SetupActionSheet()
        {
            _actionSheet = new UIActionSheet("Incident Actions");
            _actionSheet.AddButton("Close Incident");
            _actionSheet.AddButton("Take Photo");
            _actionSheet.AddButton("Attach Image");
            _actionSheet.AddButton("Record Audio");
            _actionSheet.AddButton("Cancel");

            _actionSheet.DestructiveButtonIndex = 0;
            _actionSheet.CancelButtonIndex = 4;

            _actionSheet.Clicked += _actionSheet_Clicked;
        }

        void _actionSheet_Clicked(object sender, UIButtonEventArgs e)
        {
            switch (e.ButtonIndex)
            {
                case 0: // Close Incident
                    break;

                case 1: // Attach Image
                    break;

                case 2: // Record Audio
                    break;

                case 3: // Take Photo
                    break;

                case 4: // Cancel
                    break;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                // button was clicked
                _actionSheet.ShowInView(View);
            })
            , true);

            this.Title = "";

            SetupBindings();
        }
        public new DisplayIncidentViewModel ViewModel
        {
            get { return (DisplayIncidentViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
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

        /// <summary>
        /// Gets or sets the incident image.
        /// </summary>
        /// <value>The incident image.</value>
        public string IncidentImage
        {
            get { return _incidentImage; }
            set
            {
                this.imgIncidentImage.Image = LoadImage(value);
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
        private void SetupBindings()
        {
            var source = new CommentTableSource(CommentTableView);
            this.CreateBinding(source).To<DisplayIncidentViewModel>(vm => vm.IncidentDetails).Apply();

            CommentTableView.Source = source;
            CommentTableView.ReloadData();

            this.CreateBinding().For(c => c.IncidentImage).To((DisplayIncidentViewModel property) => property.ImageLink).Apply();
            this.CreateBinding(lblSubject).For(f => f.Text).To<DisplayIncidentViewModel>(vm => vm.Subject).Apply();
            this.CreateBinding().For(c => c.OwnerName).To((DisplayIncidentViewModel property) => property.FullName).Apply();
            this.CreateBinding().For(c => c.OwnerInfo).To((DisplayIncidentViewModel property) => property.DateOpened).Apply();
            this.CreateBinding(lblDescription).For(f => f.Text).To<DisplayIncidentViewModel>(vm => vm.Description).Apply();
        }
    }

    public class CommentTableSource : MvxStandardTableViewSource
    {
        /// <summary>
        /// The team cell identifier
        /// </summary>
        private static readonly NSString CommentCellIdentifier = new NSString("CommentCell");

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentTableSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public CommentTableSource(UITableView tableView) : base(tableView) {}

        /// <summary>
        /// Returns the custom Comment Cell.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (CommentCell)tableView.DequeueReusableCell(CommentCellIdentifier);
            return cell;
        }
    }
}