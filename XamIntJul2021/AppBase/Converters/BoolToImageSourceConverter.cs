using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamIntJul2021.AppBase.Converters
{
    public class BoolToImageSourceConverter : IValueConverter
    {
        public BoolToImageSourceConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool result)
            {
                var imageSource = result ? ImageSource.FromFile("rigth") : ImageSource.FromFile("wrong");
                return imageSource;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
