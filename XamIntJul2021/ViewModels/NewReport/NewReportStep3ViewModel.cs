using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Validations;
using XamIntJul2021.LocalSaves;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class NewReportStep3ViewModel : BaseViewModel
    {
        bool loaded;

        public NewReportStep3ViewModel()
        {
            Title = AppResources.PageTitleNewReport2; //Cambiar
            PageId = AppBase.Constants.PageIds.NEWREPORTSTEP3;
            NextCommand = new(async () => await Next());
            LoadData();
            NavigationParametersToSend[AppBase.Constants.Parameters.STEP3_VM1] = this;
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            NavigationParametersToSend[AppBase.Constants.Parameters.STEP1_VM1] =
                navigationParameters[AppBase.Constants.Parameters.STEP1_VM1];
            NavigationParametersToSend[AppBase.Constants.Parameters.STEP2_VM1] =
                navigationParameters[AppBase.Constants.Parameters.STEP2_VM1];
        }

        private async Task Next()
        {
            bool isValid = ValidateFields();
            if (isValid)
            {
                await NavigationService.NavigateToAsync(AppBase.Constants.PageIds.REPORT_SUMMARY, NavigationParametersToSend);
            }
        }
        public override void Save()
        {
            NewReportStep3 reportStep3Save = new()
            {
                Amount = Amount,
                ReportDescription = ReportDescription
            };
            var saveJson = JsonConvert.SerializeObject(reportStep3Save);
            AppBase.Helpers.LocalFilesHelper.SaveFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP3, saveJson);
        }
        private void LoadData()
        {
            if (loaded == true)
                return;
            IsBusy = true;
            var savedJson = AppBase.Helpers.LocalFilesHelper.ReadTextFile(AppBase.Constants.LocalFiles.NEW_REPORT_STEP3);
            if (savedJson is not null)
            {
                var savedReport = JsonConvert.DeserializeObject<NewReportStep3>(savedJson);
                Amount = savedReport.Amount;
                ReportDescription = savedReport.ReportDescription;
                loaded = true;
            }
            IsBusy = false;
        }
        public Command NextCommand { get; set; }
        private string reportDescription;
        public string ReportDescription
        {
            get => reportDescription;
            set => SetProperty(ref reportDescription, value);
        }
        private decimal amount;
        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
        #region Validations

        List<Validator> validators = new();
        private void InitValidations()
        {
            if (validators.Any())
                validators.Clear();
            AmountValidator = new(amount, false);
            AmountValidator.Validations.Add(new DoubleGreaterThanValidator(amount, 0.1m));
            validators.Add(AmountValidator);
        }
        private bool ValidateFields()
        {
            InitValidations();
            bool valid = true;
            foreach (var validator in validators)
            {
                validator.Validate();
                valid &= (validator.IsValid == AppBase.Enums.ValidStatus.Valid);
            }
            return valid;
        }
        private Validator reportDescriptionValidator;
        public Validator ReportDescriptionValidator
        {
            get => reportDescriptionValidator;
            set => SetProperty(ref reportDescriptionValidator, value);
        }
        private Validator amountValidator;
        public Validator AmountValidator
        {
            get => amountValidator;
            set => SetProperty(ref amountValidator, value);
        }
        #endregion
    }
}
