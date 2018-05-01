using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;
using MapKit;
using Memories.iOS;
using Memories.mapRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Memories.iOS
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        List<CustomPin> customPins;


        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
               // nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
               // nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

      

        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {


            try
            {


//                Debug.WriteLine("ATTENTION!!! Anotation : " + annotation.ToString() + " mapView = " + mapView.ToString());


                MKAnnotationView annotationView = null;

                if (annotation is MKUserLocation) {
  //                  Debug.WriteLine("annotation is MKUserLocation");
                return null;
            }

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                    Debug.WriteLine("ATTENTION!!! customPin == null ");
                    throw new Exception("Custom pin not found");
            }

                Debug.WriteLine("--------------------------");
                Debug.WriteLine("customPin.Id.ToString():" + customPin.Id.ToString());
                Debug.WriteLine("--------------------------");
                annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
            if (annotationView == null)
            {
                    Debug.WriteLine("ATTENTION!!! customPin.Religion =  " + customPin.Religion + " customPin.Label = " + customPin.Label);

                    annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
                if (customPin.Religion == "Catholique")
                {
                    annotationView.Image = UIImage.FromFile("pin.png");
                }
                else {
                    annotationView.Image = UIImage.FromFile("pin.png");
                }
                   
                annotationView.CalloutOffset = new CGPoint(0, 0);

                if (customPin.Religion == "Catholique")
                {
                    annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("pin.png"));

                }
                else {
                    annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("pin.png"));
                }

                    Debug.WriteLine(" customPin.Id.ToString(); = " + customPin.Id.ToString());
                    Debug.WriteLine(" customPin.Url; = " + customPin.Url.ToString());

                    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKAnnotationView)annotationView).Id = customPin.Id_mysql.ToString();
             //   ((CustomMKAnnotationView)annotationView).Url = customPin.Url; 
            }
            annotationView.CanShowCallout = true;
                return annotationView;

            }
            catch (Exception e)
            {
                Debug.WriteLine("ATTENTION!!! Erreur : " + e.Message.ToString());
                
               throw;
            }
        }


        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            
            var customView = e.View as CustomMKAnnotationView;
            if (!string.IsNullOrWhiteSpace(customView.Url))
            {
               UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
            }
        }


        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            customPinView = new UIView();

          //  if (customView.Id == "Xamarin")
          //  {
                customPinView.Frame = new CGRect(0, 0, 200, 84);
                var image = new UIImageView(new CGRect(0, 0, 200, 84));
                image.Image = UIImage.FromFile("xamarin.png");
                customPinView.AddSubview(image);
                customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
                e.View.AddSubview(customPinView);
           // }
           // else {
           //     customPinView.Frame = new CGRect(0, 0, 200, 84);
           //     var image = new UIImageView(new CGRect(0, 0, 200, 84));
           //     image.Image = UIImage.FromFile("xamarin.png");
            //    customPinView.AddSubview(image);
            //    customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
            //    e.View.AddSubview(customPinView);
                // autre que id Xamarin
           // }
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
           // Debug.WriteLine("GetCustomPin " + annotation.Coordinate.ToString());

            Debug.WriteLine("customPins " + customPins.ToString());
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    Debug.WriteLine("return pin " + pin.Religion);
                    return pin;
                }
            }
            return null;
        }

    }
}
