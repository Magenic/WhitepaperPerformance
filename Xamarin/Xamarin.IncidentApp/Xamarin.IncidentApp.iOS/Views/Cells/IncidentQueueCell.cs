using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("IncidentQueueCell")]
    public partial class IncidentQueueCell : MvxTableViewCell
    {
        public static readonly NSString Identifier = new NSString("IncidentQueueCell");

        public IncidentQueueCell()
        {
            InitializeBindings();
        }

        public IncidentQueueCell(IntPtr handle) : base(handle)
        {
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {

            });
        }

    }
}