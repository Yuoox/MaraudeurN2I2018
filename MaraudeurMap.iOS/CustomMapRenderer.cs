using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using MapKit;
using MaraudeurMap.iOS;
using MaraudeurMap.Model;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MaraudeurMap.iOS {
    public class CustomMapRenderer : MapRenderer {
        UIView customPinView;
        List<CustomPin> customPins;
 

        protected override void OnElementChanged(ElementChangedEventArgs<View> e) {
            base.OnElementChanged(e);
            if (e.OldElement != null) {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null) {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }


        //Custom block of annotation
        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation) {
            MKAnnotationView annotationView = null;
            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null) {
                throw new Exception("Custom pin not found");
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
            if (annotationView == null) {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());

                //customPinImage
             //   UIImage customPinImage = this.FromUrl("https://cdn0.iconfinder.com/data/icons/internet-glyphs-vol-1/52/custom__map__pin__location__pinned__gps__marker-512.png");
                annotationView.Image = UIImage.FromFile("pin.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);

                //DisplayCountryImage
                var CountryImage = new UIImageView(new CGRect(0, 0, 84, 84));
                CountryImage.Image = this.FromUrl("https://www.countryflags.io/" + customPin.Country + "/flat/64.png");

                CountryImage.Frame = new CGRect(0, 0, 50, 50);
                annotationView.LeftCalloutAccessoryView = CountryImage;

                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKAnnotationView)annotationView).Id = customPin.Id.ToString();
                ((CustomMKAnnotationView)annotationView).Url = customPin.Url;

            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e) {
            var customView = e.View as CustomMKAnnotationView;
            if (!string.IsNullOrWhiteSpace(customView.Url)) {
                UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
            }
        }

        //display the top block
        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e) {
            var customView = e.View as CustomMKAnnotationView;

            customPinView = new UIView();
            customPinView.BackgroundColor = UIColor.White;
            customPinView.Frame = new CGRect(0, 0, 320, 84);

           // customPins
            var tempLabel = new UILabel(new CGRect(85, 0, 320, 42)) {
            //    Text = String.Concat("Température : ", 19)
            };
            customPinView.AddSubview(tempLabel);

            var InterestLabel = new UILabel(new CGRect(85, 42, 320, 42));
         //  InterestLabel.Text = "Lieu d'intérêt : Développement";
           // InterestLabel.Font = UIFont.ItalicSystemFontOfSize(14);
          //  customPinView.AddSubview(InterestLabel);

            customPinView.Center = new CGPoint(20, -(e.View.Frame.Height + 75));
            e.View.AddSubview(customPinView);
          
        }

        UIImage FromUrl(string uri) {
            if (uri != null) {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                    return UIImage.LoadFromData(data);
            }
            return null; 
        }


        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e) {
            if (!e.View.Selected) {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        CustomPin findCustomPinFromName(String name) {
            foreach(var pin in customPins) {
                if (pin.Label.Equals("name"))
                    return pin; 
            }
            return null; 
        }


        CustomPin GetCustomPin(MKPointAnnotation annotation) {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins) {
                if (pin.Position == position) {
                    return pin;
                }
            }
            return null;
        }
    }
}