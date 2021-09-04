using System;
using System.Collections.Generic;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamIntJul2021.ViewModels.NewReport
{
    public class ReportSummaryViewModel : BaseViewModel
    {
        public Command EditStep2Command { get; set; }

        public ReportSummaryViewModel()
        {
            Title = AppResources.PageTitleNewReport2; // Cambiar
            PageId = AppBase.Constants.PageIds.REPORT_SUMMARY;
            EditStep2Command = new(async () => await NavigationService.NavigateToModalAsync(AppBase.Constants.PageIds.NEWREPORTSTEP2));
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            NewReportStep1ViewModel
                = navigationParameters[AppBase.Constants.Parameters.STEP1_VM1] as NewReportStep1ViewModel;
            NewReportStep2ViewModel
                = navigationParameters[AppBase.Constants.Parameters.STEP2_VM1] as NewReportStep2ViewModel;
            NewReportStep3ViewModel
                = navigationParameters[AppBase.Constants.Parameters.STEP3_VM1] as NewReportStep3ViewModel;
        }

        private NewReportStep1ViewModel newReportStep1ViewModel;

        public NewReportStep1ViewModel NewReportStep1ViewModel
        {
            get => newReportStep1ViewModel;
            set => SetProperty(ref newReportStep1ViewModel, value);
        }

        private NewReportStep2ViewModel newReportStep2ViewModel;

        public NewReportStep2ViewModel NewReportStep2ViewModel
        {
            get => newReportStep2ViewModel;
            set => SetProperty(ref newReportStep2ViewModel, value);
        }

        private NewReportStep3ViewModel newReportStep3ViewModel;

        public NewReportStep3ViewModel NewReportStep3ViewModel
        {
            get => newReportStep3ViewModel;
            set => SetProperty(ref newReportStep3ViewModel, value);
        }
    }

}
