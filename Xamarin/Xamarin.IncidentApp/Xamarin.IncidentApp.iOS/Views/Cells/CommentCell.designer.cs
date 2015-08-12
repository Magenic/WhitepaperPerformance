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
		UIImageView imgCommentImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblComment { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblOwnerInfo { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imgCommentImage != null) {
				imgCommentImage.Dispose ();
				imgCommentImage = null;
			}
			if (lblComment != null) {
				lblComment.Dispose ();
				lblComment = null;
			}
			if (lblOwnerInfo != null) {
				lblOwnerInfo.Dispose ();
				lblOwnerInfo = null;
			}
		}
	}
}
