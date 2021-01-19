using System;
using System.Globalization;
using Xamarin.Forms;

namespace GUI.Converters
{
    class DateTimeToYYYYMMDDConveter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime dt)
            {
                return dt.ToShortDateString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string str)
            {
                return DateTime.Parse(str);
            }
            return null;
        }
    }
}
