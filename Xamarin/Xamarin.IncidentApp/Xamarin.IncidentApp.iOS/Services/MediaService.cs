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

        //private StreamingPlayback _audioPlayer;
        public static AVAudioPlayer Player;

        UIImagePickerController imagePicker;
        private BaseViewController _controller;

        public MediaService(BaseViewController controller)
        {
            _controller = controller;
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
            // create a new picker controller
            imagePicker = new UIImagePickerController();

            // set our source to the photo library
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

            // set what media types
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;

            // show the picker
            _controller.NavigationController.PresentModalViewController(imagePicker, true);
        }

        private void Handle_Canceled(object sender, System.EventArgs e)
        {
            _controller.NavigationController.PopToViewController(_controller, true);
        }

        private void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            
        }

        public void TakePhoto()
        {
            throw new NotImplementedException();
        }

    }
}