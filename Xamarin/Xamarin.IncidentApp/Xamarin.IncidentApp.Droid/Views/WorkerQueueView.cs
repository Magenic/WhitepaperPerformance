using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.Droid.Constants;
using Xamarin.IncidentApp.Droid.Converters;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Worker Queue", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class WorkerQueueView : MvxActionBarActivity
    {

        private bool refresh;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WorkerQueue);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            var openIncidents = SupportActionBar.NewTab();
            openIncidents.SetText("Open");
            openIncidents.SetTag(KeyConstants.OpenTab);
            openIncidents.SetTabListener(new TabListener<WorkerQueueFragment>(ViewModel));
            SupportActionBar.AddTab(openIncidents);

            var closedIncidents = SupportActionBar.NewTab();
            closedIncidents.SetText("Closed");
            closedIncidents.SetTag(KeyConstants.ClosedTab);
            closedIncidents.SetTabListener(new TabListener<WorkerQueueFragment>(ViewModel));
            SupportActionBar.AddTab(closedIncidents);

            SupportActionBar.NavigationMode = Android.Support.V7.App.ActionBar.NavigationModeTabs;
            refresh = false;
        }

        protected override async void OnResume()
        {
            base.OnResume();
            ViewModel.PropertyChanged -= PropertyChanged;
            ViewModel.PropertyChanged += PropertyChanged;
            
            if (refresh)
            {
                await ViewModel.RefeshIncidentListAsync();
            }
            refresh = true;
        }

        protected override void OnPause()
        {
            base.OnPause();
            ViewModel.PropertyChanged -= PropertyChanged;
            ByteBitmapConverter.ClearCache();
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FullName")
            {
                SupportActionBar.Title = ViewModel.FullName;
            }
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