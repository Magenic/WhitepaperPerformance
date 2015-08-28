using Cirrious.MvvmCross.Plugins.Messenger;

namespace Xamarin.IncidentApp.Utilities
{
    public class RecordingCompleteMessage : MvxMessage
    {
        public RecordingCompleteMessage(object sender) : base(sender)
        {
        }

        public byte[] Recording;
        public string FileExtension;
    }
}
