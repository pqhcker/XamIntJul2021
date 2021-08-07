using System;
using System.Threading.Tasks;
using XamIntJul2021.Model;
using XamIntJul2021.Services.Responses;

namespace XamIntJul2021.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginCredentials loginCredentials);
        Task<RegisterResponse> Register(User newUser);
        Task<UserInfoResponse> GetUserInfo();
    }
}
