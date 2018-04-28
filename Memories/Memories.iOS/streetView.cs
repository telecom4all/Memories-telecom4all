using Memories.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
[assembly: Dependency(typeof(iOsStreetViewService))]
namespace Memories.iOS
{
    public class iOsStreetViewService : IStreetViewService
    {
        public iOsStreetViewService()
        {

        }

        public void openStreetView(double latitude, double longitude)
        {
            if (UIApplication.SharedApplication.CanOpenUrl(new Foundation.NSUrl("comgooglemaps://")))
                UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl("comgooglemaps://?center=" + latitude + "," + longitude + "&mapmode=streetview"));
            else
            {

                //Log.e("dasd", "Google maps not supported");
                UIAlertView _error = new UIAlertView("Opps", "Please install google maps to access this feature.", null, "Okay", null);
                _error.Show();
            }
            
        }
    }
}
