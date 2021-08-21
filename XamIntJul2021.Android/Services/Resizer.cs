using System;
using Android.Graphics;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Services.Interfaces;
using XamIntJul2021.Droid.Extensions;
using XamIntJul2021.Droid.Services;

[assembly:Dependency(typeof(Resizer))]
namespace XamIntJul2021.Droid.Services
{
    public class Resizer : IResizer
    {
        public Resizer()
        {
        }

        public byte[] ResizeImage(byte[] originalImage, int newHeight, int newWidth)
        {
            var bitmap = originalImage.ToBitmap();

            Bitmap scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, newWidth, newHeight, false);

            byte[] result = scaledBitmap.ToByteArray();

            scaledBitmap.Dispose();

            return result;
        }

        public byte[] ScaleImage(byte[] originalImage, double percentage)
        {
            var bitmap = originalImage.ToBitmap();

            var width = bitmap.Width;
            var height = bitmap.Height;

            var newWidth = Convert.ToInt16(width * (percentage * .01));
            var newHeight = Convert.ToInt16(height * (percentage * .01));

            return ResizeImage(originalImage, newHeight, newWidth);
        }
    }
}
