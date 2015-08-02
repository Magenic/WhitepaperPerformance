﻿using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Xamarin.IncidentApp.Converters
{
    public class BinaryAssetExistsConverter : MvxValueConverter<byte[], bool>
    {
        protected override bool Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null && value.Length > 0);
        }
    }
}
