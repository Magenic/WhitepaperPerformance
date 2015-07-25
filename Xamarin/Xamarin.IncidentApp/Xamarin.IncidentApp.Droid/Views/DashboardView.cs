using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Dashboard", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxActionBarActivity //MvxActivity<DashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Dashboard);

            var listView = FindViewById<MvxListView>(Resource.Id.worker_list);
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab .AttachToListView(listView);
            fab.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            var refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh_layout);
            refreshLayout.Refresh += HandleRefresh;
        }

        private async void HandleRefresh(object sender, System.EventArgs e)
        {
            var refreshLayout = sender as SwipeRefreshLayout;
            if (refreshLayout != null)
            {
                await ((DashboardViewModel)ViewModel).RefreshDashboardAsync();
                refreshLayout.Refreshing = false;
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            //var refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh_layout);
        }
    }
}