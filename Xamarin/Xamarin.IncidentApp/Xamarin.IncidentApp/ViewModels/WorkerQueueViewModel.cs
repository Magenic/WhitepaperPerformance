using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.Commands;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Models;

namespace Xamarin.IncidentApp.ViewModels
{
    public class WorkerQueueViewModel : BaseViewModel
    {
        private string _userId;
        private ObservableCollection<WorkerQueueItemViewModel> _incidents;
        private bool _showClosed;
        private IAzureService _azureService;

        public WorkerQueueViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }
        
        public void Init(string userId)
        {
            _userId = userId;
             Task.Run(async () => await RefeshIncidentListAsync());
        }

        public ObservableCollection<WorkerQueueItemViewModel> Incidents
        {
            get { return _incidents; }
            set
            {
                _incidents = value;
                RaisePropertyChanged(() => Incidents);
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        public bool ShowClosed
        {
            set
            {
                if (_showClosed != value)
                {
                    _showClosed = value;
                    Task.Run(async () => await RefeshIncidentListAsync());                    
                }
            }
        }

        public async Task RefeshIncidentListAsync()
        {
            var service = _azureService.MobileService;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    IList<Incident> incidents;
                    try
                    {
                        UserDialogs.ShowLoading("Retrieving Worker Queue...");
                        var returnValue = new ObservableCollection<WorkerQueueItemViewModel>();

                        incidents = await _azureService.MobileService.GetTable<Incident>()
                            .Where(r => r.AssignedToId == _userId && r.Closed == _showClosed)
                            .Take(1000)
                            .ToListAsync();

                        var workers = await service.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null);
                        FullName = workers.Single(w => w.UserId == _userId).FullName;

                        foreach (var incident in incidents)
                        {
                            returnValue.Add(new WorkerQueueItemViewModel(NetworkService, UserDialogs, incident, FullName));
                        }
                        Incidents = returnValue;
                    }
                    finally
                    {
                        UserDialogs.HideLoading();
                    }
                }
            }
        }

        private ICommand _showIncidentItemCommand;
        public ICommand ShowIncidentItemCommand
        {
            get
            {
                return _showIncidentItemCommand ?? (_showIncidentItemCommand = new MvxRelayCommand<WorkerQueueItemViewModel>(ShowIncidentItem));
            }
        }

        private void ShowIncidentItem(WorkerQueueItemViewModel incident)
        {
            ShowViewModel<DisplayIncidentViewModel>(new { incidentId = incident.Id });
        }
    }
}
