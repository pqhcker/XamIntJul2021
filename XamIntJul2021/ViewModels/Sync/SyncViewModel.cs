using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.Sync
{
    public class SyncViewModel : BaseViewModel
    {
        public SyncViewModel()
        {
            Title = AppResources.PageTitleSync;
            PageId = AppBase.Constants.PageIds.SYNC;
        }
    }
}
