using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.iOS.Controllers
{
    [MvxViewFor(typeof(AudioRecorderViewModel))]
    public partial class AudioRecorderViewController : BaseViewController
    {

        public AudioRecorderViewController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewController"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
            public AudioRecorderViewController(IntPtr handle)
                : base(handle)
        {
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public new AudioRecorderViewModel ViewModel
        {
            get { return (AudioRecorderViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        /// <summary>
        /// Fires when the View loads.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            NavigationController.NavigationBarHidden = false;

            this.Title = "Record Audio";

            SetupBindings();
        }

        private void SetupBindings()
        {
            this.CreateBinding(btnRecord).To<AudioRecorderViewModel>(vm => vm.StartRecordingCommand).Apply();
            this.CreateBinding(btnPlay).To<AudioRecorderViewModel>(vm => vm.PlayRecordingCommand).Apply();
            this.CreateBinding(btnStop).To<AudioRecorderViewModel>(vm => vm.StopRecordingCommand).Apply();
            this.CreateBinding(btnSaveRecording).To<AudioRecorderViewModel>(vm => vm.ReturnResultCommand).Apply();
        }
    }
}