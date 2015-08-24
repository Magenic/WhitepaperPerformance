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
            this.ViewModel.LoginService = new LoginService(this, Mvx.Resolve<IAzureService>());

            NavigationController.NavigationBarHidden = true;
            this.CreateBinding(btnLogin).To((LoginViewModel vm) => vm.LoginCommand).Apply();
            this.txtEmailAddress.ShouldReturn += CloseKeyboard;
            this.txtPassword.ShouldReturn += CloseKeyboard;
        }

        private bool CloseKeyboard(UIKit.UITextField textField)
        {
            if (textField.Equals(txtEmailAddress))
            {
                txtPassword.BecomeFirstResponder();
                return true;
            }
            if (textField.Equals(txtPassword))
            {
                // validate field inputs as per your requirement
                textField.ResignFirstResponder();
                return true;
            }
            return true;
        }
    }
}