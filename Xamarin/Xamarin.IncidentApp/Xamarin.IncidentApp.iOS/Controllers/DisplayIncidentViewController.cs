using System;
using System.Collections;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Services;
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
        private int _commentProxy;
        private MediaService _mediaService;

        public DisplayIncidentViewController(IntPtr p) : base(p)
        {
            InitView();
        }

        public DisplayIncidentViewController()
        {
            InitView();
        }

        private void InitView()
        {
            SetupActionSheet();
        }

        private void SetupActionSheet()
        {
            _actionSheet = new UIActionSheet("Incident Actions");
            _actionSheet.AddButton("Close Incident");
            //_actionSheet.AddButton("Take Photo");
            //_actionSheet.AddButton("Attach Image");
            //_actionSheet.AddButton("Record Audio");
            _actionSheet.AddButton("Add Comment");
            _actionSheet.AddButton("Cancel");

            _actionSheet.DestructiveButtonIndex = 0;
            _actionSheet.CancelButtonIndex = 2;

            _actionSheet.Clicked += _actionSheet_Clicked;
        }

        void _actionSheet_Clicked(object sender, UIButtonEventArgs e)
        {
            switch (e.ButtonIndex)
            {
                case 0: // Close Incident
                    ViewModel.CloseIncidentCommand.Execute(this);
                    break;

                case 1: // Add Comment
                    ViewModel.AddIncidentDetailCommand.Execute(this);
                    break;

                case 2: // Cancel
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService();
            ViewModel.SetActivityServices(_mediaService);

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
                //this.lblOwnerInfo.Text = string.Concat(value, " - ", OwnerInfo);
                _ownerName = value;
            }
        }

        public string OwnerInfo
        {
            get { return _ownerInfo; }
            set
            {
                //this.lblOwnerInfo.Text = string.Concat(OwnerName, " - ", value);
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
                if (value != null)
                {
                    //this.imgIncidentImage.Image = LoadImage(value);
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
            if (imgUrl.Length == 0)
            {
                return new UIImage();
            }

            using (var url = new NSUrl(imgUrl))
            using (var data = NSData.FromUrl(url))

            return UIImage.LoadFromData(data);
        }

        private void SetupBindings()
        {
            var tableSource = new HeaderTableSource(DisplayIncidentTableView);
            this.CreateBinding(tableSource).To<DisplayIncidentViewModel>(vm => vm.IncidentDetails).Apply();

            DisplayIncidentTableView.Source = tableSource;
            DisplayIncidentTableView.ReloadData();
        }
    }

    public class HeaderTableSource : MvxStandardTableViewSource
    {
        /// <summary>
        /// The team cell identifier
        /// </summary>
        private static readonly NSString HeaderCellIdentifier = new NSString("IncidentHeaderCell");
        private static readonly NSString CommentCellIdentifier = new NSString("CommentCell");

        private static nfloat HeaderHeight = 100;
        private static nfloat DetailMarginHeight = 10;
        private static nfloat DetailCommentHeight = 50;
        private static nfloat DetailImageHeight = 100;
        private static nfloat DetailAudioHeight = 50;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderTableSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public HeaderTableSource(UITableView tableView) : base(tableView) {}

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.LongRow == 0)
            {
                return HeaderHeight;
            }

            var vMs = ((IList<DisplayIncidentDetailViewModel>)ItemsSource);
            var vM = vMs[Convert.ToInt32(indexPath.LongRow)];
            nfloat returnHeight = DetailMarginHeight*2;
            if (!string.IsNullOrEmpty(vM.DetailText))
            {
                returnHeight += DetailCommentHeight;
            }

            if (!string.IsNullOrEmpty(vM.ImageLink))
            {
                returnHeight += DetailImageHeight;
            }

            if (!string.IsNullOrEmpty(vM.AudioRecordingLink))
            {
                returnHeight += DetailAudioHeight;
            }

            return returnHeight;
        }

        /// <summary>
        /// Returns the custom Comment Cell.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            if (indexPath.LongRow == 0)
            {
                return (IncidentHeaderCell)tableView.DequeueReusableCell(HeaderCellIdentifier);
            }

            return (CommentCell)tableView.DequeueReusableCell(CommentCellIdentifier);
        }
    }
}