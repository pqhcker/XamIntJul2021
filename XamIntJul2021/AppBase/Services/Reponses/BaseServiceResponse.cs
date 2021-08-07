using System;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Services.Reponses
{
    public abstract class BaseServiceResponse
    {
        public string Message { get; set; }
        public ServiceResponse ServiceResponse { get; set; }

    }
}
