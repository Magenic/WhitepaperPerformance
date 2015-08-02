using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Provider;
using Xamarin.IncidentApp.Droid.Constants;
using Xamarin.IncidentApp.EventArgs;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.Media;

namespace Xamarin.IncidentApp.Droid.Services
{
    public class MediaService : IMediaService
    {
        private Activity _context;

        public MediaService(Activity context)
        {
            _context = context;
        }

        internal void PhotoResult(byte[] photo)
        {
            if (PhotoComplete != null)
            {
                PhotoComplete(this, new PhotoCompleteEventArgs(photo));
            }
        }

        internal void AudioResult(byte[] audio)
        {
            if (AudioComplete != null)
            {
                AudioComplete(this, new AudioCompleteEventArgs(audio));
            }
        }

        public void TakePhoto()
        {
            var picker = new MediaPicker(_context);
            if (!picker.IsCameraAvailable)
                return;

            var fileName = Guid.NewGuid() + ".jpg";
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
            {
                Name = fileName,
                Directory = null
            });
            _context.StartActivityForResult(intent, IntentConstants.TakePhoto);
        }

        public void SelectPhoto()
        {
            var picker = new MediaPicker(_context);
            var intent = picker.GetPickPhotoUI();
            _context.StartActivityForResult(intent, IntentConstants.SelectPhoto);
        }

        public void RecordAudio()
        {
            var intent = new Intent(MediaStore.Audio.Media.RecordSoundAction);
            _context.StartActivityForResult(intent, IntentConstants.RecordAudio);
        }

        public void PlayAudio(byte[] audioRecording)
        {
            //Todo: finish the player
            var mediaPlayer = MediaPlayer.Create(_context, 1);
            //mediaPlayer.
            mediaPlayer.Start(); 
        }

        public event PhotoCompleteEventHandler PhotoComplete;
        public event AudioCompleteEventHandler AudioComplete;
    }
}