using System;

namespace Xamarin.IncidentApp.Droid.MvxMaterial
{
    public static class MvxOwnedViewModelFragmentAttributeExtensionMethods
    {
        public static bool IsOwnedViewModelFragment(this Type candidateType)
        {
            var attributes = candidateType.GetCustomAttributes(typeof(MvxOwnedViewModelFragmentAttribute), true);
            return attributes.Length > 0;
        }
    }
}