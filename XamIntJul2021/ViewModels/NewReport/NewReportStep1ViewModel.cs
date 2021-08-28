using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.LocalSaves;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep1ViewModel : BaseViewModel
    {
        string reportId, photoPath1, photoPath2, photoPath3, photoPath4;

        public NewReportStep1ViewModel()
        {
            Title = AppResources.PageTitleNewReport;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP1;
            TakePhotoCommand = new(async (photoIndex) => await TakePhoto(photoIndex.ToString()));

            NextCommand = new(async () => await Next());

            reportId = Guid.NewGuid().ToString();
            LoadData();
        }

        private async Task Next()
        {
            await NavigationService.NavigateToAsync(AppBase.Constants.PageIds.NEWREPORTSTEP2);
        }

        public Command NextCommand { get; set; }

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

        private async Task LoadData()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                var json = AppBase.Helpers.LocalFilesHelper.ReadTextFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP1);

                if (json is not null)
                {
                    var savedReport = JsonConvert.DeserializeObject<NewReportStep1>(json);

                    photoPath1 = savedReport.PhotoPath1;
                    photoPath2 = savedReport.PhotoPath2;
                    photoPath3 = savedReport.PhotoPath3;
                    photoPath4 = savedReport.PhotoPath4;

                    if (photoPath1 is not null)
                        Photo1 = AppBase.Helpers.LocalFilesHelper.ReadFile(photoPath1);
                    if (photoPath2 is not null)
                        Photo2 = AppBase.Helpers.LocalFilesHelper.ReadFile(photoPath2);
                    if (photoPath3 is not null)
                        Photo3 = AppBase.Helpers.LocalFilesHelper.ReadFile(photoPath3);
                    if (photoPath4 is not null)
                        Photo4 = AppBase.Helpers.LocalFilesHelper.ReadFile(photoPath4);

                    reportId = savedReport.ReportId;
                }

                IsBusy = false;
            }
        }

        private async Task TakePhoto(string photoIndex)
        {
            if (!IsBusy)
            {
                IsBusy = true;

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
                        photoPath1 = photoPath;
                        break;
                    case "2":
                        Photo2 = photo;
                        photoPath2 = photoPath;
                        break;
                    case "3":
                        Photo3 = photo;
                        photoPath3 = photoPath;
                        break;
                    case "4":
                        Photo4 = photo;
                        photoPath4 = photoPath;
                        break;

                }

                Save();

                if (photo is not null)
                {
                    NavigationParametersToSend[AppBase.Constants.Parameters.PHOTO_INDEX] = photoIndex;
                    NavigationParametersToSend[AppBase.Constants.Parameters.PHOTO] = photo;
                    NavigationParametersToSend[AppBase.Constants.Parameters.REPORT_ID] = reportId;
                    NavigationParametersToSend[AppBase.Constants.Parameters.STEP_VIEWMODEL] = this;

                    await
                        NavigationService.NavigateToAsync(AppBase.Constants.PageIds.PREVIEWPHOTO,
                        NavigationParametersToSend, true, true);
                }

                IsBusy = false;
            }
        }

        public override void Save()
        {
            NewReportStep1 newReportStep1 = new()
            {
                ReportId = reportId,
                PhotoPath1 = photoPath1,
                PhotoPath2 = photoPath2,
                PhotoPath3 = photoPath3,
                PhotoPath4 = photoPath4,
            };

            var json = JsonConvert.SerializeObject(newReportStep1);

            AppBase.Helpers.LocalFilesHelper.SaveFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP1, json);
        }
    }
}
