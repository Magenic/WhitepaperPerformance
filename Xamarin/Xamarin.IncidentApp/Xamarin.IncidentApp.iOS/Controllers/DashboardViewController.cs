using System;
using System.Collections.Generic;
using System.Text;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    [MvxViewFor(typeof(DashboardViewModel))]
    partial class DashboardViewController : BaseViewController
    {
        public DashboardViewController()
        {
        }

        public DashboardViewController(IntPtr handle)
                    : base(handle)
        {
        }
        public new DashboardViewModel ViewModel
        {
            get { return (DashboardViewModel)base.ViewModel; }
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
            NavigationController.NavigationBarHidden = false;
        }
    }
}
