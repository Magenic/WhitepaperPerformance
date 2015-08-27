using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Plugins.Messenger;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class AddIncidentDetailViewModel : BaseViewModel
    {
        private IAzureService _azureService;
        private IMediaService _mediaService;
        private IMvxMessenger _messenger;
        private string _incidentId;

        public AddIncidentDetailViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService, IMvxMessenger messenger)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
            _messenger = messenger;
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
            _messenger.Subscribe<RecordingCompleteMessage>(RecordingComplete);
        }

        private string _detailText;
        public string DetailText
        {
            get { return _detailText; }
            set
            {
                _detailText = value;
                RaisePropertyChanged(() => DetailText);
            }
        }

        public void SetActivityServices(IMediaService mediaService)
        {
            _mediaService = mediaService;
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

        private string _audioRecordingFileExtension;
        public string AudioRecordingFileExtension
        {
            get { return _audioRecordingFileExtension; }
            set
            {
                _audioRecordingFileExtension = value;
                RaisePropertyChanged(() => AudioRecordingFileExtension);
            }
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
                    await SaveNewIncidentDetailAsync();
                }));
            }
        }

        private ICommand _playAudioCommand;
        public ICommand PlayAudioCommand
        {
            get
            {
                return _playAudioCommand ?? (_playAudioCommand = new MvxRelayCommand(() =>
                {
                    PlayAudio();
                }));
            }
        }

        private ICommand _removeImageCommand;
        public ICommand RemoveImageCommand
        {
            get
            {
                return _removeImageCommand ?? (_removeImageCommand = new MvxRelayCommand(async () =>
                {
                    await RemoveImageAsync();
                }));
            }
        }

        private ICommand _removeAudioCommand;
        public ICommand RemoveAudioCommand
        {
            get
            {
                return _removeAudioCommand ?? (_removeAudioCommand = new MvxRelayCommand(async () =>
                {
                    await RemoveAudioAsync();
                }));
            }
        }

        private async Task RemoveAudioAsync()
        {
            var response = await UserDialogs.ConfirmAsync(new ConfirmConfig
            {
                Title = "Remove Audio Recording",
                Message = "Are you sure you want to remove this audio recording?"
            });
            if (response)
            {
                AudioRecording = null;
                AudioRecordingFileExtension = string.Empty;
            }
        }

        private async Task RemoveImageAsync()
        {
            var response = await UserDialogs.ConfirmAsync(new ConfirmConfig
            {
                Title = "Remove Image",
                Message = "Are you sure you want to remove this image?"
            });
            if (response)
            {
                Image = null;
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
                _mediaService.PlayAudio(AudioRecording, AudioRecordingFileExtension);
            }
        }

        private void RecordAudio()
        {
            if (_mediaService != null)
            {
                ShowViewModel<AudioRecorderViewModel>();
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

        private async Task SaveNewIncidentDetailAsync()
        {
            if (!NetworkService.IsConnected)
            {
                await UserDialogs.AlertAsync("Device not connected to network, cannot login. Please try again later",
                        "Network Error");
                return;
            }

            if (DetailText == string.Empty && (Image == null || Image.Length == 0) && (AudioRecording == null || AudioRecording.Length == 0))
            {
                await UserDialogs.AlertAsync("An incident must at least have a subject and image to be saved.",
                "Validation Error");
                return;
            }

            try
            {
                UserDialogs.ShowLoading("Saving Incident Comment...");

                await SaveIncidentDetailDataAsync();
            }
            finally
            {
                UserDialogs.HideLoading();
            }

            Close(this);
        }

        private async Task SaveIncidentDetailDataAsync()
        {
            if (CheckNetworkConnection())
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
                    audioPath = await _azureService.SaveBlobAsync(AudioRecording, AudioRecordingFileExtension);
                }

                var newIncidentDetail = new IncidentDetail
                {
                    IncidentId = _incidentId,
                    DetailText = DetailText,
                    AudioLink = audioPath,
                    DateEntered = DateTime.UtcNow,
                    DetailEnteredById = UserContext.UserProfile.UserId,
                    ImageLink = imagePath
                };

                await _azureService.MobileService.GetTable<IncidentDetail>().InsertAsync(newIncidentDetail);
            }
        }

        public void RecordingComplete(RecordingCompleteMessage message)
        {
            AudioRecording = message.Recording;
            AudioRecordingFileExtension = message.FileExtension;
        }
    }
}
