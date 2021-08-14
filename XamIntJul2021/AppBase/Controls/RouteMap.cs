using System;
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

    }
}
