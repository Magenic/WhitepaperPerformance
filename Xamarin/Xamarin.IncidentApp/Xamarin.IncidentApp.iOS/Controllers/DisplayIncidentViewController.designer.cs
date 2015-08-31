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
	[Register ("DisplayIncidentViewController")]
	partial class DisplayIncidentViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Xamarin.IncidentApp.iOS.DisplayIncidentUITableView DisplayIncidentTableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DisplayIncidentTableView != null) {
				DisplayIncidentTableView.Dispose ();
				DisplayIncidentTableView = null;
			}
		}
	}
}
