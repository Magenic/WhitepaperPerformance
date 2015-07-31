using System.Collections.Generic;
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
        private IAzureServices _azureService;

        public AddIncidentViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureServices azureService)
            : base(networkService, userDialogs)
        {
            _azureService = azureService;
        }

        public void Init()
        {
            Task.Run(async () => await LoadWorkersAsync());
        }

        private async Task LoadWorkersAsync()
        {
            Workers = await _azureService.MobileService.InvokeApiAsync<IList<UserProfile>>("WorkerList", HttpMethod.Get, null);
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
            private set
            {
                _workers = value;
                RaisePropertyChanged(() => Workers);
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

        private async Task SaveNewIncidentAsync()
        {
            // First save the image/audio
            string imagePath = string.Empty;
            if (Image.Length > 0)
            {
                imagePath = await _azureService.SaveBlobAsync(Image, "png");
            }

            string audioPath =  string.Empty;
            if (AudioRecording.Length > 0)
            {
                audioPath = await _azureService.SaveBlobAsync(AudioRecording, "wav");
            }

            var newIncident = new Incident
            {
                Subject = Subject,
                AssignedToId = AssignedToId,
                Description = Description,
                ImageLink = imagePath,
                AudioLink = audioPath
            };

            var incidents = _azureService.MobileService.GetTable<Incident>();
            await incidents.InsertAsync(newIncident);
        }
    }
}