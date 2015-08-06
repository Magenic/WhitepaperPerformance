using System.Collections.Generic;
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
        private IList<WorkerQueueItemViewModel> _incidents;
        private bool _showClosed = false;
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

        public IList<WorkerQueueItemViewModel> Incidents
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
                    try
                    {
                        UserDialogs.ShowLoading("Retrieving Worker Queue...");

                        var incidents = await _azureService.MobileService.GetTable<Incident>()
                            .Where(r => r.AssignedToId == _userId && r.Closed == _showClosed)
                            .ToListAsync();

                        var newIncidents = new List<WorkerQueueItemViewModel>();
                        foreach (var incident in incidents)
                        {
                            var newIncident = new WorkerQueueItemViewModel(NetworkService, UserDialogs)
                            {
                                DateOpened = incident.DateOpened,
                                Id = incident.Id,
                                ImageLink = incident.ImageLink,
                                Subject = incident.Subject,
                                UserId = incident.AssignedToId
                            };
                            newIncidents.Add(newIncident);
                        }
                        Incidents = newIncidents;

                        var workers = await service.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null);
                        FullName = workers.Single(w => w.UserId == _userId).FullName;
                        foreach (var incident in Incidents)
                        {
                            incident.FullName = FullName;
                        }
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
