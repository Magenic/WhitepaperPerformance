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
            var btnSasKey = FindViewById<Button>(Resource.Id.btnSasKey);
            var btnCreate1000ClosedRequests = FindViewById<Button>(Resource.Id.btnCreate1000ClosedRequests);

            btnLogin.Click += btnLogin_Click;
            btnLogout.Click += btnLogout_Click;
            btnProfileInfo.Click += btnProfileInfo_Click;
            btnWorkers.Click += btnWorkers_Click;
            btnStatuses.Click += btnStatuses_Click;
            btnIncidents.Click += btnIncidents_Click;
            btnSasKey.Click += btnSasKey_Click;
            btnCreate1000ClosedRequests.Click += btnCreate1000ClosedRequests_Click;
        }

        private async void btnSasKey_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                var profile = await service.InvokeApiAsync<UserProfile>("Profile", HttpMethod.Get, null);
                var sasKey = await service.InvokeApiAsync<string>("SasGenerator", HttpMethod.Get, null);
                // Do something with the result. Used to know Id of currently logged on person and their name/display name and if they are a manager.
            }
        }

        private async void btnIncidents_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                var profile = await service.InvokeApiAsync<UserProfile>("Profile", HttpMethod.Get, null);

                var incidents2 = await service.GetTable<Incident>().ToListAsync();

                var incidents = await service.GetTable<Incident>().Where(i => i.AssignedToId == profile.UserId).ToListAsync();
                // Do something with the result. Used to know Id of currently logged on person and their name/display name and if they are a manager.
            }
        }

        private string getRandomImage()
        {

            List<string> images = new List<string>()
            {
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/b4736911-ff82-4517-876c-ae48de7fc20f.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/8c853557-f5ad-48f7-b937-bac8ae2367fc.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/a7cc0106-a64a-45b1-91b9-a59b43803512.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/09d51f5d-a9df-4655-b28c-08e3e87a5e33.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/6afc7445-4ef9-1bc4-f749-f9660e98f522.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/42d6dae7-f587-d308-f893-590fc9f5fe8c.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/43c6d5b7-8a04-8fa2-38d5-f61a98ad9e59.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/1b3d965a-57af-4671-a8fa-8f2f561f5e99.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/189f4f87-7238-43fb-9fa9-9487d124c1a9.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/89c915cb-ae75-475a-819c-68a574220c9f.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/2fe11410-39ab-4149-80cd-3052b1d2da5c.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/a19815e8-66e3-4107-8abd-58b07204d85f.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/b6735d1e-ebaf-4db2-b014-fd23fe030283.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/5cd3b3a1-21d0-809b-9290-386f4aa4dcbf.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/c02b2039-be50-dad1-b00b-d35345d7181f.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/67ab1897-5092-4e62-b6f3-59ae7635c7a2.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/1ff77af3-5f3a-4a80-b453-29ed9b2ca95c.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/5af8c74e-794c-4701-b7bd-839266a26e61.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/5e94637f-a745-c7ba-c0ab-c073d79d637d.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/d4f3f3f8-013a-4b8e-b5ff-e7296e8a56a4.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/8a34cc1c-357e-4099-a053-18081560c983.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/9aac2b66-9068-95ce-e9de-f0a469c39730.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/30ae4695-24d1-9019-7577-227f2e39f691.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/c9dfe29e-8b66-4b3f-9436-55c970a455a1.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/cef1b7ab-6f9f-e678-aa9e-cf7de0caa5ec.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/fa96083b-bf30-03ee-bc44-6f233f0f3a3c.png",
                "https://testincidentqueue.blob.core.windows.net/incidentbinaries/d1735e1d-88aa-4944-b74e-9c65cd4154d7.png"
            };
            Random rnd = new Random();
            int r = rnd.Next(images.Count);
            return images[r];
        }

        private async void btnCreate1000ClosedRequests_Click(object sender, EventArgs e)
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {

                for (int i = 1; i < 1001; i++)
                {

                    var newIncident = new Incident
                    {
                        Subject = "test # " + i,
                        AssignedToId = "Michonne@TestIncidentQueue.onmicrosoft.com",
                        Description = "this is a test closed incident",
                        ImageLink = getRandomImage(),
                        AudioLink = null,
                        Closed = true,
                        DateOpened = DateTime.MinValue,
                        DateClosed = DateTime.MinValue
                    };

                    await service.GetTable<Incident>().InsertAsync(newIncident);

                }

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

