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

namespace Xamarin.IncidentApp.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel HelloLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (HelloLabel != null) {
				HelloLabel.Dispose ();
				HelloLabel = null;
			}
		}
	}
}
