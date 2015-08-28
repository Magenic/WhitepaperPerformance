using System;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Plugins.Messenger;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class AudioRecorderViewModel : BaseViewModel
    {
        private IMediaService _mediaService;
        private readonly IMvxMessenger _msg;
        private bool _Recording = false;

        public AudioRecorderViewModel(INetworkService networkService, IUserDialogs userDialogs) : this(networkService, userDialogs, null)
        {
        }

        public AudioRecorderViewModel(INetworkService networkService, IUserDialogs userDialogs, IMvxMessenger msg) : base(networkService, userDialogs)
        {
            _msg = Mvx.Resolve<IMvxMessenger>(); ;
        }

        public void SetActivityServices(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        private ICommand _startRecordingCommand;
        public ICommand StartRecordingCommand
        {
            get
            {
                return _startRecordingCommand ?? (_startRecordingCommand = new MvxRelayCommand(() =>
                {
                    if (!_Recording)
                    {
                        _Recording = true;
                        _mediaService.StartRecording();
                    }
                }));
            }
        }

        private ICommand _stopRecordingCommand;
        public ICommand StopRecordingCommand
        {
            get
            {
                return _stopRecordingCommand ?? (_stopRecordingCommand = new MvxRelayCommand(() =>
                {
                    if (_Recording)
                    {
                        _Recording = false;
                        _mediaService.StopRecording();
                    }
                }));
            }
        }

        private ICommand _playRecordingCommand;
        public ICommand PlayRecordingCommand
        {
            get
            {
                return _playRecordingCommand ?? (_playRecordingCommand = new MvxRelayCommand(() =>
                {
                    if (_Recording)
                    {
                        UserDialogs.Alert("Cannot play while recording", "Play Error", "Ok");
                        return;
                    }
                    _mediaService.PlayAudio(_mediaService.GetRecording(), _mediaService.GetRecordingFileExtension());
                }));
            }
        }

        private ICommand _returnResultCommand;
        public ICommand ReturnResultCommand
        {
            get
            {
                return _returnResultCommand ?? (_returnResultCommand = new MvxRelayCommand(() =>
                {
                    ReturnAudioInformation();
                }));
            }
        }

        private void ReturnAudioInformation()
        {
            if ((_mediaService.GetRecording() == null || _mediaService.GetRecording().Length == 0) ||
                string.IsNullOrEmpty(_mediaService.GetRecordingFileExtension()))
            {
                UserDialogs.Alert("No Recording to Return.");
            }
            var message = new RecordingCompleteMessage(this)
            {
                FileExtension = _mediaService.GetRecordingFileExtension(),
                Recording = _mediaService.GetRecording()
            };
            _msg.Publish(message);
            Close(this);
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new MvxRelayCommand(() =>
                {
                    Close(this);
                }));
            }
        }

    }
}
