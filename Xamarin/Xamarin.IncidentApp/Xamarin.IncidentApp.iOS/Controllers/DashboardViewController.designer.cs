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
	[Register ("DashboardViewController")]
	partial class DashboardViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Xamarin.IncidentApp.iOS.TeamUITableView TeamTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TeamTableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TeamTableView != null) {
				TeamTableView.Dispose ();
				TeamTableView = null;
			}
			if (TeamTableView != null) {
				TeamTableView.Dispose ();
				TeamTableView = null;
			}
		}
	}
}
