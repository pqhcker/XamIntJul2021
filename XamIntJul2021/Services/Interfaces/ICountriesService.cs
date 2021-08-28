using System;
using System.Threading.Tasks;
using XamIntJul2021.Services.Responses;

namespace XamIntJul2021.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<CountriesResponse> GetCountries();
    }
}
