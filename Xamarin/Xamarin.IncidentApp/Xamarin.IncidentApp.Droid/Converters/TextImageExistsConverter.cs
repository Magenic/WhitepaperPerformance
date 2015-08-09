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
            return (value.ImageBytes != null && value.ImageBytes.Length > 0) || !String.IsNullOrEmpty(value.DetailText)
                ? ViewStates.Visible
                : ViewStates.Gone;
        }
    }
}