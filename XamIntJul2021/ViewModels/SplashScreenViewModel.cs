using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Settings;

namespace XamIntJul2021.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        public SplashScreenViewModel()
        {
            OnAppearingCommnad = new Command(async () => await OnAppearing());
        }

        private async Task OnAppearing()
        {
            if (UserSettings.TermsAndConditions)
                await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.PAGEID, true);
            else
                await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.LANG, true);
        }
    }
}
