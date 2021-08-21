using System;
using System.Collections.Generic;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class PreviewPhotoViewModel : BaseViewModel
    {
        NewReportStep1ViewModel newReportStep1View;


        public PreviewPhotoViewModel()
        {
            Title = AppResources.PageTitlePreviewPhoto;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP1;
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            Photo = navigationParameters[AppBase.Constants.Parameters.PHOTO] as byte[];

            newReportStep1View = navigationParameters[AppBase.Constants.Parameters.STEP_VIEWMODEL] as NewReportStep1ViewModel;

            base.OnNavigationFrom(navigationParameters);
        }

        private byte[] photo;

        public byte[] Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }
    }
}
