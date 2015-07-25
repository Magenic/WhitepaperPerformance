using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Models;

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
            await RefreshDashboardAsync();
        }

        public async Task RefreshDashboardAsync()
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

        private ICommand _addIncidentCommand;
        public ICommand AddIncidentCommand
        {
            get
            {
                return _addIncidentCommand ?? (_addIncidentCommand = new MvxCommand(() =>
                {
                    AddIncident();
                }));
            }
        }

        private void AddIncident()
        {
            ShowViewModel<AddIncidentViewModel>();
        }

        public int MaxOpenIncidents
        {
            get
            {
                if (UserStautes == null || UserStautes.Count == 0)
                {
                    return 0;
                }
                return UserStautes.Max(u => u.TotalOpenIncidents);
            }
        }

        public int MaxClosedIncidents
        {
            get
            {
                if (UserStautes == null || UserStautes.Count == 0)
                {
                    return 0;
                }
                return UserStautes.Max(u => u.TotalCompleteIncidentsPast30Days);
            }
        }
    }
}
