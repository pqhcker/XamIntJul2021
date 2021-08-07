using System;
using System.Net.Http;
using System.Text;
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
    public class UserRestService : BaseRestService, IUserService
    {
        public UserRestService()
        {
        }

        public UserRestService(string accessToken, string accessTokenType)
            : base(accessToken, accessTokenType)
        {

        }

        public async Task<UserInfoResponse> GetUserInfo()
        {
            UserInfoResponse userInfoResponse = new()
            {
                ServiceResponse = AppBase.Enums.ServiceResponse.Error,
                Message = AppResources.ErrorMessage
            };

            InitHttpClient();

            var requestUri = $"{API_ENDPOINT}{USER_ENDPOINT}";

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                try
                {
                    
                    using var httpResponse = await httpClient.GetAsync(requestUri);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Ok;
                        userInfoResponse.Message = AppResources.UserInfoSuccessMessage;
                        userInfoResponse.User = JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Unauthorized;
                        userInfoResponse.Message = AppResources.UnauthorizedMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        userInfoResponse.ServiceResponse = AppBase.Enums.ServiceResponse.BadRequest;
                        userInfoResponse.Message = AppResources.UserInfoErrorMessage;
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

        public async Task<LoginResponse> Login(LoginCredentials loginCredentials)
        {
            LoginResponse loginResponse = new()
            {
                ServiceResponse = AppBase.Enums.ServiceResponse.Error,
                Message = AppResources.ErrorMessage
            };

            InitHttpClient();

            var requestUri = $"{API_ENDPOINT}{LOGIN_ENDPOINT}";

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                try
                {
                    var stringContent = JsonConvert.SerializeObject(loginCredentials);
                    var httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");

                    using var httpResponse = await httpClient.PostAsync(requestUri, httpContent);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Ok;
                        loginResponse.Message = string.Format(AppResources.SuccessLoginMessage, loginCredentials.UserName);
                        loginResponse.AccessToken = await httpResponse.Content.ReadAsStringAsync();
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Unauthorized;
                        loginResponse.Message = AppResources.UnauthorizedMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.BadRequest;
                        loginResponse.Message = AppResources.ErrorLoginMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.ServerError;
                        loginResponse.Message = AppResources.ServerErrorMessage;
                    }

                }
                catch (TimeoutException timeout)
                {
                    loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Timeout;
                    loginResponse.Message = AppResources.TimeoutMessage;
                }
                catch (Exception ex)
                {
                    loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Error;
                    loginResponse.Message = AppResources.ErrorMessage;
                }


            }
            else
            {
                loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.NotConnected;
                loginResponse.Message = AppResources.NotConnectedError;
            }

            return loginResponse;
        }

        public async Task<RegisterResponse> Register(User newUser)
        {
            RegisterResponse loginResponse = new()
            {
                ServiceResponse = AppBase.Enums.ServiceResponse.Error,
                Message = AppResources.ErrorMessage
            };

            InitHttpClient();

            var requestUri = $"{API_ENDPOINT}{REGISTER_ENDPOINT}";

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                try
                {
                    var stringContent = JsonConvert.SerializeObject(newUser);
                    var httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");

                    using var httpResponse = await httpClient.PostAsync(requestUri, httpContent);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Ok;
                        loginResponse.Message = AppResources.UserCreatedMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Unauthorized;
                        loginResponse.Message = AppResources.UnauthorizedMessage;
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.BadRequest;
                        loginResponse.Message = await httpResponse.Content.ReadAsStringAsync();
                    }
                    else if (httpResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.ServerError;
                        loginResponse.Message = AppResources.ServerErrorMessage;
                    }

                }
                catch (TimeoutException timeout)
                {
                    loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Timeout;
                    loginResponse.Message = AppResources.TimeoutMessage;
                }
                catch (Exception ex)
                {
                    loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.Error;
                    loginResponse.Message = AppResources.ErrorMessage;
                }


            }
            else
            {
                loginResponse.ServiceResponse = AppBase.Enums.ServiceResponse.NotConnected;
                loginResponse.Message = AppResources.NotConnectedError;
            }

            return loginResponse;
        }
    }
}
