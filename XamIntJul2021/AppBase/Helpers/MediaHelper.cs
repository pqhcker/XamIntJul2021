using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamIntJul2021.AppBase.Helpers
{
    public static class MediaHelper
    {
        public static async Task<byte[]> TakePhotoAsync()
        {
            byte[] photoBytes = null;

            if(MediaPicker.IsCaptureSupported)
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if(photo is not null)
                {
                    using var stream = await photo.OpenReadAsync();
                    using var memoryStream = new MemoryStream();

                    await stream.CopyToAsync(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
            }

            return photoBytes;
        }
    }
}
