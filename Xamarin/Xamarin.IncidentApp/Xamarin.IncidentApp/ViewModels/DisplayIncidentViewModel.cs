using System;
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
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DisplayIncidentViewModel : BaseViewModel
    {
        private string _incidentId;
        private IAzureService _azureService;

        public DisplayIncidentViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
            Task.Run(async () => await LoadIncidentAsync());
        }

        private async Task LoadIncidentAsync()
        {
            var service = _azureService.MobileService;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    try
                    {
                        UserDialogs.ShowLoading("Retrieving Incident...");

                        var incidents = await _azureService.MobileService.GetTable<Incident>()
                            .Where(r => r.Id == _incidentId)
                            .ToListAsync();

                        var incident = incidents.Single();

                        ImageLink = incident.ImageLink;
                        Description = incident.Description;
                        Subject = incident.Subject;
                        AudioRecordingLink = incident.AudioLink;
                        DateOpened = incident.DateOpened;

                        var workers = await service.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null);
                        FullName = workers.Single(w => w.UserId == incident.AssignedToId).FullName;

                        IncidentDetails = await _azureService.MobileService.GetTable<IncidentDetail>()
                            .Where(r => r.IncidentId == _incidentId)
                            .ToListAsync();
                    }
                    finally
                    {
                        UserDialogs.HideLoading();
                    }
                }
            }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                RaisePropertyChanged(() => Subject);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
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

        private string _imageLink;
        public string ImageLink
        {
            get { return _imageLink; }
            set
            {
                if (_imageLink != value)
                {
                    _imageLink = value;
                    RaisePropertyChanged(() => ImageLink);
                    Task.Run(async () => ImageBytes = await BinaryHandling.LoadBytesFromUrlAsync(_imageLink));                    
                }
            }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set
            {
                _imageBytes = value;
                RaisePropertyChanged(() => ImageBytes);
            }
        }

        private string _audioRecordingLink;
        public string AudioRecordingLink
        {
            get { return _audioRecordingLink; }
            set
            {
                _audioRecordingLink = value;
                RaisePropertyChanged(() => AudioRecordingLink);
            }
        }

        private DateTime _dateOpened;
        public DateTime DateOpened
        {
            get { return _dateOpened; }
            set
            {
                _dateOpened = value;
                RaisePropertyChanged(() => DateOpened);
            }
        }

        private IList<IncidentDetail> _incidentDetails;
        public IList<IncidentDetail> IncidentDetails
        {
            get { return _incidentDetails; }
            set
            {
                _incidentDetails = value;
                RaisePropertyChanged(() => IncidentDetails);
            }
        }

        private ICommand _closeIncidentCommand;
        public ICommand CloseIncidentCommand
        {
            get
            {
                return _closeIncidentCommand ?? (_closeIncidentCommand = new MvxRelayCommand(async() => await CloseIncidentAsync()));
            }
        }

        private async Task CloseIncidentAsync()
        {
            var incidents = await _azureService.MobileService.GetTable<Incident>()
                .Where(r => r.Id == _incidentId).ToListAsync();

            var incident = incidents.Single();
            incident.Closed = true;
            await _azureService.MobileService.GetTable<Incident>().UpdateAsync(incident);
        }

    }
}