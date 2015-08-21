using Xamarin.IncidentApp.EventArgs;

namespace Xamarin.IncidentApp.Interfaces
{
    public delegate void PhotoCompleteEventHandler(object source, PhotoCompleteEventArgs e);
    public delegate void AudioCompleteEventHandler(object source, AudioCompleteEventArgs e);

    public interface IMediaService
    {
        void TakePhoto();
        void SelectPhoto();
        void RecordAudio();
        void PlayAudio(byte[] audioRecording);
        event PhotoCompleteEventHandler PhotoComplete;
        event AudioCompleteEventHandler AudioComplete;
    }
}