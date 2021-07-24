using System;
using System.Collections.Generic;
using XamIntJul2021.ViewModels;
using XamIntJul2021.Views;

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
        }
    }
}
