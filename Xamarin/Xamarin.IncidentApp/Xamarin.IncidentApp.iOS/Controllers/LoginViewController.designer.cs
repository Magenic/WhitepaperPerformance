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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnClicker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLoading { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnClicker != null) {
				btnClicker.Dispose ();
				btnClicker = null;
			}
			if (lblLoading != null) {
				lblLoading.Dispose ();
				lblLoading = null;
			}
		}
	}
}
