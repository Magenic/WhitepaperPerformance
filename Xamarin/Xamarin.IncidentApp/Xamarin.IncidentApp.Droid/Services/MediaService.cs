using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Provider;
using Java.IO;
using Java.Security;
using Xamarin.IncidentApp.Droid.Constants;
using Xamarin.IncidentApp.EventArgs;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.Media;
using File = Java.IO.File;

namespace Xamarin.IncidentApp.Droid.Services
{
    public class MediaService : IMediaService
    {
        private Activity _context;
        private static MediaPlayer _mediaPlayer = new MediaPlayer();

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
            try
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var file = new File(documents);
                var tempMp3 = File.CreateTempFile(Guid.NewGuid().ToString(), "mp3", file);
                tempMp3.DeleteOnExit();

                var stream = new FileOutputStream(tempMp3);
                stream.Write(audioRecording);
                stream.Close();

                // Tried passing path directly, but kept getting 
                // "Prepare failed.: status=0x1"
                // so using file descriptor instead
                var fileStream = new FileInputStream(tempMp3);
                _mediaPlayer.Reset();
                _mediaPlayer.SetDataSource(fileStream.FD);
                _mediaPlayer.Prepare();
                _mediaPlayer.Start();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public event PhotoCompleteEventHandler PhotoComplete;
        public event AudioCompleteEventHandler AudioComplete;
    }
}