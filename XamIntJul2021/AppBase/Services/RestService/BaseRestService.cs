using System;
using System.Net.Http;

namespace XamIntJul2021.AppBase.Services.RestService
{
    public class BaseRestService
    {
        protected static HttpClient httpClient;
        protected static string accessToken;
        protected static string accessTokenType;


        public BaseRestService(string accessToken, string accessTokenType)
        {
            BaseRestService.accessToken = accessToken;
            BaseRestService.accessTokenType = accessTokenType;
        }

        public BaseRestService()
        {

        }

        protected void InitHttpClient()
        {
            if (httpClient is null)
                httpClient = new()
                {
                    Timeout = TimeSpan.FromSeconds(Constants.RestServiceConstants.TIMEOUT)
                };
            if (!string.IsNullOrEmpty(accessToken) && !httpClient.DefaultRequestHeaders.Contains("Authorization"))
                httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessTokenType} {accessToken}");
        }
    }
}
