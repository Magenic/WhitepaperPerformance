using System;
using System.Threading;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("IncidentHeaderCell")]
    public partial class IncidentHeaderCell : MvxTableViewCell
    {
        private string _ownerName;
        private string _ownerInfo;

        public IncidentHeaderCell()
        {
            InitializeBindings();
        }

        public IncidentHeaderCell(IntPtr handle) : base(handle)
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

        private void InitializeBindings()
        {
            this.AutoresizingMask = UIViewAutoresizing.None;

            this.DelayBind(() =>
            {
                this.CreateBinding(lblSubject).For(c => c.Text).To((DisplayIncidentViewModel property) => property.Subject).Apply();
                this.CreateBinding(lblDescription).For(c => c.Text).To((DisplayIncidentViewModel property) => property.Description).Apply();
                this.CreateBinding().For(c => c.OwnerName).To((DisplayIncidentViewModel property) => property.FullName).Apply();
                this.CreateBinding().For(c => c.OwnerInfo).To((DisplayIncidentViewModel property) => property.DateOpened).Apply();
                this.CreateBinding(imgIncidentImage).For(c => c.ImageUrl).To((DisplayIncidentViewModel property) => property.ImageLink).Apply();
                this.CreateBinding(btnPlayAudio).To<DisplayIncidentViewModel>(property => property.PlayAudioCommand).Apply();
            });
        }

        public void SetDescriptionHeight(nfloat height)
        {
            lblDescription.Hidden = height == 0;
            conDescriptionHeight.Constant = height;
        }

        public void SetPlayAudioHeight(nfloat height)
        {
            btnPlayAudio.Hidden = height == 0;
            conPlayAudioHeight.Constant = height;
        }

    }
}