using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using Cirrious.MvvmCross.Droid.Support.RecyclerView;
using Cirrious.MvvmCross.Droid.Support.V4;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    public class WorkerQueueFragment : MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
        //    var ignored = base.OnCreateView(inflater, container, savedInstanceState);

        //    return inflater.Inflate(Resource.Layout.WorkerQueue, null);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);
            //var workerQueue = Activity.FindViewById<MvxRecyclerView>(Resource.Id.worker_queue_list);
            //workerQueue.ItemClick = ((WorkerQueueViewModel) ViewModel).ShowIncidentItemCommand;
            var refreshLayout = Activity.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.worker_refresh_layout);
            refreshLayout.Refresh -= HandleRefresh;
            refreshLayout.Refresh += HandleRefresh;
            refreshLayout.ViewTreeObserver.ScrollChanged -= ScrollChanged;
            refreshLayout.ViewTreeObserver.ScrollChanged += ScrollChanged;

        }

        public override void OnDetach()
        {
            base.OnDetach();
            var refreshLayout = Activity.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.worker_refresh_layout);
            refreshLayout.Refresh -= HandleRefresh;
            refreshLayout.ViewTreeObserver.ScrollChanged -= ScrollChanged;
        }

        private void ScrollChanged(object sender, System.EventArgs e)
        {
            var workerList = Activity.FindViewById<MvxRecyclerView>(Resource.Id.worker_queue_list);
            var refreshLayout = Activity.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.worker_refresh_layout);
            var topRowVerticalPosition = (workerList == null || workerList.ChildCount == 0) ? 0 : workerList.GetChildAt(0).Top;
            refreshLayout.Enabled = (topRowVerticalPosition >= 0);
        }

        private async void HandleRefresh(object sender, System.EventArgs e)
        {
            var refreshLayout = sender as MvxSwipeRefreshLayout;
            if (refreshLayout != null)
            {
                await ((WorkerQueueViewModel)ViewModel).RefeshIncidentListAsync();
                refreshLayout.Refreshing = false;
            }
        }
    }
}