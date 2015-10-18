using System;
using System.Collections.Generic;
using System.IO;
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
        private IMediaService _mediaService;

        public DisplayIncidentViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
            IncidentDetails = new List<DisplayIncidentDetailViewModel>();
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
            Task.Run(async () => await LoadIncidentAsync());
        }

        internal IMediaService MediaService
        {
            get { return _mediaService; }
        }

        public async Task LoadIncidentAsync()
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
                        Closed = incident.Closed;

                        var users = await _azureService.MobileService.InvokeApiAsync<IList<UserProfile>>("AllUserList", HttpMethod.Get, null);
                        FullName = users.Single(w => w.UserId == incident.AssignedToId).FullName;

                        var details = await _azureService.MobileService.GetTable<IncidentDetail>()
                            .Where(r => r.IncidentId == _incidentId)
                            .OrderBy(r => r.DateEntered)
                            .ToListAsync();

                        IncidentDetails = details.Select(detail => new DisplayIncidentDetailViewModel(NetworkService, UserDialogs, detail, users.Single(u => u.UserId == detail.DetailEnteredById).FullName, this)).ToList();
                    }
                    finally
                    {
                        UserDialogs.HideLoading();
                    }
                }
            }
        }

        public void SetActivityServices(IMediaService takePhotoService)
        {
            _mediaService = takePhotoService;
        }
        
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            protected set
            {
                _subject = value;
                RaisePropertyChanged(() => Subject);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            protected set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            protected set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        private string _imageLink;
        public string ImageLink
        {
            get { return _imageLink; }
            protected set
            {
                if (_imageLink != value)
                {
                    _imageLink = value;
                    RaisePropertyChanged(() => ImageLink);
                }
            }
        }

        private string _audioRecordingLink;
        public string AudioRecordingLink
        {
            get { return _audioRecordingLink; }
            protected set
            {
                _audioRecordingLink = value;
                RaisePropertyChanged(() => AudioRecordingLink);
                AudioRecordingFileExtension = Path.GetExtension(_audioRecordingLink);
                Task.Run(async () => AudioRecordingBytes = await BinaryHandling.LoadBytesFromUrlAsync(_audioRecordingLink));
            }
        }

        private byte[] _audioRecordingBytes;
        public byte[] AudioRecordingBytes
        {
            get { return _audioRecordingBytes; }
            protected set
            {
                _audioRecordingBytes = value;
                RaisePropertyChanged(() => AudioRecordingBytes);
            }
        }

        private string _audioRecordingFileExtension;
        public string AudioRecordingFileExtension
        {
            get { return _audioRecordingFileExtension; }
            protected set
            {
                _audioRecordingFileExtension = value;
                RaisePropertyChanged(() => AudioRecordingBytes);
            }
        }
        
        private DateTime _dateOpened;
        public DateTime DateOpened
        {
            get { return _dateOpened; }
            protected set
            {
                _dateOpened = value;
                RaisePropertyChanged(() => DateOpened);
            }
        }

        private bool _closed;
        public bool Closed
        {
            get { return _closed; }
            protected set
            {
                _closed = value;
                RaisePropertyChanged(() => Closed);
            }
        }

        private IList<DisplayIncidentDetailViewModel> _incidentDetails;
        public IList<DisplayIncidentDetailViewModel> IncidentDetails
        {
            get { return _incidentDetails; }
            protected set
            {
                _incidentDetails = value;
                RaisePropertyChanged(() => IncidentDetails);
                RaisePropertyChanged(() => CurrentViewModel);
            }
        }

        public DisplayIncidentViewModel CurrentViewModel
        {
            get { return this; }
        }


        private ICommand _closeIncidentCommand;
        public ICommand CloseIncidentCommand
        {
            get
            {
                return _closeIncidentCommand ?? (_closeIncidentCommand = new MvxRelayCommand(async() => await CloseIncidentAsync()));
            }
        }

        private ICommand _addIncidentDetailCommand;
        public ICommand AddIncidentDetailCommand
        {
            get
            {
                return _addIncidentDetailCommand ?? (_addIncidentDetailCommand = new MvxRelayCommand(() => AddIncidentDetail()));
            }
        }

        private void AddIncidentDetail()
        {
            ShowViewModel<AddIncidentDetailViewModel>(new { incidentId = _incidentId });
        }

        private ICommand _playAudioCommand;
        public ICommand PlayAudioCommand
        {
            get
            {
                return _playAudioCommand ?? (_playAudioCommand = new MvxRelayCommand(PlayAudio));
            }
        }

        private void PlayAudio()
        {
            if (_mediaService != null)
            {
                if (AudioRecordingBytes == null || AudioRecordingBytes.Length == 0)
                {
                    UserDialogs.Alert("No audio recording to play", "Playback Error");
                }
                else
                {
                    _mediaService.PlayAudio(AudioRecordingBytes, AudioRecordingFileExtension);
                }
            }
        }

        private async Task CloseIncidentAsync()
        {
            if (CheckNetworkConnection())
            {
                var incidents = await _azureService.MobileService.GetTable<Incident>()
                    .Where(r => r.Id == _incidentId).ToListAsync();

                var incident = incidents.Single();
                incident.Closed = true;
                incident.DateClosed = DateTime.UtcNow;
                await _azureService.MobileService.GetTable<Incident>().UpdateAsync(incident);

                Close(this);
            }
        }
    }
}