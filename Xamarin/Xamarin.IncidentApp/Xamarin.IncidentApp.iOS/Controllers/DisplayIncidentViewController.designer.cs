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
		Xamarin.IncidentApp.iOS.IncidentCommentTableView CommentTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgGradient { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgIncidentImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblOwnerInfo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblSubject { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CommentTableView != null) {
				CommentTableView.Dispose ();
				CommentTableView = null;
			}
			if (imgGradient != null) {
				imgGradient.Dispose ();
				imgGradient = null;
			}
			if (imgIncidentImage != null) {
				imgIncidentImage.Dispose ();
				imgIncidentImage = null;
			}
			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}
			if (lblOwnerInfo != null) {
				lblOwnerInfo.Dispose ();
				lblOwnerInfo = null;
			}
			if (lblSubject != null) {
				lblSubject.Dispose ();
				lblSubject = null;
			}
		}
	}
}
