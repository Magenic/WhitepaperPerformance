using System;
using Cirrious.MvvmCross.ViewModels;
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