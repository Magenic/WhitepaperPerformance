using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Team Performance", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Dashboard);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
            fab.Show();
        }

        public new DashboardViewModel ViewModel
        {
            get { return (DashboardViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddIncidentCommand.Execute(null);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh_layout);
            refreshLayout.Refresh -= HandleRefresh;
            refreshLayout.Refresh += HandleRefresh;
            refreshLayout.ViewTreeObserver.ScrollChanged -= ScrollChanged;
            refreshLayout.ViewTreeObserver.ScrollChanged += ScrollChanged;
        }

        protected override void OnPause()
        {
            base.OnPause();
            var refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh_layout);
            refreshLayout.Refresh -= HandleRefresh;
            refreshLayout.ViewTreeObserver.ScrollChanged -= ScrollChanged;
        }

        private void ScrollChanged(object sender, System.EventArgs e)
        {
            var workerList = FindViewById<MvxRecyclerView>(Resource.Id.worker_list);
            var refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh_layout);
            var topRowVerticalPosition = (workerList == null || workerList.ChildCount == 0) ? 0 : workerList.GetChildAt(0).Top;
            refreshLayout.Enabled = (topRowVerticalPosition >= 0);
        }

        private async void HandleRefresh(object sender, System.EventArgs e)
        {
            var refreshLayout = sender as SwipeRefreshLayout;
            if (refreshLayout != null)
            {
                await ViewModel.RefreshDashboardAsync();
                refreshLayout.Refreshing = false;
            }
        }
    }
}