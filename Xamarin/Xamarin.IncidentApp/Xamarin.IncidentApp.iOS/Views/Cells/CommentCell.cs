using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("CommentCell")]
    public partial class CommentCell : MvxTableViewCell
    {
        private string _ownerName;
        private string _ownerInfo;
        private string _audioNote;

        public CommentCell()
        {
            InitializeBindings();
        }

        public CommentCell(IntPtr handle) : base(handle)
        {
            InitializeBindings();
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                lblOwner.Text = string.Concat(value, " - ", OwnerInfo);
                _ownerName = value;
            }
        }

        public string OwnerInfo
        {
            get { return _ownerInfo; }
            set
            {
                lblOwner.Text = string.Concat(OwnerName, " - ", value);
                _ownerInfo = value;
            }
        }

        public string AudioNote
        {
            get { return _audioNote; }
            set
            {
                if (btnPlayAudio != null) btnPlayAudio.Hidden = (value.Length == 0);
                _audioNote = value;
            }
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblComment).For(c => c.Text).To((DisplayIncidentDetailViewModel property) => property.DetailText).Apply();
                this.CreateBinding().For(c => c.OwnerName).To((DisplayIncidentDetailViewModel property) => property.FullName).Apply();
                this.CreateBinding().For(c => c.OwnerInfo).To((DisplayIncidentDetailViewModel property) => property.DateOpened).Apply();
                this.CreateBinding().For(c => c.AudioNote).To((DisplayIncidentDetailViewModel property) => property.AudioRecordingLink).Apply();
                this.CreateBinding(btnPlayAudio).To<DisplayIncidentDetailViewModel>(vm => vm.PlayAudioCommand).Apply();
                this.CreateBinding(imgCommentImage).For(c => c.ImageUrl).To((WorkerQueueItemViewModel property) => property.ImageLink).Apply();
            });
        }
    }
}