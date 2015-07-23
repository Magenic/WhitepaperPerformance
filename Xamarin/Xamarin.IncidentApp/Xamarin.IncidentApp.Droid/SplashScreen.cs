using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;

namespace Xamarin.IncidentApp.Droid
{
    [Activity(
		Label = "Xamarin.IncidentApp.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
        , Theme = "@style/Theme.IncidentSplash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}