using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.iOS.Services;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    [MvxViewFor(typeof(LoginViewModel))]
    partial class LoginViewController : BaseViewController
    {
        public LoginViewController()
        {
        }

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            this.ViewModel.LoginService = new LoginService(this, Mvx.Resolve<IAzureService>());

            NavigationController.NavigationBarHidden = true;
            this.CreateBinding(btnLogin).To((LoginViewModel vm) => vm.LoginCommand).Apply();
        }
    }
}