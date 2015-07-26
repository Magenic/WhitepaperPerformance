using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Views.Cells;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    /// <summary>
    /// Class DashboardViewController.
    /// </summary>
    [MvxViewFor(typeof (DashboardViewModel))]
    //[Register("DashboardViewController")]
    partial class DashboardViewController : BaseViewController
    {
        public DashboardViewController()
        {
        }

        public DashboardViewController(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public new DashboardViewModel ViewModel
        {
            get { return (DashboardViewModel) base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        /// <summary>
        /// Fires when the View loads.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            NavigationController.NavigationBarHidden = false;
            SetupTable();
        }

        /// <summary>
        /// Setup the table bindings and refresh properties.
        /// </summary>
        private void SetupTable()
        {
            var refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += async (sender, e) =>
            {
                await ViewModel.RefreshDashboardAsync();
                refreshControl.EndRefreshing();
            };

            var source = new DashboardTableSource(TeamTableView);
            this.CreateBinding(source).To<DashboardViewModel>(vm => vm.UserStatuses).Apply();
            this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<DashboardViewModel>(vm => vm.ItemSelectedCommand).Apply();

            TeamTableView.Source = source;
            TeamTableView.AddSubview(refreshControl);
            TeamTableView.ReloadData();
        }
    }

    /// <summary>
    /// Class DashboardTableSource.
    /// </summary>
    public class DashboardTableSource : MvxStandardTableViewSource
    {
        private static readonly NSString TeamCellIdentifier = new NSString("TeamStatusCell");

        public DashboardTableSource(UITableView tableView) : base(tableView)
        {
        }

        /// <summary>
        /// Called to determine the height of the row at <paramref name="indexPath" />.
        /// </summary>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Location of the row.</param>
        /// <returns>The height of the row (in points) as a <see langword="float" />.</returns>
        /// <remarks><para>This method allows rows to have different heights (for example, rows that contain a variable number of text lines). If this method is implemented, it overrides the <see cref="P:UIKit.UITableView.RowHeight" /> property set on the table view, for the row at <paramref name="indexPath" />.</para>
        /// <para>There are performance implications to using this method instead of <see cref="P:UIKit.UITableView.RowHeight" />: every time a table view is displayed it calls this method for each of its rows. This can result in poor performance when the table has a large number of rows (for example, 1000 rows or more).</para>
        /// <para>Declared in [UITableViewDelegate]</para></remarks>
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 50;
        }

        /// <summary>
        /// Gets the or create cell for.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (TeamStatusCell)tableView.DequeueReusableCell(TeamCellIdentifier);
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }
    }
}