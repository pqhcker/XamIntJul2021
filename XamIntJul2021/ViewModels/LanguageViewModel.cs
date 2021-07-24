using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Enums;
using XamIntJul2021.AppBase.Interfaces;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Settings;

namespace XamIntJul2021.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        public LanguageViewModel()
        {
            Title = AppResources.PageTitleLanguage;
            PageId = AppBase.Constants.PageIds.LANG;
            EnglishCommand = new(() => ChangeLanguage(Language.English));
            SpanishCommand = new(() => ChangeLanguage(Language.Spanish));
            NextComand = new(async () => await Next());
        }

        private async Task Next()
        {
            await NavigationService.NavigateToAsync(AppBase.Constants.PageIds.TERMS, null);
        }

        public Command SpanishCommand { get; set; }
        public Command EnglishCommand { get; set; }
        public Command NextComand { get; set; }

        private string nextButton;
        private bool canGoNext;

        public string NextButton
        {
            get => nextButton;
            set => SetProperty(ref nextButton, value);
        }

        public bool CanGoNext
        {
            get => canGoNext;
            set => SetProperty(ref canGoNext, value);
        }


        private void ChangeLanguage(Language language)
        {
            switch (language)
            {
                case Language.Spanish:
                    UserSettings.Language = AppBase.Constants.Language.SPANISH;
                    break;
                case Language.English:
                    UserSettings.Language = AppBase.Constants.Language.ENGLISH;
                    break;
            }

            CultureInfo ci = new(UserSettings.Language);
            AppResources.Culture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            CanGoNext = true;
            Title = AppResources.PageTitleLanguage;
            NextButton = AppResources.Next;

        }


    }
}
