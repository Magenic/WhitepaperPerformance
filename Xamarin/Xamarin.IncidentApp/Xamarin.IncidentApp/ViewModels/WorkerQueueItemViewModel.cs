using System;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class WorkerQueueItemViewModel : BaseViewModel
    {
        public WorkerQueueItemViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
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

        private string _imageLink;
        public string ImageLink
        {
            get { return _imageLink; }
            set
            {
                _imageLink = value;
                RaisePropertyChanged(() => ImageLink);
            }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
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
    }
}