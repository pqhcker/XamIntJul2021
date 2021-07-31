using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.Reports
{
    public class ReportsViewModel : BaseViewModel
    {
        public ReportsViewModel()
        {
            Title = AppResources.PageTitleReport;
            PageId = AppBase.Constants.PageIds.REPORTS;
        }
    }
}
