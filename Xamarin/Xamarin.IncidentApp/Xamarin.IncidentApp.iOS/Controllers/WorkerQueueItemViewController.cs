using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    //[Register("WorkerQueueItemViewController")]    
    [MvxViewFor(typeof(WorkerQueueItemViewModel))]
    public partial class WorkerQueueItemViewController : BaseViewController
    {
        private string _ownerName;
        private string _ownerInfo;

        public WorkerQueueItemViewController(IntPtr p) : base(p)
        {
        }

        public WorkerQueueItemViewController()
        {
        }

        public new WorkerQueueItemViewModel ViewModel
        {
            get { return (WorkerQueueItemViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupBindings();
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                this.lblOwnerInfo.Text = string.Concat(value, " - ", OwnerInfo);
                _ownerName = value;
            }
        }

        public string OwnerInfo
        {
            get { return _ownerInfo; }
            set
            {
                this.lblOwnerInfo.Text = string.Concat(OwnerName, " - ", value);
                _ownerInfo = value;
            }
        }

        private void SetupBindings()
        {
            this.CreateBinding().For(c => c.Title).To<WorkerQueueItemViewModel>(vm => vm.FullName).Apply();
            this.CreateBinding().For(f => f.lblSubject).To<WorkerQueueItemViewModel>(vm => vm.Subject).Apply();
            this.CreateBinding().For(c => c.OwnerName).To((WorkerQueueItemViewModel property) => property.FullName).Apply();
            this.CreateBinding().For(c => c.OwnerInfo).To((WorkerQueueItemViewModel property) => property.DateOpened.ToString()).Apply();
        }
    }
}