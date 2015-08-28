using System;
using System.IO;
using Android.App;
using Android.Media;
using Java.IO;
using Xamarin.IncidentApp.Droid.Constants;
using Xamarin.IncidentApp.EventArgs;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.Media;
using File = Java.IO.File;

namespace Xamarin.IncidentApp.Droid.Services
{
    public class MediaService : IMediaService, IDisposable
    {
        private Activity _context;
        private static MediaPlayer _mediaPlayer = new MediaPlayer();
        private static MediaRecorder _mediaRecorder;
        private string _recordedFileName;

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

        public void PlayAudio(byte[] audioRecording, string fileExtension)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var file = new File(documents);
            var tempAudioFile = File.CreateTempFile(Guid.NewGuid().ToString(), fileExtension, file);
            tempAudioFile.DeleteOnExit();

            var stream = new FileOutputStream(tempAudioFile);
            stream.Write(audioRecording);
            stream.Close();

            // Tried passing path directly, but kept getting 
            // "Prepare failed.: status=0x1"
            // so using file descriptor instead
            var fileStream = new FileInputStream(tempAudioFile);
            _mediaPlayer.Reset();
            _mediaPlayer.SetDataSource(fileStream.FD);
            _mediaPlayer.Prepare();
            _mediaPlayer.Start();
        }

        public event PhotoCompleteEventHandler PhotoComplete;
        public event AudioCompleteEventHandler AudioComplete;


        public byte[] GetRecording()
        {
            if (string.IsNullOrEmpty(_recordedFileName))
            {
                return null;
            }
            return System.IO.File.ReadAllBytes(_recordedFileName);
        }

        public string GetRecordingFileExtension()
        {
            if (string.IsNullOrEmpty(_recordedFileName))
            {
                return string.Empty;
            }
            return Path.GetExtension(_recordedFileName);
        }

        public void StartRecording()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _recordedFileName = documents + File.PathSeparator + Guid.NewGuid() + ".mp4";

            ClearMediaRecorder();
            _mediaRecorder = new MediaRecorder();
            _mediaRecorder.SetAudioSource(AudioSource.Default);
            _mediaRecorder.SetOutputFormat(OutputFormat.Mpeg4);
            _mediaRecorder.SetAudioEncoder(AudioEncoder.Aac);
            _mediaRecorder.SetOutputFile(_recordedFileName);
            _mediaRecorder.Prepare();
            _mediaRecorder.Start();
        }

        public void StopRecording()
        {
            if (_mediaRecorder != null)
            _mediaRecorder.Stop();
        }

        public void Dispose()
        {
            ClearMediaRecorder();
        }

        private void ClearMediaRecorder()
        {
            if (_mediaRecorder != null)
            {
                _mediaRecorder.Release();
                _mediaRecorder = null;
            }
        }
    }
}