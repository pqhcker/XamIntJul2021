using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamIntJul2021.AppBase.Converters
{
    public class BoolToInverseConverter : IValueConverter
    {
        public BoolToInverseConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
                return !boolean;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
