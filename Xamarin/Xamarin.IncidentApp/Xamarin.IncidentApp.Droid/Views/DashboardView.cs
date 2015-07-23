using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Dashboard", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxActivity<DashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Dashboard);

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
                await ViewModel.RefreshDashboardAsync();
                refreshLayout.Refreshing = false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var refreshLayout = FindViewById(Resource.Id.worker_list);

            //refreshLayout.Refresh += async (sender, e) =>
            //{
            //    await ViewModel.RefreshDashboardAsync();
            //    refreshLayout.Refreshing = false;
            //};
        }
    }
}