using System;
using System.Diagnostics;
using AVFoundation;
using Foundation;
using StreamingAudio;
using UIKit;
using Xamarin.IncidentApp.EventArgs;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.iOS.Services
{
    public class MediaService : IMediaService
    {
        public event AudioCompleteEventHandler AudioComplete;
        public event PhotoCompleteEventHandler PhotoComplete;

        //private StreamingPlayback _audioPlayer;
        private AVAudioPlayer _player;

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
                _player = AVAudioPlayer.FromData(data);

                _player.Volume = 1.0f;
                _player.FinishedPlaying += player_FinishedPlaying;
                _player.PrepareToPlay();
                _player.Play();
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
            throw new NotImplementedException();
        }

        public void TakePhoto()
        {
            throw new NotImplementedException();
        }

    }
}