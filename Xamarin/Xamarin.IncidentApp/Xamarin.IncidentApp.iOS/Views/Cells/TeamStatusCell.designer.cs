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
		UILabel lblCompleted { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTeamLead { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblWaitTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView uvCompletedBar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView uvWaitTime { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblCompleted != null) {
				lblCompleted.Dispose ();
				lblCompleted = null;
			}
			if (lblTeamLead != null) {
				lblTeamLead.Dispose ();
				lblTeamLead = null;
			}
			if (lblWaitTime != null) {
				lblWaitTime.Dispose ();
				lblWaitTime = null;
			}
			if (uvCompletedBar != null) {
				uvCompletedBar.Dispose ();
				uvCompletedBar = null;
			}
			if (uvWaitTime != null) {
				uvWaitTime.Dispose ();
				uvWaitTime = null;
			}
		}
	}
}
