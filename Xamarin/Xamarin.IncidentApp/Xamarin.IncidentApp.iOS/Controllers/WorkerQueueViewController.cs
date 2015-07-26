using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    [MvxViewFor(typeof (WorkerQueueViewModel))]
    //[Register("WorkerQueueViewController")]    
    partial class WorkerQueueViewController : BaseViewController
    {
        public WorkerQueueViewController()
        {
        }

        public WorkerQueueViewController(IntPtr p) : base(p)
        {
        }
        
        public new WorkerQueueViewModel ViewModel
        {
            get { return (WorkerQueueViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}