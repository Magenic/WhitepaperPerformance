using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.ViewModels
{
    public class AddIncidentDetailViewModel : BaseViewModel
    {
        private string _incidentId;

        public AddIncidentDetailViewModel(INetworkService networkService, IUserDialogs userDialogs)
            : base(networkService, userDialogs)
        {
        }

        public void Init(string incidentId)
        {
            _incidentId = incidentId;
        }
    }
}
