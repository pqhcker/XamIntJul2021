using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep1ViewModel : BaseViewModel
    {
        string reportId;

        public NewReportStep1ViewModel()
        {
            Title = AppResources.PageTitleNewReport;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP1;
            TakePhotoCommand = new(async (photoIndex) => await TakePhoto(photoIndex.ToString()));
            reportId = Guid.NewGuid().ToString();
        }
        public Command TakePhotoCommand { get; set; }

        private byte[] photo1;

        public byte[] Photo1
        {
            get => photo1;
            set => SetProperty(ref photo1, value);
        }

        private byte[] photo2;

        public byte[] Photo2
        {
            get => photo2;
            set => SetProperty(ref photo2, value);
        }

        private byte[] photo3;

        public byte[] Photo3
        {
            get => photo3;
            set => SetProperty(ref photo3, value);
        }

        private byte[] photo4;

        public byte[] Photo4
        {
            get => photo4;
            set => SetProperty(ref photo4, value);
        }

        private async Task TakePhoto(string photoIndex)
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
            
            switch (photoIndex)
            {
                case "1":
                    Photo1 = photo;
                    break;
                case "2":
                    Photo2 = photo;
                    break;
                case "3":
                    Photo3 = photo;
                    break;
                case "4":
                    Photo4 = photo;
                    break;

            }

            NavigationParametersToSend[AppBase.Constants.Parameters.PHOTO] = photo;
            NavigationParametersToSend[AppBase.Constants.Parameters.STEP_VIEWMODEL] = this;

            await
                NavigationService.NavigateToAsync(AppBase.Constants.PageIds.PREVIEWPHOTO,
                NavigationParametersToSend, true, true);
        }
    }
}
