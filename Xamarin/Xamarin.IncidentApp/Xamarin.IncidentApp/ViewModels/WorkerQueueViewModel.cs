using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class WorkerQueueViewModel : BaseViewModel
    {
        public WorkerQueueViewModel(INetworkService networkService, IUserDialogs userDialogs)
            : base(networkService, userDialogs)
        {
        }
    }
}
