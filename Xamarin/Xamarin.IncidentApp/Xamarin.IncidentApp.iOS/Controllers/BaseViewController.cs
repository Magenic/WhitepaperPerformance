using System;
using System.Diagnostics;
using Cirrious.MvvmCross.Touch.Views;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    public abstract class BaseViewController : MvxViewController
    {
        public BaseViewController(IntPtr p) : base(p) { }
        public BaseViewController() : base() { }

        public override void ViewDidLoad()
        {
            try
            {
                base.ViewDidLoad();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}