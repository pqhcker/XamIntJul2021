using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep1ViewModel : BaseViewModel
    {
        public NewReportStep1ViewModel()
        {
            Title = AppResources.PageTitleNewReport;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP1;
        }
    }
}
