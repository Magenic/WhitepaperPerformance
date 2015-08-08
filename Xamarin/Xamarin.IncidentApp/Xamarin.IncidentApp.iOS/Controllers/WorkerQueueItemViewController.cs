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
    [Register("WorkerQueueItemViewController")]    
    [MvxViewFor(typeof(WorkerQueueItemViewModel))]
    public class WorkerQueueItemViewController : BaseViewController
    {
        public WorkerQueueItemViewController(IntPtr p) : base(p)
        {
        }

        public WorkerQueueItemViewController()
        {
        }


    }
}