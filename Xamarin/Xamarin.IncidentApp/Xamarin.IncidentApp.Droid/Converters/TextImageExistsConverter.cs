using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Views;
using Cirrious.CrossCore.Converters;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class TextImageExistsConverter : MvxValueConverter<DisplayIncidentDetailViewModel, ViewStates>
    {
        protected override ViewStates Convert(DisplayIncidentDetailViewModel value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!string.IsNullOrEmpty(value.ImageLink) || !String.IsNullOrEmpty(value.DetailText))
                ? ViewStates.Visible
                : ViewStates.Gone;
        }
    }
}