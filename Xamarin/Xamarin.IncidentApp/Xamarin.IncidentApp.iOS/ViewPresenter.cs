using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Foundation;
using UIKit;

namespace Xamarin.IncidentApp.iOS
{
    public class ViewPresenter : MvxModalNavSupportTouchViewPresenter
    {
        public UIViewController RootController
        {
            get;
            private set;
        }

        public ViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
    }
}