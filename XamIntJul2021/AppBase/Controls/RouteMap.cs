using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.AppBase.Controls
{
    public class RouteMap : Map
    {
        public static readonly BindableProperty UserLocationProperty
            = BindableProperty.Create(nameof(UserLocation), typeof(Position),
                typeof(RouteMap), null, propertyChanged: OnUserPositionChanged);

        public Position UserLocation {
            get => (Position)GetValue(UserLocationProperty);
            set => SetValue(UserLocationProperty,value);
        }

        public static readonly BindableProperty RouteProperty
            = BindableProperty.Create(nameof(Route), typeof(IEnumerable<Position>),
                typeof(RouteMap), null, propertyChanged: OnRouteChanged);

        public static readonly BindableProperty ColorsPinProperty
            = BindableProperty.Create(nameof(ColorPins), typeof(IEnumerable<ColorPin>),
                typeof(RouteMap), null);

        public IEnumerable<ColorPin> ColorPins
        {
            get => (IEnumerable<ColorPin>)GetValue(ColorsPinProperty);
            set => SetValue(ColorsPinProperty, value);
        }

        public IEnumerable<Position> Route
        {
            get => (IEnumerable<Position>)GetValue(RouteProperty);
            set => SetValue(RouteProperty, value);
        }

        private static void OnRouteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue is IEnumerable<Position> route)
            {
                Polyline polyline = new()
                {
                    StrokeColor = Color.Red,
                    StrokeWidth = 5
                };

                foreach (var coordinate in route)
                {
                    polyline.Geopath.Add(coordinate);
                }
                var map = bindable as Map;

                map.MapElements.Clear();
                map.MapElements.Add(polyline);

                var distance = Xamarin.Essentials.Location.CalculateDistance(
                    new(route.First().Latitude, route.First().Longitude),
                    new(route.Last().Latitude, route.Last().Longitude),
                    Xamarin.Essentials.DistanceUnits.Kilometers
                    );
                map.MoveToRegion(MapSpan.FromCenterAndRadius(route.First(),
                    Distance.FromKilometers(distance)));
            }
        }

        private static void OnUserPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Pin pin = new()
            {
                Label = AppResources.CurrentLocationLabel,
                Position= (Position)newValue
            };

            Map map = bindable as Map;

            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius((Position)newValue, Distance.FromKilometers(3)));
        }

        public void FinishRenderer()
        {
            ColorPins = new List<ColorPin>();
            (ColorPins as IList).Add(new ColorPin()
            {
                Label = AppResources.OriginPinLabel,
                Position = Route.First(),
                Color = Color.Blue
            });
            (ColorPins as IList).Add(new ColorPin()
            {
                Label = AppResources.DestinationPinLabel,
                Position = Route.Last(),
                Color = Color.Yellow
            });
        }

    }

    public class ColorPin
    {
        public Color Color { get; set; }
        public string Label { get; set; }
        public Position Position { get; set; }

    }
}
