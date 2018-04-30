using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Memories
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPin : ContentPage
	{
        public DetailPin(CustomPin Pin)
        {
            InitializeComponent();

            Debug.WriteLine("BindingContext");

            label_IdXaml.Text = Pin.Label;
            label2_IdXaml.Text = Pin.Label;

            icon_IdXaml.Source = ImageSource.FromUri(new Uri(Configuration.ServerImages + "images/" + Pin.Icon));
            icon2_IdXaml.Text = Pin.Icon;

            id_xamarin_IdXaml.Text = Pin.Id_xamarin;
            longitude_IdXaml.Text = Pin.Longitude;
            latitude_IdXaml.Text = Pin.Latitude;

            address_IdXaml.Text = Pin.Address;
            url_IdXaml.Text = Pin.Url;

            user_xamarin_IdXaml.Text = Pin.User_xamarin;

            religion_IdXaml.Text = Pin.Religion;

            date_enregistrement_IdXaml.Text = Pin.Date_enregistrement;
            date_modification_IdXaml.Text = Pin.Date_modification;
         //   50.52476,3.861273

            // string addressStreetView = "<iframe src='https://www.google.com/maps/embed?pb=!4v1524746222082!6m8!1m7!1sZtSyQD8uhD1DvF5qMtHLUg!2m2!1d50.46737151769455!2d4.435777953117031!3f148.5292376411138!4f-36.433779951598126!5f0.7820865974627469' width='600' height='450' frameborder='0' style='border:0' allowfullscreen ></ iframe >";
            //string addressStreetView = "<iframe src='https://www.google.com/maps/embed?pb=!4v1524746222082!6m8!1m7!1sZtSyQD8uhD1DvF5qMtHLUg!2m2!1d"+ Pin.Latitude + "!2d" + Pin.Londitude + "!3f148.5292376411138!4f-36.433779951598126!5f0.7820865974627469' width='600' height='450' frameborder='0' style='border:0' allowfullscreen ></ iframe >";
            //string addressStreetView = "<iframe src='https://www.google.com/maps/embed/v1/streetview?key=AIzaSyDHXtcI2e-x2zJE5ukrSsa7BiXUEvLc6jo&location=" + Pin.Latitude + "!2d" + Pin.Londitude + "&heading=210&pitch=10&fov=35'></ iframe >";
            string addressStreetView = "<iframe src='https://www.google.com/maps/@50.52476,3.861273,86a,35y,2.62h,45.02t/data=!3m1!1e3'></ iframe >";

/*
            streetView.Source = new HtmlWebViewSource
            {
                Html = addressStreetView
            };*/
            /*https://www.google.com/maps/embed/v1/streetview?key=AIzaSyDHXtcI2e-x2zJE5ukrSsa7BiXUEvLc6jo&location=46.414382,10.013988&heading=210&pitch=10&fov=35
     */

            streetView.Source = "https://www.google.com/maps/@" + Pin.Latitude + "," + Pin.Longitude + ",3a,60y,49.34h,143.42t/data=!3m6!1e1!3m4!1su5DgmDWGpVaiLgY22hj_9A!2e0!7i13312!8i6656";

           
            Debug.WriteLine("https://www.google.com/maps/@" + Pin.Latitude + "," + Pin.Longitude + ",3a,60y,49.34h,143.42t/data=!3m6!1e1!3m4!1su5DgmDWGpVaiLgY22hj_9A!2e0!7i13312!8i6656");
            //  streetView.Source = "https://www.google.com/maps/@" + Pin.Latitude + "," + Pin.Londitude + ",3a,75y,148.53h,53.57t/data=!3m6!1e1!3m4!1sZtSyQD8uhD1DvF5qMtHLUg!2e0!7i13312!8i6656";
            //            streetView.Source = "https://maps.googleapis.com/maps/api/streetview?size=400x200&location=" + Pin.Latitude + "," + Pin.Londitude + "&fov=90&heading=235&pitch=10&key=AIzaSyDoER8npcjFlhBVygmd2Ec9Sc6PNSjSg5Y";


            

            // DependencyService.Get<IStreetViewService>().openStreetView(Convert.ToDouble(Pin.Latitude, CultureInfo.GetCultureInfo("en-us")), Convert.ToDouble(Pin.Longitude, CultureInfo.GetCultureInfo("en-us")));

        }
    }
}