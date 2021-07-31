using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.Locations
{
    public class LocationsViewModel : BaseViewModel
    {
        public LocationsViewModel()
        {
            Title = AppResources.PageTitleLocations;
            PageId = AppBase.Constants.PageIds.LOCATIONS;
        }
    }
}
