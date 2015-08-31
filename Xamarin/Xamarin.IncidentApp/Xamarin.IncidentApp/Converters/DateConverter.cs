using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.Converters
{
    public class DateConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0} @ {1}", String.Format("{0:d}", value), String.Format("{0:t}", value));
        }
    }
}