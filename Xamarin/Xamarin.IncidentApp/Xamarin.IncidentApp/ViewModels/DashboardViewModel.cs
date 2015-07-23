using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.Models;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }

        private  IList<UserStatus> _userStatuses ;
        public IList<UserStatus> UserStautes
        {
            get { return _userStatuses; }
            private set
            {
                _userStatuses = value;
                RaisePropertyChanged(() => UserStautes);
            }
        }

        public async void Init()
        {
            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    UserStautes = await service.InvokeApiAsync<IList<UserStatus>>("StatusList", HttpMethod.Get, null);                        
                }
            }
        }
    }
}
