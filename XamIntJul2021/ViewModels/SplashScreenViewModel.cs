using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Settings;
using XamIntJul2021.Services.Interfaces;
using XamIntJul2021.Services.RestServices;

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
            {
                if (UserSettings.AccessToken is not null)
                {
                    CultureInfo ci = new(UserSettings.Language);
                    AppResources.Culture = ci;
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;

                    IUserService userService = new UserRestService(UserSettings.AccessToken, AppBase.Constants.RestServiceConstants.TOKEN_TYPE);

                    var userInfoResponse = await userService.GetUserInfo();

                    if (userInfoResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                        await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.MAINMENU, true);
                    else if (userInfoResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Unauthorized)
                    {
                        UserSettings.AccessToken = null;
                        await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.PAGEID, true);
                    }
                }
                else
                    await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.PAGEID, true);
            }
            else
                await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.LANG, true);
        }
    }
}
