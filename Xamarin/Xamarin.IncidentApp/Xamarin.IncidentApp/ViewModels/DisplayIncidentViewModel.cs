using System.Linq;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
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

                        var indicent = incidents.Single();

                        ImageLink = indicent.ImageLink;

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

        private byte[] _audioRecordingLink;
        public byte[] AudioRecordingLink
        {
            get { return _audioRecordingLink; }
            set
            {
                _audioRecordingLink = value;
                RaisePropertyChanged(() => AudioRecordingLink);
            }
        }
    }
}