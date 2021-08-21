using System;
namespace XamIntJul2021.AppBase.Services.Interfaces
{
    public interface IResizer
    {
        byte[] ScaleImage(byte[] originalImage, double percentage);

        byte[] ResizeImage(byte[] originalImage, int newHeight, int newWidth);
    }
}
