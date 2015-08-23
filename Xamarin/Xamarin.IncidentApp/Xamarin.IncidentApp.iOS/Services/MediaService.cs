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
        public static AVAudioPlayer Player;

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
            throw new NotImplementedException();
        }

        public void TakePhoto()
        {
            throw new NotImplementedException();
        }

    }
}