using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.Views;

namespace XamIntJul2021.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
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

        }

        async Task SignUpAsync() => await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new SignUpPage()));

        public Command SignUpCommand { get; set; }

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
