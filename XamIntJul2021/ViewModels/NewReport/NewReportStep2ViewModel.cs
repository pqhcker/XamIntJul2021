using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.Model;
using XamIntJul2021.Services.RestServices;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep2ViewModel : BaseViewModel
    {
        public NewReportStep2ViewModel()
        {
            Title = AppResources.PageTitleNewReport2;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP2;
            LoadData();
        }

        private async Task LoadData()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await LoadCatalogs();

                IsBusy = false;
            }
        }

        private async Task LoadCatalogs()
        {
            var countriesService =
                new CountriesRestService(AppBase.Settings.UserSettings.AccessToken,
                AppBase.Constants.RestServiceConstants.TOKEN_TYPE);

            var response = await countriesService.GetCountries();

            if (response.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                Countries = new(response.Countries);

            Documents = new() { "Pasaporte", "Licencia de conducir" };
        }

        public Command NextCommand { get; set; }
        private ObservableCollection<Country> countries;
        public ObservableCollection<Country> Countries
        {
            get => countries;
            set => SetProperty(ref countries, value);
        }
        private ObservableCollection<string> documents;
        public ObservableCollection<string> Documents
        {
            get => documents;
            set => SetProperty(ref documents, value);
        }
        private string clientName;
        public string ClientName
        {
            get => clientName;
            set => SetProperty(ref clientName, value);
        }
        private string clientPhoneNumber;
        public string ClientPhoneNumber
        {
            get => clientPhoneNumber;
            set => SetProperty(ref clientPhoneNumber, value);
        }
        private string clientEmail;
        public string ClientEmail
        {
            get => clientEmail;
            set => SetProperty(ref clientEmail, value);
        }
        private string clientNumber;
        public string ClientNumber
        {
            get => clientNumber;
            set => SetProperty(ref clientNumber, value);
        }
        private Country selectedCountry;
        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                SetProperty(ref selectedCountry, value);
                Save();
            }
        }
        private string clientCity;
        public string ClientCity
        {
            get => clientCity;
            set => SetProperty(ref clientCity, value);
        }
        private string selectedDocument;
        public string SelectedDocument
        {
            get => selectedDocument;
            set => SetProperty(ref selectedDocument, value);
        }
        private string clientDocumentNumber;
        public string ClientDocumentNumber
        {
            get => clientDocumentNumber;
            set => SetProperty(ref clientDocumentNumber, value);
        }
    }
}
