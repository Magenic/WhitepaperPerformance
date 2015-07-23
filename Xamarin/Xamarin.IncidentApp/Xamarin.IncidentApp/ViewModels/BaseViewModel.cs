using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private readonly string _noConnectionMessage = "No Network Connection";

        protected INetworkService NetworkService { get; set; }
        protected IUserDialogs UserDialogs { get; set; }

        public BaseViewModel(INetworkService networkService, IUserDialogs userDialogs)
        {
            NetworkService = networkService;
            UserDialogs = userDialogs;
        }

        protected new bool Close(IMvxViewModel viewModel)
        {
            return ChangePresentation(new MvxClosePresentationHint(viewModel));
        }

        protected void HideDialog(IProgressDialog dialog)
        {
            try
            {
                dialog.Hide();
            }
            catch (Exception)
            {
                //todo: needs to be refactored to catch correct exception.
                //not handled on purpose, can crash during app rotation
            }
        }

        public bool IsConnected()
        {
            return NetworkService.IsConnected;
        }

        public void ShowNoConnectionDialog()
        {
            UserDialogs.Alert(_noConnectionMessage);
        }

        protected async Task CheckNetworkConnectionAsync(Func<Task> taskToExecute)
        {
            if (!NetworkService.IsConnected)
            {
                UserDialogs.Alert(_noConnectionMessage);
                return;
            }

            await taskToExecute().ConfigureAwait(false);
        }

        protected void CheckIfTokenExpired(HttpRequestException e)
        {
            const string unauthorizedMessage = "401 (Unauthorized)";

            if (e.Message == unauthorizedMessage)
            {
                ShowViewModel<LoginViewModel>();
            }
        }
    }
}
