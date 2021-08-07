using System;
using XamIntJul2021.AppBase.Services.Reponses;
using XamIntJul2021.Model;

namespace XamIntJul2021.Services.Responses
{
    public class UserInfoResponse : BaseServiceResponse
    {
        public User User { get; set; }
    }
}
