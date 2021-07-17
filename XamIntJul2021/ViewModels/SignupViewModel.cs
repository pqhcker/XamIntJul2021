using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;

namespace XamIntJul2021.ViewModels
{
    public class SignupViewModel: BaseViewModel
    {
        public SignupViewModel()
        {
            Title = AppBase.Constants.Titles.REGISTRO;
            Subtitle = AppBase.Constants.Subtitles.SINGUP;
            PageId = AppBase.Constants.PageIds.SIGNUP;

            CancelCommand = new(async () => await Cancel());
        }

        private async Task Cancel() => await Application.Current.MainPage.Navigation.PopModalAsync();

        public Command CreateUserCommand { get; set; }
        public Command CancelCommand { get; set; }

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

        private string address;

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        private string email;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        #region Validaciones
        private bool isValidUserName;

        public bool IsValidUserName
        {
            get => isValidUserName;
            set => SetProperty(ref isValidUserName, value);
        }

        private bool isValidPassword;

        public bool IsValidPassword
        {
            get => isValidPassword;
            set => SetProperty(ref isValidPassword, value);
        }

        private bool isValidAddress;

        public bool IsValidAddress
        {
            get => isValidAddress;
            set => SetProperty(ref isValidAddress, value);
        }

        private bool isValidPhoneNumber;

        public bool IsValidPhoneNumber
        {
            get => isValidPhoneNumber;
            set => SetProperty(ref isValidPhoneNumber, value);
        }

        private bool isValidEmail;

        public bool IsValidEmail
        {
            get => isValidEmail;
            set => SetProperty(ref isValidEmail, value);
        }
        #endregion
    }
}
