using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IList<Incident> _incidents;
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

        public IList<Incident> Incidents
        {
            get { return _incidents; }
            set
            {
                _incidents = value;
                RaisePropertyChanged(() => Incidents);
            }
        }

        public bool ShowClosed
        {
            set
            {
                _showClosed = value;
                Task.Run(async () => await RefeshIncidentListAsync());
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

                        Incidents = await _azureService.MobileService.GetTable<Incident>()
                            .Where(r => r.AssignedToId == _userId && r.Closed == _showClosed)
                            .ToListAsync();
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
                return _showIncidentItemCommand ?? (_showIncidentItemCommand = new MvxRelayCommand<string>(ShowIncidentItem));
            }
        }
        
        private void ShowIncidentItem(string incidentId)
        {
            ShowViewModel<DisplayIncidentViewModel>(incidentId);
        }
    }
}
