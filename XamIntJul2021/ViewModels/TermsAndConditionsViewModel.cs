using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Settings;

namespace XamIntJul2021.ViewModels
{
    public class TermsAndConditionsViewModel: BaseViewModel
    {
        public TermsAndConditionsViewModel()
        {
            PageId = AppBase.Constants.PageIds.TERMS;
            Title = AppResources.PageTitleTerms;

            TermsPath = UserSettings.Language
                switch
            {
                AppBase.Constants.Language.SPANISH => "TermsAndConditionsES.html",
                AppBase.Constants.Language.ENGLISH => "TermsAndConditionsEN.html",
                _ => "TermsAndConditionsEN.html"
            };

            AcceptTermsCommand = new(async () => await AcceptAsync());
        }

        private async Task AcceptAsync()
        {
            UserSettings.TermsAndConditions = true;
            await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.PAGEID,true);
        }

        public Command AcceptTermsCommand { get; set; }

        private string termsPath;

        public string TermsPath
        {
            get => termsPath;
            set => SetProperty(ref termsPath, value);
        }


    }
}
