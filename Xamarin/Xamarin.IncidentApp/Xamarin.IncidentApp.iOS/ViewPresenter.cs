// ***********************************************************************
// Assembly         : XamarinIncidentAppiOS
// Author           : Ken Ross
// Created          : 07-22-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-28-2015
// ***********************************************************************
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using UIKit;

/// <summary>
/// The iOS namespace.
/// </summary>
namespace Xamarin.IncidentApp.iOS
{
    /// <summary>
    /// Class ViewPresenter.
    /// </summary>
    public class ViewPresenter : MvxModalNavSupportTouchViewPresenter
    {
        /// <summary>
        /// The ViewCreator backing field.
        /// </summary>
        private IMvxTouchViewCreator _viewCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPresenter"/> class.
        /// </summary>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="window">The window.</param>
        public ViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        /// <summary>
        /// Gets the view creator.
        /// </summary>
        /// <value>The view creator.</value>
        protected IMvxTouchViewCreator ViewCreator
        {
            get { return _viewCreator ?? (_viewCreator = Mvx.Resolve<IMvxTouchViewCreator>()); }
        }

        /// <summary>
        /// Gets the root controller.
        /// </summary>
        /// <value>The root controller.</value>
        public UIViewController RootController { get; private set; }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void Show(MvxViewModelRequest request)
        {
            if (request.PresentationValues != null)
            {
                if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
                {
                    clearStackAndNavigate(request);
                    return;
                }
            }

            base.Show(request);
        }

        /// <summary>
        /// Clears the stack and navigates to the requested View, making this View the "top" of the navigation stack.
        /// </summary>
        /// <param name="request">The request.</param>
        private void clearStackAndNavigate(MvxViewModelRequest request)
        {
            var nextViewController = (UIViewController) ViewCreator.CreateView(request);

            if (MasterNavigationController.TopViewController.GetType() != nextViewController.GetType())
            {
                MasterNavigationController.PopToRootViewController(false);
                MasterNavigationController.ViewControllers = new[] {nextViewController};
            }
        }
    }
}