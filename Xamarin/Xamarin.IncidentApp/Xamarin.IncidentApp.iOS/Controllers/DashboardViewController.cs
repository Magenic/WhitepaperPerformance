// ***********************************************************************
// Assembly         : XamarinIncidentAppiOS
// Author           : Ken Ross
// Created          : 07-22-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-26-2015
// ***********************************************************************
using System;
using System.Diagnostics;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Views.Cells;
using Xamarin.IncidentApp.ViewModels;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace Xamarin.IncidentApp.iOS.Controllers
{
    /// <summary>
    /// Class DashboardViewController.
    /// </summary>
    [MvxViewFor(typeof (DashboardViewModel))]
    partial class DashboardViewController : BaseViewController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewController"/> class.
        /// </summary>
        public DashboardViewController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewController"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
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

        /// <summary>
        /// Override to handle memory warnings.
        /// </summary>
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ViewModel.RefreshDashboardAsync();
        }

        /// <summary>
        /// Fires when the View loads.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
                {
                    // button was clicked
                    ViewModel.AddIncidentCommand.Execute(this);
                })
            , true);
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
            this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<DashboardViewModel>(vm => vm.ShowWorkerQueueCommand).Apply();

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
        /// <summary>
        /// The team cell identifier
        /// </summary>
        private static readonly NSString TeamCellIdentifier = new NSString("TeamStatusCell");

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardTableSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public DashboardTableSource(UITableView tableView) : base(tableView)
        {
        }

        /// <summary>
        /// Returns the custom Team Status Cell.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (TeamStatusCell)tableView.DequeueReusableCell(TeamCellIdentifier);
            return cell;
        }
    }
}