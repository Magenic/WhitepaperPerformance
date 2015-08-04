using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace Xamarin.IncidentApp.Droid.MvxMaterial
{
    public class MvxSimpleLayoutInflaterHolder : IMvxLayoutInflater
    {
        public MvxSimpleLayoutInflaterHolder(LayoutInflater layoutInflater)
        {
            LayoutInflater = layoutInflater;
        }

        public LayoutInflater LayoutInflater { get; private set; }
    }
}