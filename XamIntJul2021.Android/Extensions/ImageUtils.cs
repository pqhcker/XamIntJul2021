using System;
using System.IO;
using Android.Graphics;

namespace XamIntJul2021.Droid.Extensions
{
    public static class ImageUtils
    {
        public static Bitmap ToBitmap(this byte[] image) =>
            BitmapFactory.DecodeByteArray(image, 0, image.Length);

        public static byte[] ToByteArray(this Bitmap image,int queality = 100)
        {
            //byte[] result = null;
            using MemoryStream memoryStream = new();
            Bitmap.CompressFormat compressFormat = Bitmap.CompressFormat.Jpeg;
            image.Compress(compressFormat, queality, memoryStream);

            return memoryStream.ToArray();
        }
        
    }
}
