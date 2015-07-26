using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.Commands;
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

        private ObservableCollection<UserStatus> _userStatuses;
        public ObservableCollection<UserStatus> UserStatuses
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
            var service = Utilities.MobileService.Service;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    var userStatuses = await service.InvokeApiAsync<IList<UserStatus>>("StatusList", HttpMethod.Get, null);
                    var maxCompleted = MaxCompletedIncidents(userStatuses);
                    var maxWaitTime = MaxWaitTime(userStatuses);
                    foreach (var userStatus in userStatuses)
                    {
                        userStatus.MaxCompletedIncidents = maxCompleted;
                        userStatus.MaxWaitTime = maxWaitTime;
                    }
                    var collection = new ObservableCollection<UserStatus>(userStatuses);
                    UserStatuses = collection;
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

        private ICommand _showWorkerQueueCommand;
        public ICommand ShowWorkerQueueCommand
        {
            get
            {
                return _showWorkerQueueCommand ?? (_showWorkerQueueCommand = new MvxRelayCommand<UserStatus>(user =>
                {
                    ShowWorkerQueue(user.User.UserId);
                }));
            }
        }

        private void ShowWorkerQueue(string userId)
        {
            ShowViewModel<WorkerQueueViewModel>(userId);
        }

        private double MaxWaitTime(IList<UserStatus> userStatuses)
        {
            if (userStatuses == null || userStatuses.Count == 0)
            {
                return 0;
            }
            return userStatuses.Max(u => u.AvgWaitTimeOfOpenIncidents);
        }

        private int MaxCompletedIncidents(IList<UserStatus> userStatuses)
        {
            if (userStatuses == null || userStatuses.Count == 0)
            {
                return 0;
            }
            return userStatuses.Max(u => u.TotalCompleteIncidentsPast30Days);
        }
    }
}
