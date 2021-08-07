using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Services.RestService;
using XamIntJul2021.Model;
using XamIntJul2021.Services.Interfaces;
using XamIntJul2021.Services.Responses;
using static XamIntJul2021.AppBase.Constants.RestServiceConstants;

namespace XamIntJul2021.Services.RestServices
{
    public class BranchRestService : BaseRestService, IBranchesService
    {
        public BranchRestService(string accessToken, string accessTokenType)
            :base(accessToken,accessTokenType)
        {

        }

        public async Task<BranchesResponse> GetBranches()
        {
            BranchesResponse userInfoResponse = new()
            {
                ServiceResponse = AppBase.Enums.ServiceResponse.Error,
                Message = AppResources.ErrorMessage
            };

            InitHttpClient();

            var requestUri = $"{API_ENDPOINT}{BRANCH_ENDPOINT}";

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                try
                {

                    using var httpResponse = await httpClient.GetAsync(requestUri);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Ok;
                        userInfoResponse.Message = AppResources.GetBranchesSuccessMessage;
                        userInfoResponse.Branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(await httpResponse.Content.ReadAsStringAsync());
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Unauthorized;
                        userInfoResponse.Message = AppResources.UnauthorizedMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.BadRequest;
                        userInfoResponse.Message = AppResources.GetBranchesErrorMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.ServerError;
                        userInfoResponse.Message = AppResources.ServerErrorMessage;
                    }

                }
                catch (TimeoutException timeout)
                {
                    userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Timeout;
                    userInfoResponse.Message = AppResources.TimeoutMessage;
                }
                catch (Exception ex)
                {
                    userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Error;
                    userInfoResponse.Message = AppResources.ErrorMessage;
                }


            }
            else
            {
                userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.NotConnected;
                userInfoResponse.Message = AppResources.NotConnectedError;
            }

            return userInfoResponse;
        }
    }
}
