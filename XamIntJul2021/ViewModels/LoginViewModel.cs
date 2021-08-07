using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.Services.Interfaces;
using XamIntJul2021.Services.RestServices;
using XamIntJul2021.Views;

namespace XamIntJul2021.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        IUserService userService = new UserRestService();

        public LoginViewModel()
        {
            Title = AppBase.Constants.Titles.LOGINPAGE;
            Subtitle = AppBase.Constants.Subtitles.SUBTITLE;
            PageId = AppBase.Constants.PageIds.PAGEID;

            //IsBusy = true;

            //OnAppearingCommnad = new Command(() => App.Current.MainPage.DisplayAlert("Hola", "Aparecio la pantalla", "Aceptar"));

#if DEBUG
            //UserName = "Alejandro";
            //Password = "1234";
#endif

            SignUpCommand = new(async () => await SignUpAsync());
            LoginCommand = new(async () => await Login());
        }

        //async Task SignUpAsync() => await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new SignUpPage()));
        async Task SignUpAsync() => await NavigationService.NavigateToModalAsync(AppBase.Constants.PageIds.SIGNUP);

        private async Task Login()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                //await Task.Delay(2000);

                var loginReponse = await userService.Login(new()
                {
                    UserName = userName,
                    Password = password
                });

                if (loginReponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.LoginTitle, loginReponse.Message, AppResources.AcceptButton);
                    await NavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.MAINMENU, true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.LoginTitle, loginReponse.Message, AppResources.AcceptButton);
                }

                IsBusy = false;
            }
        }

        public Command SignUpCommand { get; set; }
        public Command LoginCommand { get; set; }

        private string userName;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
    }
}
