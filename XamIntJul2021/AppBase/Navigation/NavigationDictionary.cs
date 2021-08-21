using System;
using System.Collections.Generic;
using XamIntJul2021.ViewModels;
using XamIntJul2021.Views;
using XamIntJul2021.Views.NewReport;
using XamIntJul2021.ViewModels.NewReport;
using XamIntJul2021.Views.Reports;
using XamIntJul2021.ViewModels.Reports;
using XamIntJul2021.Views.Locations;
using XamIntJul2021.ViewModels.Locations;
using XamIntJul2021.Views.Sync;
using XamIntJul2021.ViewModels.Sync;
using XamIntJul2021.Views.About;
using XamIntJul2021.ViewModels.About;

namespace XamIntJul2021.AppBase.Navigation
{
    public class NavigationDictionary : Dictionary<string, NavigationPair>
    {
        private static readonly NavigationDictionary instance = new();

        public static NavigationDictionary Instance => instance;

        public NavigationDictionary()
        {
            Add(Constants.PageIds.PAGEID, new(typeof(LoginPage), typeof(LoginViewModel)));
            Add(Constants.PageIds.SIGNUP, new(typeof(SignUpPage), typeof(SignupViewModel)));
            Add(Constants.PageIds.LANG, new(typeof(LanguagePage), typeof(LanguageViewModel)));
            Add(Constants.PageIds.TERMS, new(typeof(TermsAndConditionsPage), typeof(TermsAndConditionsViewModel)));
            Add(Constants.PageIds.SPLASH, new(typeof(SplashScreenPage), typeof(SplashScreenViewModel)));
            Add(Constants.PageIds.MAINMENU, new(typeof(MainMenuPage), typeof(MainMenuViewModel)));

            Add(Constants.PageIds.NEWREPORTSTEP1, new(typeof(NewReportStep1), typeof(NewReportStep1ViewModel)));
            Add(Constants.PageIds.REPORTS, new(typeof(ReportsPage), typeof(ReportsViewModel)));
            Add(Constants.PageIds.PREVIEWPHOTO, new(typeof(PreviewPhotoPage), typeof(PreviewPhotoViewModel)));
            Add(Constants.PageIds.LOCATIONS, new(typeof(LocationsPage), typeof(LocationsViewModel)));
            Add(Constants.PageIds.BRANCH_DETAIL, new(typeof(BranchDetail), typeof(BranchDetailViewModel)));
            Add(Constants.PageIds.SYNC, new(typeof(SyncPage), typeof(SyncViewModel)));
            Add(Constants.PageIds.ABOUT, new(typeof(AboutPage), typeof(AboutViewModel)));
        }
    }
}
