using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Services;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    //[Register("AddIncidentViewController")]
    [MvxViewFor(typeof(AddIncidentViewModel))]
    public partial class AddIncidentViewController : BaseViewController
    {
        private UIActionSheet _actionSheet;
        private MediaService _mediaService;

        public AddIncidentViewController(IntPtr p) : base(p)
        {
            InitView();
        }

        public AddIncidentViewController()
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
                    break;

                case 3: // Cancel
                    break;
            }
        }

        void _mediaService_PhotoComplete(object source, EventArgs.PhotoCompleteEventArgs e)
        {
            NSData imageData = NSData.FromArray(e.ImageStream);
            imgPhoto.Image = UIImage.LoadFromData(imageData);

            ((AddIncidentViewModel) ViewModel).Image = e.ImageStream;

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService(this);
            _mediaService.PhotoComplete += _mediaService_PhotoComplete;

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                // button was clicked
                _actionSheet.ShowInView(View);
            })
            , true);

            this.Title = "Add Incident";

            SetupBindings();
        }

        private void SetupBindings()
        {
            this.CreateBinding(txtSubject).For(c => c.Text).To((AddIncidentViewModel property) => property.Subject).Apply();
            this.CreateBinding(txtSubject).For(c => c.Text).To((AddIncidentViewModel property) => property.Subject).Apply();
            this.CreateBinding(imgPhoto).For(c => c.Image).To((AddIncidentViewModel property) => property.Image).Apply();

            this.CreateBinding(btnSaveIncident).To<AddIncidentViewModel>(vm => vm.SaveNewIncidentCommand).Apply();
            this.CreateBinding(btnRemoveImage).To<AddIncidentViewModel>(vm => vm.RemoveImageCommand).Apply();
            this.CreateBinding(btnAudioNote).To<AddIncidentViewModel>(vm => vm.PlayAudioCommand).Apply();

            // A little more work involved in binding to the picker
            var pickerViewModel = new MvxPickerViewModel(pkrAssigned);
            pkrAssigned.Model = pickerViewModel;
            pkrAssigned.ShowSelectionIndicator = true;
            pickerViewModel.SelectedItemChanged += (sender, args) =>
            {
                var selectedAssignee = (UserProfile)pickerViewModel.SelectedItem;
                ((AddIncidentViewModel)ViewModel).AssignedToId = selectedAssignee.UserId;
            };

            var pickerBindingSet = this.CreateBindingSet<AddIncidentViewController, AddIncidentViewModel>();
            //pickerBindingSet.Bind(pickerViewModel).For(c => ((UserProfile)c.SelectedItem).UserId).To(vm => vm.AssignedToId);
            pickerBindingSet.Bind(pickerViewModel).For(c => c.ItemsSource).To(vm => vm.Workers);
            pickerBindingSet.Apply();

        }
    }
}