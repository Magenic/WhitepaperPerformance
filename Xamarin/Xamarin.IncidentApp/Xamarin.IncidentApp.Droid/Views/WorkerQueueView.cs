using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Worker Queue", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class WorkerQueueView : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WorkerQueue);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            var openIncidents = SupportActionBar.NewTab();
            openIncidents.SetText("OPEN");
            openIncidents.SetTabListener(new TabListener<WorkerQueueFragment>("A", ViewModel));
            SupportActionBar.AddTab(openIncidents);

            var closedIncidents = SupportActionBar.NewTab();
            closedIncidents.SetText("Closed");
            closedIncidents.SetTabListener(new TabListener<WorkerQueueFragment>("B", ViewModel));
            SupportActionBar.AddTab(closedIncidents);

            SupportActionBar.NavigationMode = Android.Support.V7.App.ActionBar.NavigationModeTabs;
        }

        public new WorkerQueueViewModel ViewModel
        {
            get { return (WorkerQueueViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}