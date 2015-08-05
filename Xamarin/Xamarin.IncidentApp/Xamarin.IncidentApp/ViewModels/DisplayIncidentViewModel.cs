using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class DisplayIncidentViewModel : BaseViewModel
    {
        private string _incidentId;
        
        public DisplayIncidentViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
        }
    }
}