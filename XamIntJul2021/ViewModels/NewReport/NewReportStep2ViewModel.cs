using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Validations;
using XamIntJul2021.LocalSaves;
using XamIntJul2021.Model;
using XamIntJul2021.Services.RestServices;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep2ViewModel : BaseViewModel
    {

        bool loaded = false;

        public NewReportStep2ViewModel()
        {
            Title = AppResources.PageTitleNewReport2;
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP2;
            NextCommand = new(async () => await Next());
            LoadData();
        }

        private async Task Next()
        {
            bool isValid = Validate();
        }

        private async Task LoadData()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await LoadCatalogs();

                var savedJson = AppBase.Helpers.LocalFilesHelper.ReadTextFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP2);

                if (savedJson is not null)
                {
                    var savedReport = JsonConvert.DeserializeObject<NewReportStep2>(savedJson);
                    ClientName = savedReport.ClientName;
                    ClientNumber = savedReport.ClientNumber;
                    ClientPhoneNumber = savedReport.ClientPhoneNumber;
                    ClientCity = savedReport.ClientCity;
                    ClientEmail = savedReport.ClientEmail;
                    SelectedCountry = Countries.FirstOrDefault(c => c.CountryCode == savedReport.ClientCountry);
                    SelectedDocument = Documents.FirstOrDefault(d => d == savedReport.ClientDocument);
                    ClientDocumentNumber = savedReport.ClientDocumentNumber;
                }

                loaded = true;

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

        public override void Save()
        {
            NewReportStep2 reportStep2Save = new()
            {
                ClientName = ClientName,
                ClientNumber = ClientNumber,
                ClientPhoneNumber = ClientPhoneNumber,
                ClientEmail = ClientEmail,
                ClientCountry = SelectedCountry?.CountryCode,
                ClientCity = ClientCity,
                ClientDocument = selectedDocument,
                ClientDocumentNumber = ClientDocumentNumber
            };
            var saveJson = JsonConvert.SerializeObject(reportStep2Save);
            AppBase.Helpers.LocalFilesHelper.SaveFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP2, saveJson);
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
                if (loaded)
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

        #region Validations

        List<Validator> validators = new();

        private void InitValidations()
        {
            if (validators.Any())
                validators.Clear();

            ClientNumberValidator = new Validator(ClientNumber, true);
            ClientNumberValidator.Validations.Add(new ExactLengthValidator(ClientNumber, 10));
            ClientNameValidator = new(ClientName, true);
            ClientPhoneNumberValidator = new(ClientPhoneNumber, true);
            ClientPhoneNumberValidator.Validations.Add(new TelephoneNumberValidator(ClientPhoneNumber));
            ClientEmailValidator = new(ClientEmail, true);
            ClientEmailValidator.Validations.Add(new EmailValidator(ClientEmail));
            ClientCountryValidator = new(selectedCountry?.CountryCode, true);
            ClientCityValidator = new (ClientCity, true);
            ClientDocumentValidator = new(selectedDocument, true);
            ClientDocumentNumberValidator = new(ClientDocumentNumber, true);

            validators.Add(ClientNumberValidator);
            validators.Add(ClientNameValidator);
            validators.Add(ClientPhoneNumberValidator);
            validators.Add(ClientEmailValidator);
            validators.Add(ClientCountryValidator);
            validators.Add(ClientCityValidator);
            validators.Add(ClientDocumentValidator);
            validators.Add(ClientDocumentNumberValidator);


        }

        private bool Validate()
        {
            InitValidations();

            bool isValid = true;

            foreach (var validation in validators)
            {
                validation.Validate();
                isValid &= validation.IsValid == AppBase.Enums.ValidStatus.Valid;
            }
            return isValid;
        }

        private Validator clientNameValidator;
        public Validator ClientNameValidator
        {
            get => clientNameValidator;
            set => SetProperty(ref clientNameValidator, value);
        }
        private Validator clientPhoneNumberValidator;
        public Validator ClientPhoneNumberValidator
        {
            get => clientPhoneNumberValidator;
            set => SetProperty(ref clientPhoneNumberValidator, value);
        }

        private Validator clientEmailValidator;
        public Validator ClientEmailValidator
        {
            get => clientEmailValidator;
            set => SetProperty(ref clientEmailValidator, value);
        }

        private Validator clientNumberValidator;
        public Validator ClientNumberValidator
        {
            get => clientNumberValidator;
            set => SetProperty(ref clientNumberValidator, value);
        }
        private Validator clientCountryValidator;
        public Validator ClientCountryValidator
        {
            get => clientCountryValidator;
            set => SetProperty(ref clientCountryValidator, value);
        }
        private Validator clientCityValidator;
        public Validator ClientCityValidator
        {
            get => clientCityValidator;
            set => SetProperty(ref clientCityValidator, value);
        }
        private Validator clientDocumentValidator;
        public Validator ClientDocumentValidator
        {
            get => clientDocumentValidator;
            set => SetProperty(ref clientDocumentValidator, value);
        }
        private Validator clientDocumentNumberValidator;
        public Validator ClientDocumentNumberValidator
        {
            get => clientDocumentNumberValidator;
            set => SetProperty(ref clientDocumentNumberValidator, value);
        }

        #endregion
    }
}
