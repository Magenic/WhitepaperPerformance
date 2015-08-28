using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Services;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    //[Register("AddIncidentDetailViewController")]
    [MvxViewFor(typeof(AddIncidentDetailViewModel))]
    public partial class AddIncidentDetailViewController : BaseViewController
    {
        private UIActionSheet _actionSheet;
        private MediaService _mediaService;

        public AddIncidentDetailViewController(IntPtr p) : base(p)
        {
            InitView();
        }

        public AddIncidentDetailViewController()
        {
            InitView();
        }

        private void InitView()
        {
            SetupActionSheet();
        }

        private void SetupActionSheet()
        {
            _actionSheet = new UIActionSheet("Comment Actions");
            _actionSheet.AddButton("Take Photo");
            _actionSheet.AddButton("Attach Image");
            _actionSheet.AddButton("Record Audio");
            _actionSheet.AddButton("Cancel");

            _actionSheet.CancelButtonIndex = 3;

            _actionSheet.Clicked += _actionSheet_Clicked;

        }

        void _actionSheet_Clicked(object sender, UIButtonEventArgs e)
        {
            switch (e.ButtonIndex)
            {
                case 0: // Take Photo
                    _mediaService.TakePhoto();
                    break;

                case 1: // Attach Image
                    _mediaService.SelectPhoto();
                    break;

                case 2: // Record Audio
                    _mediaService.RecordAudio();
                    break;

                case 3: // Cancel
                    break;
            }
        }
        //void _mediaService_PhotoComplete(object source, EventArgs.PhotoCompleteEventArgs e)
        //{
        //    NSData imageData = NSData.FromArray(e.ImageStream);
        //    imgPhoto.Image = UIImage.LoadFromData(imageData);

        //    ((AddIncidentDetailViewModel)ViewModel).Image = e.ImageStream;

        //}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService(this);
            //_mediaService.PhotoComplete += _mediaService_PhotoComplete;

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                // button was clicked
                _actionSheet.ShowInView(View);
            })
            , true);

            this.Title = "Add Comment";

            SetupBindings();
        }

        private void SetupBindings()
        {
            this.CreateBinding(txtComment).For(c => c.Text).To((AddIncidentDetailViewModel property) => property.DetailText).Apply();
            this.CreateBinding(imgPhoto).For(c => c.Image).To((AddIncidentDetailViewModel property) => property.Image).WithConversion("ByteBitmap").Apply();

            this.CreateBinding(btnSaveComment).To<AddIncidentDetailViewModel>(vm => vm.SaveNewIncidentCommand).Apply();
            this.CreateBinding(btnRemoveImage).To<AddIncidentDetailViewModel>(vm => vm.RemoveImageCommand).Apply();
            this.CreateBinding(btnAudioNote).To<AddIncidentDetailViewModel>(vm => vm.PlayAudioCommand).Apply();
            this.CreateBinding(btnRemoveAudio).To<AddIncidentDetailViewModel>(vm => vm.RemoveAudioCommand).Apply();
        }
    }
}