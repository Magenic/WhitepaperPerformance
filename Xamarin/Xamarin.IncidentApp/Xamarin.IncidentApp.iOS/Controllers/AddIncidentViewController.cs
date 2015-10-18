using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using UIKit;
using Xamarin.IncidentApp.iOS.Services;
using Xamarin.IncidentApp.Models;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
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

        public new AddIncidentViewModel ViewModel
        {
            get { return (AddIncidentViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
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
                    ViewModel.TakeNewPhotoCommand.Execute(null);
                    break;

                case 1: // Attach Image
                    ViewModel.SelectPhotoCommand.Execute(null);
                    break;

                case 2: // Record Audio
                    ViewModel.RecordAudioCommand.Execute(null);
                    break;

                case 3: // Cancel
                    break;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService(this);

            ViewModel.SetActivityServices(_mediaService);

            NavigationController.NavigationBarHidden = false;
            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                // button was clicked
                _actionSheet.ShowInView(View);
            })
            , true);

            Title = "Add Incident";

            txtSubject.ShouldReturn += CloseKeyboard;
            txtDescription.ShouldReturn += CloseKeyboard;

            ViewModel.PropertyChanged += Vm_PropertyChanged;

            SetupBindings();
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Image" || e.PropertyName == "AudioRecording")
            scrlMain.LayoutIfNeeded();
        }

        private void SetupBindings()
        {
            this.CreateBinding(txtSubject).For(c => c.Text).To((AddIncidentViewModel property) => property.Subject).Apply();
            this.CreateBinding(txtDescription).For(c => c.Text).To((AddIncidentViewModel property) => property.Description).Apply();
            this.CreateBinding(imgPhoto).For(c => c.Image).To((AddIncidentViewModel property) => property.Image).WithConversion("ByteBitmap").Apply();
            this.CreateBinding(conPhotoHeight).For(c => c.Constant).To<AddIncidentViewModel>(vm => vm.Image).WithConversion("ByteBitmapHiddenHeight", 201).Apply();

            this.CreateBinding(btnSaveIncident).To<AddIncidentViewModel>(vm => vm.SaveNewIncidentCommand).Apply();
            this.CreateBinding(btnRemoveImage).To<AddIncidentViewModel>(vm => vm.RemoveImageCommand).Apply();
            this.CreateBinding(btnRemoveImage).For(c => c.Hidden).To<AddIncidentDetailViewModel>(vm => vm.Image).WithConversion("ByteBitmapHidden").Apply();
            this.CreateBinding(btnAudioNote).To<AddIncidentViewModel>(vm => vm.PlayAudioCommand).Apply();
            this.CreateBinding(btnAudioNote).For(c => c.Hidden).To<AddIncidentDetailViewModel>(vm => vm.AudioRecording).WithConversion("ByteBitmapHidden").Apply();
            this.CreateBinding(conAudioHeight).For(c => c.Constant).To<AddIncidentViewModel>(vm => vm.AudioRecording).WithConversion("ByteBitmapHiddenHeight", 34).Apply();
            this.CreateBinding(btnRemoveAudio).To<AddIncidentViewModel>(vm => vm.RemoveAudioCommand).Apply();
            this.CreateBinding(btnRemoveAudio).For(c => c.Hidden).To<AddIncidentDetailViewModel>(vm => vm.AudioRecording).WithConversion("ByteBitmapHidden").Apply();

            // A little more work involved in binding to the picker
            var pickerViewModel = new MvxPickerViewModel(pkrAssigned);
            pkrAssigned.Model = pickerViewModel;
            pkrAssigned.ShowSelectionIndicator = true;
            pickerViewModel.SelectedItemChanged += (sender, args) =>
            {
                var selectedAssignee = (UserProfile)pickerViewModel.SelectedItem;
                ViewModel.AssignedToId = selectedAssignee.UserId;
            };

            var pickerBindingSet = this.CreateBindingSet<AddIncidentViewController, AddIncidentViewModel>();
            pickerBindingSet.Bind(pickerViewModel).For(c => c.ItemsSource).To(vm => vm.Workers);
            pickerBindingSet.Apply();
        }

        private bool CloseKeyboard(UITextField textField)
        {
            if (textField.Equals(txtSubject))
            {
                // validate field inputs as per your requirement
                txtDescription.BecomeFirstResponder();
                return true;
            }
            if (textField.Equals(txtDescription))
            {
                // validate field inputs as per your requirement
                textField.ResignFirstResponder();
                return true;
            }
            return true;
        }
    }
}