using System;
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
            this.EdgesForExtendedLayout = UIRectEdge.None;
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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ViewModel.LoadIncidentAsync();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService(this);
            ViewModel.SetActivityServices(_mediaService);

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                // button was clicked
                _actionSheet.ShowInView(View);
            })
            , true);

            Title = "";

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

        private void SetupBindings()
        {
            var tableSource = new IncidentTableSource(DisplayIncidentTableView);
            this.CreateBinding(tableSource).To<DisplayIncidentViewModel>(vm => vm.CurrentViewModel).WithConversion("DetailList").Apply();

            DisplayIncidentTableView.Source = tableSource;
            DisplayIncidentTableView.ReloadData();
        }
    }

    public class IncidentTableSource : MvxStandardTableViewSource
    {
        /// <summary>
        /// The team cell identifier
        /// </summary>
        private static readonly NSString HeaderCellIdentifier = new NSString("IncidentHeaderCell");
        private static readonly NSString CommentCellIdentifier = new NSString("CommentCell");

        private static nfloat HeaderPaddingHeight = 5;
        private static nfloat HeaderDescriptionHeight = 106;
        private static nfloat HeaderAudioNoteHeight = 34;
        private static nfloat DetailMarginHeight = 20;
        private static nfloat DetailCommentHeight = 40;
        private static nfloat DetailAudioHeight = 55;
        private static nfloat DetailImageMargin = 22;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncidentTableSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public IncidentTableSource(UITableView tableView) : base(tableView) {}

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            //var screenRect = TableView.Bounds;
            //var screenWidth = screenRect.Size.Width;
            var vMs = ((IList<BaseViewModel>)ItemsSource);
            var imageDimension = TableView.ContentSize.Width;
           
            if (indexPath.LongRow == 0)
            {
                var returnValue = HeaderPaddingHeight + imageDimension;
                var headerVm = (DisplayIncidentViewModel)vMs[Convert.ToInt32(indexPath.LongRow)];
                if (!string.IsNullOrEmpty(headerVm.Description))
                {
                    returnValue += HeaderDescriptionHeight;
                }
                if (!string.IsNullOrEmpty(headerVm.AudioRecordingLink))
                {
                    returnValue += HeaderAudioNoteHeight;
                }
                return returnValue;
            }

            var vM = (DisplayIncidentDetailViewModel)vMs[Convert.ToInt32(indexPath.LongRow)];
            nfloat returnHeight = DetailMarginHeight*2;

            if (!string.IsNullOrEmpty(vM.DetailText))
            {
                returnHeight += DetailCommentHeight;
            }

            if (!string.IsNullOrEmpty(vM.ImageLink))
            {
                returnHeight += (imageDimension - (DetailImageMargin * 2)); // screenRect.Width;
            }

            if (!string.IsNullOrEmpty(vM.AudioRecordingLink))
            {
                returnHeight += DetailAudioHeight;
            }

            return returnHeight;
        }

        /// <summary>
        /// Returns the custom Comment Cell or Header Cell, depending on the value of indexPath.LongRow.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var vMs = ((IList<BaseViewModel>)ItemsSource);
            if (indexPath.LongRow == 0)
            {
                var headerCell = (IncidentHeaderCell)tableView.DequeueReusableCell(HeaderCellIdentifier);
                var headerVm = (DisplayIncidentViewModel)vMs[Convert.ToInt32(indexPath.LongRow)];
                headerCell.SetDescriptionHeight(string.IsNullOrEmpty(headerVm.Description) ? 0 : HeaderDescriptionHeight);
                headerCell.SetPlayAudioHeight(string.IsNullOrEmpty(headerVm.AudioRecordingLink) ? 0 : HeaderAudioNoteHeight);
                return headerCell;
            }

            var vM = (DisplayIncidentDetailViewModel)vMs[Convert.ToInt32(indexPath.LongRow)];
            var detailCell = (CommentCell)tableView.DequeueReusableCell(CommentCellIdentifier);

            detailCell.SetCommentHeight(string.IsNullOrEmpty(vM.DetailText) ? 0 : DetailCommentHeight);
            detailCell.SetImageHeight(string.IsNullOrEmpty(vM.ImageLink) ? 0 : 1);
            detailCell.SetPlayAudioHeight(string.IsNullOrEmpty(vM.AudioRecordingLink) ? 0 : DetailAudioHeight);
            return detailCell;
        }
    }
}