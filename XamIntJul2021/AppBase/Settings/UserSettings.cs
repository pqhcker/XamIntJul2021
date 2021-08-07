using System;
using Xamarin.Essentials;

namespace XamIntJul2021.AppBase.Settings
{
    public static class UserSettings
    {
        public static string Language
        {
            get => Preferences.Get(Constants.UserSettings.LANGUAGE, "es");
            set => Preferences.Set(Constants.UserSettings.LANGUAGE, value);
        }

        public static bool TermsAndConditions
        {
            get => Preferences.Get(Constants.UserSettings.TERMSANDCONDITIONS, false);
            set => Preferences.Set(Constants.UserSettings.TERMSANDCONDITIONS, value);
        }

        public static string AccessToken
        {
            get => Preferences.Get(Constants.UserSettings.TOKEN, null);
            set => Preferences.Set(Constants.UserSettings.TOKEN, value);
        }
    }
}
