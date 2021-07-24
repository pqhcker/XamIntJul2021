using System;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels
{
    public class TermsAndConditionsViewModel: BaseViewModel
    {
        public TermsAndConditionsViewModel()
        {
            PageId = AppBase.Constants.PageIds.TERMS;
            Title = AppResources.PageTitleTerms;
        }


    }
}
