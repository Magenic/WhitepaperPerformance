using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class WorkerQueueViewModel : BaseViewModel
    {
        private string _userId;
         
        public WorkerQueueViewModel(INetworkService networkService, IUserDialogs userDialogs)
            : base(networkService, userDialogs)
        {
        }
        
        public void Init(string userId)
        {
             _userId = userId;
        }

    }
}
