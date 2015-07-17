using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;

namespace TestIncidentQueueService.TestHarness
{
    [Activity(Label = "TestIncidentQueueService.TestHarness", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            var btnLogout = FindViewById<Button>(Resource.Id.btnLogout);

            btnLogin.Click += btnLogin_Click;
            btnLogout.Click += btnLogout_Click;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                service.Logout();
                ShowUser();
            }
        }

        private void ShowUser()
        {
            var service = Utilities.MobileService.Service;
            var lblUser = FindViewById<TextView>(Resource.Id.lblUser);

            if (service.CurrentUser != null)
            {
                lblUser.Text = service.CurrentUser.UserId;
            }
            else
            {
                lblUser.Text = string.Empty;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            
            try
            {
                var token = await service.LoginAsync(this, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
                // Possible this token should be saved.
                ShowUser();
            }
            catch (InvalidOperationException)
            {
                Toast.MakeText(this, "Login Cancelled", ToastLength.Long).Show();
            }
        }
    }
}

