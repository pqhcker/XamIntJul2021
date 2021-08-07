using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.Services.Interfaces;
using XamIntJul2021.Services.RestServices;

namespace XamIntJul2021.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        IUserService userService = new UserRestService();

        public SignupViewModel()
        {
            Title = AppBase.Constants.Titles.REGISTRO;
            Subtitle = AppBase.Constants.Subtitles.SINGUP;
            PageId = AppBase.Constants.PageIds.SIGNUP;

            CancelCommand = new(async () => await Cancel());
            CreateUserCommand = new(async () => await CreateUser());
        }

        //private async Task Cancel() => await Application.Current.MainPage.Navigation.PopModalAsync();
        private async Task Cancel() => await NavigationService.GoBackModalAsync();

        public Command CreateUserCommand { get; set; }
        public Command CancelCommand { get; set; }

        private async Task CreateUser()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                //await Task.Delay(1500);

                var registrationResponse = await userService.Register(new()
                {
                    Address = address,
                    Email = email,
                    Password = password,
                    UserName = userName,
                    PhoneNumber = phoneNumber

                });

                if (registrationResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.UserCreatedTitle, registrationResponse.Message, AppResources.AcceptButton);
                    CleanData();
                    await NavigationService.GoBackAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.UserCreationErrorTitle, registrationResponse.Message, AppResources.AcceptButton);
                }


                //await Application.Current.MainPage.DisplayAlert("Usuario creado", $"{UserName} se creo exitosamente", "Aceptar");
                CleanData();
                //await Application.Current.MainPage.Navigation.PopModalAsync();
                await NavigationService.GoBackModalAsync();
                IsBusy = false;
            }
        }

        private void CleanData()
        {
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
        }

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
            set
            {
                SetProperty(ref isValidUserName, value);
                ValidateAll();
            }
        }

        private bool isValidPassword;

        public bool IsValidPassword
        {
            get => isValidPassword;
            set
            {
                SetProperty(ref isValidPassword, value);
                ValidateAll();
            }
        }

        private bool isValidAddress;

        public bool IsValidAddress
        {
            get => isValidAddress;
            set
            {
                SetProperty(ref isValidAddress, value);
                ValidateAll();
            }
        }

        private bool isValidPhoneNumber;

        public bool IsValidPhoneNumber
        {
            get => isValidPhoneNumber;
            set
            {
                SetProperty(ref isValidPhoneNumber, value);
                ValidateAll();
            }
        }

        private bool isValidEmail;

        public bool IsValidEmail
        {
            get => isValidEmail;
            set
            {
                SetProperty(ref isValidEmail, value);
                ValidateAll();
            }
        }

        private bool isValid;

        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        private void ValidateAll() =>
            IsValid = IsValidAddress && IsValidEmail && IsValidPassword && IsValidPhoneNumber && isValidUserName;

        #endregion
    }
}
