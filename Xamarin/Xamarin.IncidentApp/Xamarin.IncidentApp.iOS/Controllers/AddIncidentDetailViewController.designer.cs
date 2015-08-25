// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Xamarin.IncidentApp.iOS.Controllers
{
	[Register ("AddIncidentDetailViewController")]
	partial class AddIncidentDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSaveComment { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtComment { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnSaveComment != null) {
				btnSaveComment.Dispose ();
				btnSaveComment = null;
			}
			if (txtComment != null) {
				txtComment.Dispose ();
				txtComment = null;
			}
		}
	}
}
