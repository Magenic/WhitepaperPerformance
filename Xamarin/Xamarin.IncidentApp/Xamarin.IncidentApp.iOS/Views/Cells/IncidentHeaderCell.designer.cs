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
	[Register ("IncidentHeaderCell")]
	partial class IncidentHeaderCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgIncidentImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblOwner { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblSubject { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imgIncidentImage != null) {
				imgIncidentImage.Dispose ();
				imgIncidentImage = null;
			}
			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
			if (lblOwner != null) {
				lblOwner.Dispose ();
				lblOwner = null;
			}
			if (lblSubject != null) {
				lblSubject.Dispose ();
				lblSubject = null;
			}
		}
	}
}
