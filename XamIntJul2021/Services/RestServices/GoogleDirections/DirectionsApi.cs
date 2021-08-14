using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamIntJul2021.AppBase.Services.RestService;
using XamIntJul2021.Services.Responses.GoogleDirections;

namespace XamIntJul2021.Services.RestServices.GoogleDirections
{
    public class DirectionsApi:BaseRestService
    {
        public DirectionsApi()
        {

        }

        public async Task<GoogleDirectionsResponse> GetDirectionsAsync(string origin, string destination)
        {
            InitHttpClient();

            string uri = string.Format(AppBase.Constants.GoogleDirections.URI,
                origin, destination, AppBase.Constants.GoogleDirections.APIKEY);

            using var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var route =
                    JsonConvert.DeserializeObject<GoogleDirectionsResponse>(await response.Content.ReadAsStringAsync());
                return route;
            }
            return null;

        }
    }
}
