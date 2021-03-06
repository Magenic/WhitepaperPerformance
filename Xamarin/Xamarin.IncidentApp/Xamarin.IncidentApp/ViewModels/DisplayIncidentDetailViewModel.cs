﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.Commands;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DisplayIncidentDetailViewModel : BaseViewModel
    {
        private  WeakReference _parent;

        public DisplayIncidentDetailViewModel(INetworkService networkService, IUserDialogs userDialogs, IncidentDetail model, string fullName, DisplayIncidentViewModel parent)
            : base(networkService, userDialogs)
        {
            DetailText = model.DetailText;
            ImageLink = model.ImageLink;
            AudioRecordingLink = model.AudioLink;
            DateOpened = model.DateEntered;
            FullName = fullName;

            _parent = new WeakReference(parent);
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            private set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        private string _detailText;
        public string DetailText
        {
            get { return _detailText; }
            private set
            {
                _detailText = value;
                RaisePropertyChanged(() => DetailText);
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

        private void PlayAudio()
        {
            var mediaService = ((DisplayIncidentViewModel) _parent.Target).MediaService;
            if (mediaService != null)
            {
                if (AudioRecordingBytes == null || AudioRecordingBytes.Length == 0)
                {
                    UserDialogs.Alert("No audio recording to play", "Playback Error");
                }
                mediaService.PlayAudio(AudioRecordingBytes, AudioRecordingFileExtension);
            }
        }
    }
}