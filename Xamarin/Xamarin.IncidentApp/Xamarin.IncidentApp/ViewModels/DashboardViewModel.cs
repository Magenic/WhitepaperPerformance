using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }

        public string Title
        {
            get { return "Team Performance";  }
        }

        private  IList<UserStatus> _userStatuses ;
        public IList<UserStatus> UserStatuses
        {
            get { return _userStatuses; }
            private set
            {
                _userStatuses = value;
                RaisePropertyChanged(() => UserStatuses);
            }
        }

        public async void Init()
        {
            await RefreshDashboardAsync();
        }

        public async Task RefreshDashboardAsync()
        {
            var service = MobileService.Service;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    UserStatuses = await service.InvokeApiAsync<IList<UserStatus>>("StatusList", HttpMethod.Get, null);                        
                }
            }
        }

        private MvxCommand<UserStatus> _itemSelectedCommand;

        public ICommand ItemSelectedCommand
        {
            get
            {
                _itemSelectedCommand = _itemSelectedCommand ?? new MvxCommand<UserStatus>(DoSelectItem);
                return _itemSelectedCommand;
            }
        }

        public void DoSelectItem(UserStatus userStatus)
        {
            ShowViewModel<WorkerQueueViewModel>(new {userId = userStatus.User.UserId});
        }
    }
}
