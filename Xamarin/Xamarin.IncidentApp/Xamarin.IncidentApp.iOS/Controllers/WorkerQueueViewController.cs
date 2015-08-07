using System;
using System.Diagnostics;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Views.Cells;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    //[Register("WorkerQueueViewController")]    
    [MvxViewFor(typeof(WorkerQueueViewModel))]
    partial class WorkerQueueViewController : BaseViewController
    {
        public WorkerQueueViewController()
        {
        }

        public WorkerQueueViewController(IntPtr p) : base(p)
        {
        }
        
        public new WorkerQueueViewModel ViewModel
        {
            get { return (WorkerQueueViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        partial void sgOpenCloseFilter_ValueChanged(UIKit.UISegmentedControl sender)
        {
            ViewModel.ShowClosed = (sgOpenCloseFilter.SelectedSegment == 1);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                // button was clicked
                Debug.WriteLine("Add incident pressed!");
            })
            , true);

            SetupTable();
        }

        private void SetupTable()
        {
            var refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += async (sender, e) =>
            {
                await ViewModel.RefeshIncidentListAsync();
                refreshControl.EndRefreshing();
            };

            var source = new IncidentQueueTableSource(IncidentQueueTableView);
            this.CreateBinding(source).To<WorkerQueueViewModel>(vm => vm.Incidents).Apply();
            this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<WorkerQueueViewModel>(vm => vm.ShowIncidentItemCommand).Apply();

            IncidentQueueTableView.Source = source;
            IncidentQueueTableView.AddSubview(refreshControl);
            IncidentQueueTableView.ReloadData();
        }

    }

    public class IncidentQueueTableSource : MvxStandardTableViewSource
    {
        /// <summary>
        /// The Incident Queue cell identifier
        /// </summary>
        private static readonly NSString IncidentCellIdentifier = new NSString("IncidentQueueCell");

        /// <summary>
        /// Initializes a new instance of the <see cref="IncidentQueueTableSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public IncidentQueueTableSource(UITableView tableView) : base(tableView) {}

        /// <summary>
        /// Returns the custom Team Status Cell.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (IncidentQueueCell)tableView.DequeueReusableCell(IncidentCellIdentifier);
            return cell;
        }
    }
}