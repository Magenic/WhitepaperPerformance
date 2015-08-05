using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace Xamarin.IncidentApp.Droid.MvxMaterial
{
    public interface IMvxFragmentView
        : IMvxBindingContextOwner
        , IMvxView
    {
    }

    public interface IMvxFragmentView<TViewModel>
        : IMvxFragmentView
        , IMvxView<TViewModel> where TViewModel : class
        , IMvxViewModel
    {
    }
}