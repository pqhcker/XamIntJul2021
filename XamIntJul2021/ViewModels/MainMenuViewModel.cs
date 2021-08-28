using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            Title = AppResources.PageTitleMainMenu;
            PageId = AppBase.Constants.PageIds.MAINMENU;

            AboutCommand = new(async () => await NavigateToPage(AppBase.Constants.PageIds.ABOUT));
            LocationCommand = new(async () => await NavigateToPage(AppBase.Constants.PageIds.LOCATIONS));
            SyncCommand = new(async () => await NavigateToPage(AppBase.Constants.PageIds.SYNC));
            ReportsCommand = new(async () => await NavigateToPage(AppBase.Constants.PageIds.REPORTS));
            NewReportCommand = new(async () => await NavigateToPage(AppBase.Constants.PageIds.NEWREPORTSTEP1));
        }

        public Command AboutCommand { get; set; }
        public Command LocationCommand { get; set; }
        public Command SyncCommand { get; set; }
        public Command ReportsCommand { get; set; }
        public Command NewReportCommand { get; set; }

        private async Task NavigateToPage(string page)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await NavigationService.NavigateToAsync(page);
                IsBusy = false;
            }
            
        }
    }
}
