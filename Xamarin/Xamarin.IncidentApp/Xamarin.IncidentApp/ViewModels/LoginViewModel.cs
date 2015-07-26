using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ILoginService _login;

        public LoginViewModel(INetworkService networkService, IUserDialogs userDialogs)
            : base(networkService, userDialogs)
        {
        }

        public ILoginService LoginService
        {
            set { _login = value; }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new MvxCommand(async () =>
                {
                    await LoginAndGo();
                }));
            }
        }

        public async Task LoginAndGo()
        {
            if (_login == null)
            {
                throw new ArgumentException("Login Service must be set.");
            }

            if (!NetworkService.IsConnected)
            {
                await UserDialogs.AlertAsync("Device not connected to network, cannot login. Please try again later", "Network Error");
                return;
            }

            try
            {
                var service = MobileService.Service;

               await _login.LoginAsync().ConfigureAwait(true);
                var profile = await LoadProfileAsync(service);
                UserContext.UserProfile = profile;

                if (profile.Manager)
                {
                    ShowViewModel<DashboardViewModel>();
                }
                else
                {
                    ShowViewModel<WorkerQueueViewModel>(profile.UserId);
                }
                Close(this);
            }
            catch (InvalidOperationException e)
            {
                UserDialogs.Alert("User login canceled", "Login Error");
            }
            catch (Exception e)
            {
                UserDialogs.Alert("An unknown exception occurred logging in, please try again.", "Login Error");
            }
        }

        private async Task<UserProfile> LoadProfileAsync(MobileServiceClient service)
        {
            try
            {
                UserDialogs.ShowLoading("Loading User Information...");
                var profile = await service.InvokeApiAsync<UserProfile>("Profile", HttpMethod.Get, null);
                UserContext.UserProfile = profile;
                return profile;
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }
    }
}
