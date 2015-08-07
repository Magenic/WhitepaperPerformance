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
	[Register ("WorkerQueueViewController")]
	partial class WorkerQueueViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Xamarin.IncidentApp.iOS.IncidentQueueUITableView IncidentQueueTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl sgOpenCloseFilter { get; set; }

		[Action ("sgOpenCloseFilter_ValueChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void sgOpenCloseFilter_ValueChanged (UISegmentedControl sender);

		void ReleaseDesignerOutlets ()
		{
			if (IncidentQueueTableView != null) {
				IncidentQueueTableView.Dispose ();
				IncidentQueueTableView = null;
			}
			if (sgOpenCloseFilter != null) {
				sgOpenCloseFilter.Dispose ();
				sgOpenCloseFilter = null;
			}
		}
	}
}
