using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "", Theme = "@style/Theme.Incident.TransparentActionBar",
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class DisplayIncident : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DisplayIncident);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        public new DisplayIncidentViewModel ViewModel
        {
            get { return (DisplayIncidentViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.DisplayIncidentMenu, menu);
            return base.OnCreateOptionsMenu(menu);
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