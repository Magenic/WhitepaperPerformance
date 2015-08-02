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

namespace Xamarin.IncidentApp.ViewModels
{
    public class AddIncidentViewModel : BaseViewModel
    {
        private IAzureService _azureService;
        private IMediaService _mediaService;

        public AddIncidentViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }

        public void Init()
        {
            Task.Run(async () => await LoadWorkersAsync());
        }


        public void SetActivityServices(IMediaService takePhotoService)
        {
            _mediaService = takePhotoService;
        }

        private async Task LoadWorkersAsync()
        {
            SetWorkers(await _azureService.MobileService.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null));
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

        private string _assignedToId;
        public string AssignedToId
        {
            get { return _assignedToId; }
            set
            {
                _assignedToId = value;
                RaisePropertyChanged(() => AssignedToId);
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        private byte[] _audioRecording;
        public byte[] AudioRecording
        {
            get { return _audioRecording; }
            set
            {
                _audioRecording = value;
                RaisePropertyChanged(() => AudioRecording);
            }
        }

        private IList<UserProfile> _workers;
        public IList<UserProfile> Workers
        {
            get { return _workers; }

        }

        private void SetWorkers(IList<UserProfile> workers)
        {
            _workers = workers;
            RaisePropertyChanged(() => Workers);
        }

        private ICommand _takeNewPhotoCommand;
        public ICommand TakeNewPhotoCommand
        {
            get
            {
                return _takeNewPhotoCommand ?? (_takeNewPhotoCommand = new MvxRelayCommand(() =>
                {
                    TakeNewPhoto();
                }));
            }
        }

        private ICommand _selectPhotoCommand;
        public ICommand SelectPhotoCommand
        {
            get
            {
                return _selectPhotoCommand ?? (_selectPhotoCommand = new MvxRelayCommand(() =>
                {
                    SelectPhoto();
                }));
            }
        }

        private ICommand _recordAudioCommand;
        public ICommand RecordAudioCommand
        {
            get
            {
                return _recordAudioCommand ?? (_recordAudioCommand = new MvxRelayCommand(() =>
                {
                    RecordAudio();
                }));
            }
        }

        private ICommand _saveNewIncidentCommand;
        public ICommand SaveNewIncidentCommand
        {
            get
            {
                return _saveNewIncidentCommand ?? (_saveNewIncidentCommand = new MvxRelayCommand(async () =>
                {
                    await SaveNewIncidentAsync();
                }));
            }
        }

        private ICommand _playAudioCommand;
        public ICommand PlayAudioCommand
        {
            get
            {
                return _playAudioCommand ?? (_playAudioCommand = new MvxRelayCommand(async () =>
                {
                    PlayAudio();
                }));
            }
        }

        private void PlayAudio()
        {
            if (_mediaService != null)
            {
                if (AudioRecording == null || AudioRecording.Length == 0)
                {
                    UserDialogs.Alert("No audio recording to play", "Playback Error");
                }
                _mediaService.PlayAudio(AudioRecording);
            }
        }

        private void RecordAudio()
        {
            if (_mediaService != null)
            {
                _mediaService.AudioComplete -= AudioComplete;
                _mediaService.AudioComplete += AudioComplete;
                _mediaService.RecordAudio();
            }
        }

        private void SelectPhoto()
        {
            if (_mediaService != null)
            {
                _mediaService.PhotoComplete -= PhotoComplete;
                _mediaService.PhotoComplete += PhotoComplete;
                _mediaService.SelectPhoto();
            }
        }

        private void TakeNewPhoto()
        {
            if (_mediaService != null)
            {
                _mediaService.PhotoComplete -= PhotoComplete;
                _mediaService.PhotoComplete += PhotoComplete;
                _mediaService.TakePhoto();
            }
        }

        private void PhotoComplete(object source, EventArgs.PhotoCompleteEventArgs e)
        {
            if (e.ImageStream != null && e.ImageStream.Length > 0)
            {
                Image = e.ImageStream;
            }
        }

        private void AudioComplete(object source, EventArgs.AudioCompleteEventArgs e)
        {
            if (e.AudioStream != null && e.AudioStream.Length > 0)
            {
                Image = e.AudioStream;
            }
        }

        private async Task SaveNewIncidentAsync()
        {

            if (!NetworkService.IsConnected)
            {
                await
                    UserDialogs.AlertAsync("Device not connected to network, cannot login. Please try again later",
                        "Network Error");
                return;
            }

            try
            {
                UserDialogs.ShowLoading("Saving new Incident...");
                await SaveIncidentDataAsync();
            }
            finally
            {
                UserDialogs.HideLoading();
            }

            Close(this);
        }

        private async Task SaveIncidentDataAsync()
        {
// First save the image/audio
            string imagePath = string.Empty;
            if (Image != null && Image.Length > 0)
            {
                imagePath = await _azureService.SaveBlobAsync(Image, "png");
            }


            string audioPath = string.Empty;
            if (AudioRecording != null && AudioRecording.Length > 0)
            {
                audioPath = await _azureService.SaveBlobAsync(AudioRecording, "wav");
            }

            var newIncident = new Incident
            {
                Subject = Subject,
                AssignedToId = AssignedToId,
                Description = Description,
                ImageLink = imagePath,
                AudioLink = audioPath,
                Closed = false,
                DateOpened = DateTime.MinValue,
                DateClosed = DateTime.MinValue
            };

            await _azureService.MobileService.GetTable<Incident>().InsertAsync(newIncident);
        }
    }
}