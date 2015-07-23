using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }
    }
}
