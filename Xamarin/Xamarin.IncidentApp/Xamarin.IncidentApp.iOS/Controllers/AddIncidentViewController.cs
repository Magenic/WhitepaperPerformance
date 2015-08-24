using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using Xamarin.IncidentApp.iOS.Services;
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
                    break;

                case 1: // Attach Image
                    SelectPhoto();
                    break;

                case 2: // Record Audio
                    break;

                case 3: // Cancel
                    break;
            }
        }

        private void SelectPhoto()
        {
            var window = new UIWindow(UIScreen.MainScreen.Bounds);

            var viewController = new ImageViewController();

            var navigationController = new UINavigationController();
            navigationController.PushViewController(viewController, false);

            // If you have defined a view, add it here:
            window.AddSubview(navigationController.View);

            // make the window visible
            window.MakeKeyAndVisible();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService();
            //ViewModel.SetActivityServices(_mediaService);

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
            this.CreateBinding(pkrAssigned).For(c => c.DataSource).To((AddIncidentViewModel property) => property.Workers).Apply();
            this.CreateBinding(imgPhoto).For(c => c.Image).To((AddIncidentViewModel property) => property.Image).Apply();

            this.CreateBinding(btnSaveIncident).To<AddIncidentViewModel>(vm => vm.SaveNewIncidentCommand).Apply();
            this.CreateBinding(btnRemoveImage).To<AddIncidentViewModel>(vm => vm.RemoveImageCommand).Apply();
            this.CreateBinding(btnAudioNote).To<AddIncidentViewModel>(vm => vm.PlayAudioCommand).Apply();
        }
    }
}