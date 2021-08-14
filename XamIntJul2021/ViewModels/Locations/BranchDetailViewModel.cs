using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.Model;
using XamIntJul2021.Services.RestServices.GoogleDirections;
using XamIntJul2021.Services.RestServices.GoogleDirections.Entities;

namespace XamIntJul2021.ViewModels.Locations
{
    public class BranchDetailViewModel : BaseViewModel
    {
        public BranchDetailViewModel()
        {
            Title = AppResources.BranchDetailTitle;
            PageId = AppBase.Constants.PageIds.BRANCH_DETAIL;
            OnAppearingCommnad = new Command(async () => await GetCurrentLocation());
            ShowRouteCommand = new(async () => await ShowRoute());
        }

        private async Task ShowRoute()
        {
            var placeMarks =
                await Geocoding.GetPlacemarksAsync(currentLocation.Latitude, currentLocation.Longitude);

            string origin;
            if(placeMarks is not null)
            {
                var placeMark = placeMarks.First();
                origin = $"{placeMark.FeatureName} {placeMark.SubLocality} {placeMark.Locality}, {placeMark.CountryName}, {placeMark.PostalCode}";
            }
            else
            {
                origin = $"{currentLocation.Latitude},{currentLocation.Longitude}";
            }

            string destination = Location;

            var directionsClient = new DirectionsApi();

            var directions = await directionsClient.GetDirectionsAsync(origin, destination);

            var steps = directions?.routes?.FirstOrDefault().legs?.FirstOrDefault()?.steps;

            List<Position> route = new();

            foreach(var step in steps)
            {
                var decodedPoint = GooglePoints.Decode(step.polyline?.points);
                foreach (var point in decodedPoint)
                {
                    route.Add(new(point.Latitude, point.Longitude));
                }
            }

            Route = route;

        }

        private async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30));
                
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    CurrentLocation = new(location.Latitude, location.Longitude);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.GetLocationErrorTitle, AppResources.GetLocationNotSupportedError, AppResources.AcceptButton);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.GetLocationErrorTitle, AppResources.GetLocationNotEnabledError, AppResources.AcceptButton);
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.GetLocationErrorTitle, AppResources.GetLocationPermissionError, AppResources.AcceptButton);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.GetLocationErrorTitle, AppResources.GetLocationNotSupportedError, AppResources.AcceptButton);
            }
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            var branch = navigationParameters[AppBase.Constants.Parameters.BRANCH] as Branch;
            Name = branch.Name;
            Location = branch.Location;
        }

        private string name;
        private string location;
        private Position currentLocation;
        private IEnumerable<Position> route;

        public String Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public String Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public Position CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }


        public IEnumerable<Position> Route
        {
            get => route;
            set => SetProperty(ref route, value);
        }

        public Command ShowRouteCommand { get; set; }
    }
}
