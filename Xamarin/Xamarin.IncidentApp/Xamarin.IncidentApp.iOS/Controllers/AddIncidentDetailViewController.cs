using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
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
                    break;

                case 1: // Attach Image
                    break;

                case 2: // Record Audio
                    break;

                case 3: // Cancel
                    break;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mediaService = new MediaService(this);
            //ViewModel.SetActivityServices(_mediaService);

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
            this.CreateBinding(btnSaveComment).To<AddIncidentDetailViewModel>(vm => vm.SaveNewIncidentCommand).Apply();
        }
    }
}