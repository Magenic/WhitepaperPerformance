
namespace Xamarin.IncidentApp.EventArgs
{
    public class AudioCompleteEventArgs : System.EventArgs
    {
        private byte[] _audioStream;
        public AudioCompleteEventArgs(byte[] audioStream)
        {
            _audioStream = audioStream;
        }
        public byte[] AudioStream
        {
            get { return _audioStream; }
        }
    }
}