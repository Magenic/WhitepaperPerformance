using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.Droid.MvxMaterial;

namespace Xamarin.IncidentApp.Droid.Views
{
    public class WorkerQueueFragment : MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.WorkerQueue, null);
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
    }
}