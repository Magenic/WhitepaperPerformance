using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cirrious.CrossCore.Converters;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Converters
{
    public class DetailListConverter : MvxValueConverter<DisplayIncidentViewModel, IList<BaseViewModel>>
    {
        
        protected override IList<BaseViewModel> Convert(DisplayIncidentViewModel value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnList = new List<BaseViewModel>();

            returnList.Add(value);
            returnList.AddRange(value.IncidentDetails.Cast<BaseViewModel>());
            return returnList;
        }
    }
}