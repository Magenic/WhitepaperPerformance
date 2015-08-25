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
	[Register ("CommentCell")]
	partial class CommentCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlayAudio { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Xamarin.IncidentApp.iOS.Views.UrlImageView imgCommentImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblComment { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblOwner { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnPlayAudio != null) {
				btnPlayAudio.Dispose ();
				btnPlayAudio = null;
			}
			if (imgCommentImage != null) {
				imgCommentImage.Dispose ();
				imgCommentImage = null;
			}
			if (lblComment != null) {
				lblComment.Dispose ();
				lblComment = null;
			}
			if (lblOwner != null) {
				lblOwner.Dispose ();
				lblOwner = null;
			}
		}
	}
}
