using System;
using Xamarin.Essentials;

namespace XamIntJul2021.AppBase.Helpers
{
    public static class DeviInfoHelper
    {
        public static string DeviceModel => DeviceInfo.Model;
        public static string DeviceManufacturer => DeviceInfo.Manufacturer;
        public static string DeviceName => DeviceInfo.Name;
        public static string DeviceSystemVersion => DeviceInfo.VersionString;
        public static string DevicePlatform => DeviceInfo.Platform.ToString();


    }
}
