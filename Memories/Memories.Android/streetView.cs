using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Memories.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidStreetViewService))]
namespace Memories.Droid
{
    public class DroidStreetViewService : IStreetViewService
    {

        public void openStreetView(double latitude, double longitude)
        {
            Android.Net.Uri gmmIntentUri = Android.Net.Uri.Parse("google.streetview:cbll=" + latitude + "," + longitude);
            Intent mapIntent = new Intent(Intent.ActionView, gmmIntentUri);
            mapIntent.SetPackage("com.google.android.apps.maps");
            Forms.Context.StartActivity(mapIntent);

        }
    }
}