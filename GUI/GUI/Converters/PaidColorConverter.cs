﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace GUI.Converters
{
    class PaidColorConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool paid))
                throw new ArgumentException($"{nameof(PaidColorConverter)} expected boolean value");

            return paid
                ? Color.Green
                : Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
