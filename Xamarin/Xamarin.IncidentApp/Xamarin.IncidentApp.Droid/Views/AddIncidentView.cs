using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "New Incident", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddIncidentView : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WorkerQueue);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        public new AddIncidentViewModel ViewModel
        {
            get { return (AddIncidentViewModel)base.ViewModel; }
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