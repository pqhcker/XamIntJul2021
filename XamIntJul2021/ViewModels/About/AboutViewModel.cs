using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.About
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = AppResources.PageTitleAbout;
            PageId = AppBase.Constants.PageIds.ABOUT;
        }

        public String AppName => AppBase.Helpers.AppInfoHelper.AppName;
        public String AppVersion => AppBase.Helpers.AppInfoHelper.AppVersion;

        public String DeviceName => AppBase.Helpers.DeviInfoHelper.DeviceName;
        public String DeviceManufacturer => AppBase.Helpers.DeviInfoHelper.DeviceManufacturer;
        public String DevicePlatform => AppBase.Helpers.DeviInfoHelper.DevicePlatform;
        public String DeviceVersion => AppBase.Helpers.DeviInfoHelper.DeviceSystemVersion;
        public String DeviceModel => AppBase.Helpers.DeviInfoHelper.DeviceModel;

    }
}
