using System;
using System.Collections.Generic;
using System.Net.Http;
using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using TestIncidentQueueService.TestHarness.Models;

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
            var btnProfileInfo = FindViewById<Button>(Resource.Id.btnProfileInfo);
            var btnWorkers = FindViewById<Button>(Resource.Id.btnWorkers);
            var btnStatuses = FindViewById<Button>(Resource.Id.btnStatuses);
            var btnIncidents = FindViewById<Button>(Resource.Id.btnIncidents);

            btnLogin.Click += btnLogin_Click;
            btnLogout.Click += btnLogout_Click;
            btnProfileInfo.Click += btnProfileInfo_Click;
            btnWorkers.Click += btnWorkers_Click;
            btnStatuses.Click += btnStatuses_Click;
            btnIncidents.Click += btnIncidents_Click;
        }

        private async void btnIncidents_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                var profile = await service.InvokeApiAsync<UserProfile>("Profile", HttpMethod.Get, null);
                var incidents = await service.GetTable<Incident>().Where(i => i.AssignedToId == profile.UserId).ToListAsync();
                // Do something with the result. Used to know Id of currently logged on person and their name/display name and if they are a manager.
            }
        }

        private async void btnStatuses_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                try
                {
                    var statuses = await service.InvokeApiAsync<IList<UserStatus>>("StatusList", HttpMethod.Get, null);
                    // Do something with the result, used by the manager status screen.
                }
                catch (MobileServiceInvalidOperationException ex)
                {
                    Toast.MakeText(this, "You must be in the manager role to call this method.", ToastLength.Long).Show();
                }
            }
        }

        private async void btnWorkers_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                try
                {
                    var workers = await service.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null);
                    // Do something with the result, used in app for list of people to assign an incident to.
                }
                catch (MobileServiceInvalidOperationException ex)
                {
                    Toast.MakeText(this, "You must be in the manager role to call this method.", ToastLength.Long).Show();
                }
            }
        }

        private async void btnProfileInfo_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                var profile = await service.InvokeApiAsync<UserProfile>("Profile", HttpMethod.Get, null);
                // Do something with the result. Used to know Id of currently logged on person and their name/display name and if they are a manager.
            }
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                service.Logout();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            
            try
            {
                var token = await service.LoginAsync(this, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
                // Possible this token should be saved.
            }
            catch (InvalidOperationException ex)
            {
                Toast.MakeText(this, "Login Cancelled", ToastLength.Long).Show();
            }
        }
    }
}

