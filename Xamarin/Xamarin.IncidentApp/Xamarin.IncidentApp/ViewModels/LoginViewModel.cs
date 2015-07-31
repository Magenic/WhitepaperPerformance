// ***********************************************************************
// Assembly         : Xamarin.IncidentApp
// Author           : Ken Ross
// Created          : 07-26-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-26-2015
// ***********************************************************************

using System;
using System.Collections.Generic;
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

/// <summary>
/// The ViewModels namespace.
/// </summary>

namespace Xamarin.IncidentApp.ViewModels
{
    /// <summary>
    ///     Class LoginViewModel.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private ILoginService _loginService;
        private ICommand _loginCommand;
        private IAzureService _azureService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel" /> class.
        /// </summary>
        /// <param name="networkService">The network service.</param>
        /// <param name="userDialogs">The user dialogs.</param>
        /// <param name="azureService">Link to Azure service Proxy.</param>
        public LoginViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }

        /// <summary>
        /// Sets the login service.
        /// </summary>
        /// <value>The login service.</value>
        public ILoginService LoginService
        {
            set { _loginService = value; }
        }

        /// <summary>
        /// Gets the login command.
        /// </summary>
        /// <value>The login command.</value>
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand =
                    new MvxCommand(async () => { await LoginAndGo(); }));
            }
        }

        /// <summary>
        /// Checks for network connectivity, launches the user login, and if successful navigates to the
        /// appropriate start page based on the user's role (manager or not). Note also that we're using a
        /// presentation bundle in order to control clearing the navigation stack when showing the post-login
        /// page [so we don't end up with a Back button in iOS].
        /// </summary>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">Login Service must be set.</exception>
        public async Task LoginAndGo()
        {
            if (_loginService == null)
            {
                throw new ArgumentException("Login Service must be set.");
            }

            if (!NetworkService.IsConnected)
            {
                await
                    UserDialogs.AlertAsync("Device not connected to network, cannot login. Please try again later",
                        "Network Error");
                return;
            }

            try
            {
                var service = _azureService.MobileService;

                await _loginService.LoginAsync().ConfigureAwait(true);
                var profile = await LoadProfileAsync(service);
                UserContext.UserProfile = profile;

                var clearStackBundle = new MvxBundle(new Dictionary<string, string> { {PresentationBundleFlagKeys.ClearStack, "" } });

                if (profile.Manager)
                {
                    ShowViewModel<DashboardViewModel>(presentationBundle: clearStackBundle);
                }
                else
                {
                    ShowViewModel<WorkerQueueViewModel>(profile.UserId, clearStackBundle);
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

        /// <summary>
        /// Load the user profile as an asynchronous operation.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>Task&lt;UserProfile&gt;.</returns>
        private async Task<UserProfile> LoadProfileAsync(IMobileServiceClient service)
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