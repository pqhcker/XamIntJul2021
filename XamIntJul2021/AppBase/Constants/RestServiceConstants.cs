using System;
namespace XamIntJul2021.AppBase.Constants
{
    public static class RestServiceConstants
    {
        public const int TIMEOUT = 120;

        public const string API_ENDPOINT = "https://xamarininteligenteapi.azurewebsites.net/api";

        public const string LOGIN_ENDPOINT = "/auth/login";

        //public const string SHORTLOGIN_ENDPOINT = "/auth/shortlogin";

        public const string REGISTER_ENDPOINT = "/auth/Register";

        public const string USER_ENDPOINT = "/auth/user";

        public const string BRANCH_ENDPOINT = "/branches";

        public const int BRANCH_PAGE_SIZE = 30;

        public const string TOKEN_TYPE = "bearer";
    }
}
