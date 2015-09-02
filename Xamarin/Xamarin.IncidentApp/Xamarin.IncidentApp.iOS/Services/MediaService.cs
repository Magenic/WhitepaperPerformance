using System;
using System.Diagnostics;
using System.IO;
using AVFoundation;
using Foundation;
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

        public static AVAudioRecorder Recorder;

        private UIImagePickerController _imagePicker;
        private BaseViewController _controller;
        private AVAudioPlayer _player;
        private AVAudioRecorder _recorder;

        private string _recordedFileName;
        private string _tempAudioFile;

        public MediaService(BaseViewController controller)
        {
            _controller = controller;

            // create a new picker controller
            _imagePicker = new UIImagePickerController();
            _imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            _imagePicker.Canceled += Handle_Canceled;

        }

        internal void PhotoResult(byte[] photo)
        {
            if (PhotoComplete != null)
            {
                PhotoComplete(this, new PhotoCompleteEventArgs(photo));
            }
        }

        public void PlayAudio(byte[] audioRecording, string fileExtension)
        {
            if (_player != null)
            {
                _player.Dispose();
                _player = null;
            }
            var session = AVAudioSession.SharedInstance();
            if (session == null || audioRecording == null)
            {
                var alert = new UIAlertView("Playback error", "Unable to playback stream", null, "Cancel");
                alert.Show();
                alert.Clicked += (object senderObj, UIButtonEventArgs arg) => alert.DismissWithClickedButtonIndex(0, true);
            }
            else
            {
                NSError error;
                ObjCRuntime.Class.ThrowOnInitFailure = false;
                session.SetCategory(AVAudioSessionCategory.Playback);
                session.SetActive(true, out error);
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var temp = Path.Combine(documents, "..", "tmp");
                _tempAudioFile = Path.Combine(temp, Guid.NewGuid() + fileExtension);
                File.WriteAllBytes(_tempAudioFile, audioRecording);
                using (var url = new NSUrl(_tempAudioFile))
                {
                    _player = AVAudioPlayer.FromUrl(url, out error);
                }

                _player.Volume = 1.0f;
                _player.NumberOfLoops = 0;
                _player.FinishedPlaying += player_FinishedPlaying;
                _player.PrepareToPlay();
                _player.Play();
            }
        }

        private void player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            Debug.WriteLine("Finished playing");
            if (_player != null)
            {
                if (File.Exists(_tempAudioFile))
                {
                    File.Delete(_tempAudioFile);
                }
            }
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


        public void StartRecording()
        {
            NSError error;
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var temp = Path.Combine(documents, "..", "tmp");
            _recordedFileName = Path.Combine(temp, Guid.NewGuid() + ".mp4");

            using (var url = new NSUrl(_recordedFileName))
            {

                //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
                var values = new NSObject[]
                {
                    NSNumber.FromFloat(44100.0f), //Sample Rate
                    NSNumber.FromInt32((int) AudioToolbox.AudioFormatType.MPEG4AAC), //AVFormat
                    NSNumber.FromInt32(2), //Channels
                    NSNumber.FromInt32(16), //PCMBitDepth
                    NSNumber.FromBoolean(false), //IsBigEndianKey
                    NSNumber.FromBoolean(false) //IsFloatKey
                };

                //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
                var keys = new NSObject[]
                {
                    AVAudioSettings.AVSampleRateKey,
                    AVAudioSettings.AVFormatIDKey,
                    AVAudioSettings.AVNumberOfChannelsKey,
                    AVAudioSettings.AVLinearPCMBitDepthKey,
                    AVAudioSettings.AVLinearPCMIsBigEndianKey,
                    AVAudioSettings.AVLinearPCMIsFloatKey
                };

                //Set Settings with the Values and Keys to create the NSDictionary
                var settings = NSDictionary.FromObjectsAndKeys(values, keys);

                //Set recorder parameters
                _recorder = AVAudioRecorder.Create(url, new AudioSettings(settings), out error);
            }

            //Set Recorder to Prepare To Record
            _recorder.PrepareToRecord();

            _recorder.Record();
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

        public void StopRecording()
        {
            if (_recorder != null)
                _recorder.Stop();
        }

        public byte[] GetRecording()
        {
            if (string.IsNullOrEmpty(_recordedFileName))
            {
                return null;
            }
            return File.ReadAllBytes(_recordedFileName);
        }

        public string GetRecordingFileExtension()
        {
            if (string.IsNullOrEmpty(_recordedFileName))
            {
                return string.Empty;
            }
            return Path.GetExtension(_recordedFileName);
        }
    }
}