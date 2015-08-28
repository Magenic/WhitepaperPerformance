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
	[Register ("AddIncidentViewController")]
	partial class AddIncidentViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAudioNote { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRemoveAudio { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRemoveImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSaveIncident { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgPhoto { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView pkrAssigned { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtSubject { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAudioNote != null) {
				btnAudioNote.Dispose ();
				btnAudioNote = null;
			}
			if (btnRemoveAudio != null) {
				btnRemoveAudio.Dispose ();
				btnRemoveAudio = null;
			}
			if (btnRemoveImage != null) {
				btnRemoveImage.Dispose ();
				btnRemoveImage = null;
			}
			if (btnSaveIncident != null) {
				btnSaveIncident.Dispose ();
				btnSaveIncident = null;
			}
			if (imgPhoto != null) {
				imgPhoto.Dispose ();
				imgPhoto = null;
			}
			if (pkrAssigned != null) {
				pkrAssigned.Dispose ();
				pkrAssigned = null;
			}
			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}
			if (txtSubject != null) {
				txtSubject.Dispose ();
				txtSubject = null;
			}
		}
	}
}
