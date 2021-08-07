using System;
using XamIntJul2021.AppBase.Services.Reponses;

namespace XamIntJul2021.Services.Responses
{
    public class LoginResponse : BaseServiceResponse
    {
        public string AccessToken { get; set; }
    }
}
