using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    [Register("TeamCell")]
    public partial class TeamCell : MvxTableViewCell
    {
        public static readonly NSString Identifier = new NSString("TeamCell");
        private const string BindingText = "";

		public TeamCell (IntPtr handle) : base (handle)
		{
			InitializeBindings();
		}

        public TeamCell()
        {
			InitializeBindings();
		}

        //ToDo: Add Bindings here!
        private void InitializeBindings()
        {
            
        }

    }
}
