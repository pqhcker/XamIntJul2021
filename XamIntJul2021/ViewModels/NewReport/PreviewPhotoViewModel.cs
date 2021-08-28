using System;
using System.Collections.Generic;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class PreviewPhotoViewModel : BaseViewModel
    {
        NewReportStep1ViewModel newReportStep1ViewModel;
        private string photoIndex;
        private string reportId;

        public Command AcceptPhotoCommand { get; set; }
        public Command TakePhotoCommand { get; set; }

        public PreviewPhotoViewModel()
        {
            Title = AppResources.PageTitlePreviewPhoto;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP1;
            AcceptPhotoCommand = new(async () => await AcceptPhoto());
            TakePhotoCommand = new(async () => await TakePhoto());
        }

        private async Task TakePhoto()
        {
            var photo = await AppBase.Helpers.MediaHelper.TakePhotoAsync();

            string photoPath = $"{reportId}-{photoIndex}.jpg";

            if (photo is not null)
            {
                AppBase.Helpers.LocalFilesHelper.SaveFile(photoPath, photo);
            }
            else
            {
                AppBase.Helpers.LocalFilesHelper.DeleteFile(photoPath);
            }

            Photo = photo;

            switch (photoIndex)
            {
                case "1":
                    newReportStep1ViewModel.Photo1 = photo;
                    break;
                case "2":
                    newReportStep1ViewModel.Photo2 = photo;
                    break;
                case "3":
                    newReportStep1ViewModel.Photo3 = photo;
                    break;
                case "4":
                    newReportStep1ViewModel.Photo4 = photo;
                    break;

            }


            await
                NavigationService.NavigateToAsync(AppBase.Constants.PageIds.PREVIEWPHOTO,
                NavigationParametersToSend, true, true);
        }

        private async Task AcceptPhoto()
        {
            await NavigationService.GoBackModalAsync();
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            Photo = navigationParameters[AppBase.Constants.Parameters.PHOTO] as byte[];

            newReportStep1ViewModel = navigationParameters[AppBase.Constants.Parameters.STEP_VIEWMODEL] as NewReportStep1ViewModel;

            photoIndex = navigationParameters[AppBase.Constants.Parameters.PHOTO_INDEX]?.ToString();

            reportId = navigationParameters[AppBase.Constants.Parameters.REPORT_ID]?.ToString();

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
