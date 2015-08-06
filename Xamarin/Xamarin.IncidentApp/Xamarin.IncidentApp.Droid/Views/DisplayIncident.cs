using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.IncidentApp.Droid.MvxMaterial;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "Team Performance", Theme = "@style/Theme.Incident.ActionBar",
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class DisplayIncident : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DisplayIncident);
        }
    }
}