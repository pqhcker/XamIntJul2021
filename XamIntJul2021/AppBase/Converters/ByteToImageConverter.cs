using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Services.Interfaces;

namespace XamIntJul2021.AppBase.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public ByteToImageConverter() 
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is byte[] photo)
            {
                return ImageSource.FromStream(() =>
                new MemoryStream(DependencyService.Get<IResizer>().ScaleImage(photo, 20)));
            }

            return ImageSource.FromFile("take");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
