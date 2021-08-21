using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Services.Interfaces;
using XamIntJul2021.iOS.Extensions;
using XamIntJul2021.iOS.Services;

[assembly:Dependency(typeof(Resizer))]
namespace XamIntJul2021.iOS.Services
{
    public class Resizer : IResizer
    {
        public Resizer()
        {
        }

        public byte[] ResizeImage(byte[] originalImage, int newHeight, int newWidth)
        {
            var uiImage = originalImage.ToImage();

            UIGraphics.BeginImageContext(new CoreGraphics.CGSize(newWidth, newHeight));

            uiImage.Draw(new CGRect(0, 0, newWidth, newHeight));

            var result = UIGraphics.GetImageFromCurrentImageContext();

            return result.AsJPEG().ToArray();
        }

        public byte[] ScaleImage(byte[] originalImage, double percentage)
        {
            var uiImage = originalImage.ToImage();

            var width = uiImage.Size.Width;
            var height = uiImage.Size.Height;

            var newWidth = Convert.ToInt16(width * (percentage * 0.01));
            var newHeight = Convert.ToInt16(height * (percentage * 0.01));

            return ResizeImage(originalImage, newHeight, newWidth);
        }
    }
}
