using System;
using System.Diagnostics;
using AVFoundation;
using Foundation;
using StreamingAudio;
using UIKit;
using Xamarin.IncidentApp.EventArgs;
using Xamarin.IncidentApp.iOS.Controllers;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.iOS.Services
{
    public class MediaService : IMediaService
    {
        public event AudioCompleteEventHandler AudioComplete;
        public event PhotoCompleteEventHandler PhotoComplete;

        public static AVAudioPlayer Player;
        public static AVAudioRecorder Recorder;

        private UIImagePickerController _imagePicker;
        private BaseViewController _controller;

        public MediaService(BaseViewController controller)
        {
            _controller = controller;

            // create a new picker controller
            _imagePicker = new UIImagePickerController();
            _imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            _imagePicker.Canceled += Handle_Canceled;

        }

        public void PlayAudio(byte[] audioRecording)
        {
            var session = AVAudioSession.SharedInstance();
            if (session == null || audioRecording == null)
            {
                var alert = new UIAlertView("Playback error", "Unable to playback stream", null, "Cancel");
                alert.Show();
                alert.Clicked += (object senderObj, UIButtonEventArgs arg) => alert.DismissWithClickedButtonIndex(0, true);
            }
            else
            {
                var data = NSData.FromArray(audioRecording);
                Player = AVAudioPlayer.FromData(data);

                Player.Volume = 1.0f;
                Player.FinishedPlaying += player_FinishedPlaying;
                Player.PrepareToPlay();
                Player.Play();
            }
        }

        private void player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            Debug.WriteLine("Finished playing");
        }

        public void RecordAudio()
        {
            throw new NotImplementedException();
        }

        public void SelectPhoto()
        {
            // set our source to the photo library
            _imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

            // set what media types
            _imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            // show the picker
            _controller.NavigationController.PresentModalViewController(_imagePicker, true);
        }

        private void Handle_Canceled(object sender, System.EventArgs e)
        {
            _imagePicker.DismissModalViewController(true);
        }

        private void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;

            // get the original image
            UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
            if (originalImage != null)
            {
                // do something with the image
                PhotoCompleteEventHandler handler = PhotoComplete;
                if (handler != null)
                {
                    PhotoCompleteEventArgs args = new PhotoCompleteEventArgs(originalImage.AsJPEG().ToArray());

                    handler(this, args);
                }
            }

            _imagePicker.DismissModalViewController(true);
        }

        public void TakePhoto()
        {
            // set our source to the photo library
            _imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;

            // set what media types
            _imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

            // show the picker
            _controller.NavigationController.PresentModalViewController(_imagePicker, true);
        }

    }
}