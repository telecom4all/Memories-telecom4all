using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MapKit;
using System.Diagnostics;

namespace Memories.iOS
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public string Url { get; set; }
       
        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            
            : base(annotation, id)
        {
            Debug.WriteLine("annotation = " + annotation.GetTitle() + "id = " + id.ToString());
        }
    }
}