using System;
using System.ComponentModel;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using XamIntJul2021.AppBase.Controls;
using XamIntJul2021.iOS.CustomRenderers;

[assembly:ExportRenderer(typeof(RouteMap),typeof(RouteMapRenderer))]
namespace XamIntJul2021.iOS.CustomRenderers
{
    public class ColorPointAnnotation : MKPointAnnotation
    {
        public UIColor Color
        {
            get;
            private set;
        }
        public ColorPointAnnotation(UIColor color)
        {
            Color = color;
        }
    }

    public class RouteMapRenderer : MapRenderer
    {
        MKPolylineRenderer polylineRenderer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is not null)
            {
                var nativeMap = Control as MKMapView;

                if (nativeMap is not null && nativeMap.Overlays is not null)
                {
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    polylineRenderer = null;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(RouteMap.Route)))
            {
                var nativeMap = Control as MKMapView;
                nativeMap.RemoveAnnotations(nativeMap.Annotations);

                var formsMap = Element as RouteMap;

                formsMap.FinishRenderer();

                foreach (var colorPin in formsMap.ColorPins)
                {
                    var annotation = CreateAnnotation(colorPin);
                    nativeMap.AddAnnotation(annotation);
                }
            }
        }

        private IMKAnnotation CreateAnnotation(ColorPin colorPin)
        {
            var annotation = new ColorPointAnnotation(colorPin.Color.ToUIColor())
            {
                Title = colorPin.Label,
                Coordinate =
                new CoreLocation.CLLocationCoordinate2D(
                    colorPin.Position.Latitude,
                    colorPin.Position.Longitude)
            };

            return annotation;
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var colorAnnotation = annotation as ColorPointAnnotation;

            MKMarkerAnnotationView view = null;

            if (colorAnnotation is not null)
            {
                var identifier = "colorAnnotation";
                view = mapView.DequeueReusableAnnotation(identifier) as MKMarkerAnnotationView;

                if (view is null)
                    view = new(colorAnnotation, identifier);

                view.CanShowCallout = true;
                view.MarkerTintColor = colorAnnotation.Color;

                //Esto es para cambiar el icono, y se obtiene desde el Assest
                //Y tiene que ser un template image
                //view.GlyphImage
            }

            //return base.GetViewForAnnotation(mapView, annotation);
            return view;
        }

    }
}
