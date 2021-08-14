using System;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using XamIntJul2021.AppBase.Controls;
using XamIntJul2021.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(RouteMap), typeof(RouteMapRenderer))]
namespace XamIntJul2021.Droid.CustomRenderers
{
    public class RouteMapRenderer : MapRenderer
    {
        public RouteMapRenderer(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (Control is not null)
                Control.GetMapAsync(this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(RouteMap.Route)))
            {
                var nativeMap = NativeMap;

                nativeMap.Clear();

                var formsMaps = Element as RouteMap;

                formsMaps.FinishRenderer();

                foreach (var colorPin in formsMaps.ColorPins)
                {
                    var marker = new MarkerOptions();

                    marker.SetTitle(colorPin.Label);
                    //marker.SetSnippet("Hola");
                    marker.SetPosition(new LatLng(colorPin.Position.Latitude, colorPin.Position.Longitude));
                    marker.SetIcon(GetColorMarkerIcon(colorPin.Color.ToAndroid()));
                    
                    nativeMap.AddMarker(marker).ShowInfoWindow();
                }
            }
        }

        private BitmapDescriptor GetColorMarkerIcon(Android.Graphics.Color color)
        {
            float[] hsv = new float[3];

            Android.Graphics.Color.ColorToHSV(color,hsv);

            return BitmapDescriptorFactory.DefaultMarker(hsv[0]);
        }
    }
}
