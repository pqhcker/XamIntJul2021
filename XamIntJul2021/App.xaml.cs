using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamIntJul2021.AppBase.Navigation;
using XamIntJul2021.Views;

namespace XamIntJul2021
{
    public partial class App : Application
    {
        public FormsNavigationService FormsNavigationService { get; private set; }

        public App()
        {
            InitializeComponent();
            FormsNavigationService = new(this);
            FormsNavigationService.RegisterNavigationDictionary(NavigationDictionary.Instance);
            //MainPage = new NavigationPage(new LoginPage());
            FormsNavigationService.ReplaceRootAsync(AppBase.Constants.PageIds.SPLASH, true);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
