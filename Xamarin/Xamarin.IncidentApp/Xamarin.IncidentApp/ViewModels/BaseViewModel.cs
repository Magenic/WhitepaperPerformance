using System;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


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

        protected bool CheckNetworkConnection()
        {
            if (!NetworkService.IsConnected)
            {
                UserDialogs.Alert(_noConnectionMessage);
                return false;
            }

            return true;
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