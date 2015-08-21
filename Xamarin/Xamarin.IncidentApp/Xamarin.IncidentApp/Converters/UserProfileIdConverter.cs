using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using Xamarin.IncidentApp.Models;

namespace Xamarin.IncidentApp.Converters
{
    public class UserProfileIdConverter : MvxValueConverter<string, UserProfile>
    {
        protected override string ConvertBack(UserProfile value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty: value.UserId;
        }
    }
}