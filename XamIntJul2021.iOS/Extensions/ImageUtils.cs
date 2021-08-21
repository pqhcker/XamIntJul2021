using System;
using Foundation;
using UIKit;

namespace XamIntJul2021.iOS.Extensions
{
    public static class ImageUtils
    {
        public static UIImage ToImage(this byte[] image) =>
            new (NSData.FromArray(image));
    }
}
