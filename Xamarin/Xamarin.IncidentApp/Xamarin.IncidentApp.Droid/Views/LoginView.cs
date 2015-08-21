using Android.App;
using Android.Content.PM;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Xamarin.IncidentApp.Droid.Services;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "", Theme = "@style/Theme.IncidentSplash",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            ViewModel.LoginService = new LoginService(this, Mvx.Resolve<IAzureService>());
        }
    }
}