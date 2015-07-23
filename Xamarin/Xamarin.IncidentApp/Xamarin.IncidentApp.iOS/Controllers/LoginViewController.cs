using System;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

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
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            NavigationController.NavigationBarHidden = true;
            this.CreateBinding(btnClicker).To((LoginViewModel vm) => vm.LoginCommand).Apply();

        }
    }
}