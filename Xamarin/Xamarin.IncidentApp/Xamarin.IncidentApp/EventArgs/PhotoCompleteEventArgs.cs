
namespace Xamarin.IncidentApp.EventArgs
{
    public class PhotoCompleteEventArgs : System.EventArgs
    {
        private byte[] _imageStream;
        public PhotoCompleteEventArgs(byte[] imageStream)
        {
            _imageStream = imageStream;
        }
        public byte[] ImageStream
        {
            get { return _imageStream; }
        }
    }
}
