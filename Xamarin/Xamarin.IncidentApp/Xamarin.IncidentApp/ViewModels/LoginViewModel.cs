using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;

namespace Xamarin.IncidentApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel(INetworkService networkService, IUserDialogs userDialogs) : base(networkService, userDialogs)
        {
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new MvxCommand(() =>
                {
                    ShowViewModel<DashboardViewModel>();
                }));
            }
        }

    }
}
