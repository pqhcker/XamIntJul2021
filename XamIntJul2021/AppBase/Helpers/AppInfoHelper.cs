using System;
using Xamarin.Essentials;

namespace XamIntJul2021.AppBase.Helpers
{
    public static class AppInfoHelper
    {
        public static string AppName => AppInfo.Name;
        public static string AppVersion => AppInfo.VersionString;
    }
}
