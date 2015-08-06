using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Droid.Constants;
using Xamarin.IncidentApp.ViewModels;
using ActionBar = Android.Support.V7.App.ActionBar;

namespace Xamarin.IncidentApp.Droid.Views
{
    public class TabListener<T> : Java.Lang.Object, ActionBar.ITabListener where T : MvxFragment, new()
    {
        private MvxFragment _Fragment;
        private IMvxViewModel _viewModel;

        public TabListener(IMvxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            //throw new System.NotImplementedException();
        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            // Check if the fragment is already initialized
            if (_Fragment == null)
            {
                // If not, instantiate and add it to the activity
                _Fragment = new T();
                _Fragment.ViewModel = _viewModel;

                ft.Add(Android.Resource.Id.Content, _Fragment, tab.Tag.ToString());
            }
            else
            {
                // If it exists, simply attach it in order to show it
                ft.Attach(_Fragment);
            }

            ((WorkerQueueViewModel) _Fragment.ViewModel).ShowClosed = _Fragment.Tag == KeyConstants.ClosedTab;
        }

        public void OnTabUnselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            if (_Fragment != null)
            {
                ft.Detach(_Fragment);
            }
        }
    }
}