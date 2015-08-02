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

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
	[Register ("TeamStatusCell")]
	partial class TeamStatusCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblAvgWaitTitle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblCompleted { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblIncidentTitle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTeamLead { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblWaitTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIProgressView pvCompletedBar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIProgressView pvWaitTimeBar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblAvgWaitTitle != null) {
				lblAvgWaitTitle.Dispose ();
				lblAvgWaitTitle = null;
			}
			if (lblCompleted != null) {
				lblCompleted.Dispose ();
				lblCompleted = null;
			}
			if (lblIncidentTitle != null) {
				lblIncidentTitle.Dispose ();
				lblIncidentTitle = null;
			}
			if (lblTeamLead != null) {
				lblTeamLead.Dispose ();
				lblTeamLead = null;
			}
			if (lblWaitTime != null) {
				lblWaitTime.Dispose ();
				lblWaitTime = null;
			}
			if (pvCompletedBar != null) {
				pvCompletedBar.Dispose ();
				pvCompletedBar = null;
			}
			if (pvWaitTimeBar != null) {
				pvWaitTimeBar.Dispose ();
				pvWaitTimeBar = null;
			}
		}
	}
}
