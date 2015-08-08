using System;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DisplayIncidentDetailViewModel : BaseViewModel
    {
        public DisplayIncidentDetailViewModel(INetworkService networkService, IUserDialogs userDialogs, IncidentDetail model, string fullName)
            : base(networkService, userDialogs)
        {
            DetailText = model.DetailText;
            ImageLink = model.ImageLink;
            AudioRecordingLink = model.AudioLink;
            DateOpened = model.DateEntered;
            FullName = fullName;
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
                    Task.Run(async () => ImageBytes = await BinaryHandling.LoadBytesFromUrlAsync(_imageLink));
                }
            }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            protected set
            {
                _imageBytes = value;
                RaisePropertyChanged(() => ImageBytes);
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

    }
}