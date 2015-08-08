﻿using System;
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
    public class AddIncidentDetailViewModel : BaseViewModel
    {
        private IAzureService _azureService;
        private IMediaService _mediaService;
        private string _incidentId;

        public AddIncidentDetailViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
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


        public void SetActivityServices(IMediaService takePhotoService)
        {
            _mediaService = takePhotoService;
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
                AudioRecording = e.AudioStream;
            }
        }

        private async Task SaveNewIncidentDetailAsync()
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

            Close(this);
        }
    }
}
