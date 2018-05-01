using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using Memories.mapRenderer;
using System.Collections.Generic;



// API MAPS = AIzaSyDHXtcI2e-x2zJE5ukrSsa7BiXUEvLc6jo
// API GEOCODING = AIzaSyDoER8npcjFlhBVygmd2Ec9Sc6PNSjSg5Y
// API GEOLOCALISATION = AIzaSyCeZTID3TrcJFIJiu8pblzkLUjNMh-m5Zo

namespace Memories.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartePage : ContentPage
	{
        public IGeolocator locator = CrossGeolocator.Current;
        Map map;
        private Xamarin.Forms.Maps.Position _position;
        public ObservableCollection<InfosPinsClass.InfosPinStruct> InfosPinList = new ObservableCollection<InfosPinsClass.InfosPinStruct>();

        public CartePage ()
		{
			InitializeComponent ();


            

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CheckGPS();
        }
        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public async Task CheckGPS()
        {
            
            if (IsLocationAvailable())
            {
               await  FindPinsAsync();
            }
            else
            {
               await DisplayAlert("Attention GPS Désativé", "GPS désactivé, merci d'ativé votre GPS sur l'appareil.", "OK");
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {
                   // Debug.WriteLine("timer");
                    if (IsLocationAvailable())
                    {
                        Tools.AsyncHelper.RunSync(() => FindPinsAsync());
                  
                        
                        return false;
                    }
                    return true;
                });
            }
        }


        public async void GetPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locatore = CrossGeolocator.Current;
                locatore.DesiredAccuracy = 100;

                position = await locatore.GetLastKnownLocationAsync();

                if (position != null)
                {
                    _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                   // Debug.WriteLine("_position != null Long:" + _position.Longitude.ToString() + " lat: " + _position.Latitude.ToString());
                    //got a cahched position, so let's use it.
                    return;
                }

                if (!locatore.IsGeolocationAvailable || !locatore.IsGeolocationEnabled)
                {
                    //not available or enabled
                    await DisplayAlert("Attention GPS Désativé", "GPS désactivé, merci d'ativé votre GPS sur l'appareil.", "OK");
                    return;
                }

                position = await locatore.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

                _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                if (position == null)
                    return;

                Debug.WriteLine("Position Long:" + position.Longitude.ToString() + " lat: " + position.Latitude.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
                //Display error as we have timed out or can't get location.
            }
            
            
        }

        public async Task FindPinsAsync()
        {
            using (HttpClient http = new HttpClient())
            {
                try { 
                    var response = await http.GetAsync("http://memories.ovh/api.php?api=84bdba-d214e3-38c29e-701e1e-1634ed&action=list-pins-all");
                    // Debug.WriteLine(response);
                    if ((int)response.StatusCode == 200)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //  Debug.WriteLine(data.ToString());
                        try
                        {
                            StatusReq status = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusReq>(data);
                            if (status.status == true)
                            {
                                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data).First.Next;
                                //  Debug.WriteLine("type = " + jsonObj.GetType());


                                DoCarte(jsonObj);
                            }

                        }
                        catch (Exception e)
                        {

                            Debug.WriteLine(e.Message);
                            await DisplayAlert("Erreur", e.Message.ToString(), "Ok");
                        }


                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.Message);
                    await DisplayAlert("Erreur", err.Message.ToString(), "Ok");
                    throw;
                }
            }
        }


        public void PinClicked(object sender, EventArgs e, CustomPin pin)
        {
            var DetailPin = new DetailPin(pin);
            Navigation.PushAsync(DetailPin);

        }
       

        public void DoCarte(Newtonsoft.Json.Linq.JToken jsonObj)
            {
            
           // Debug.WriteLine("Do Carte = " + jsonObj.ToString());
            GetPosition();
          //  Debug.WriteLine("Position Long:" + _position.Longitude.ToString() + " lat: " + _position.Latitude.ToString());
            var map = new CustomMap
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(_position.Latitude, _position.Latitude), Distance.FromMiles(0.3));

      
      
            map.CustomPins = new List<CustomPin> {  };




            
            foreach (var item in jsonObj.First)
            {
                InfosPinsClass.InfosPinStruct lignePin = (InfosPinsClass.InfosPinStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString(), typeof(InfosPinsClass.InfosPinStruct));
                int i = 0;
               
                if (InfosPinList.Count > 0)
                {
                    for (i = 0; i < InfosPinList.Count; i++)
                    {
                         Debug.WriteLine("nb item = " + i);
                        Debug.WriteLine("ID MYSQL = " + lignePin.id.ToString());
                    }
                } 
                
                InfosPinList.Insert(i, new InfosPinsClass.InfosPinStruct()
                {

                    id = lignePin.id,
                    id_xamarin = lignePin.id_xamarin,
                    longitude = lignePin.longitude,
                    latitude = lignePin.latitude,
                    label = lignePin.label,
                    address = lignePin.address,
                    url = lignePin.url,
                    user_xamarin = lignePin.user_xamarin,
                    religion = lignePin.religion,
                    icon = lignePin.icon,
                    id_mysql = lignePin.id,
                    date_enregistrement = lignePin.date_enregistrement,
                    date_modification = lignePin.date_modification


                });

         

                var pin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(lignePin.latitude, CultureInfo.GetCultureInfo("en-us")), Convert.ToDouble(lignePin.longitude, CultureInfo.GetCultureInfo("en-us"))),
                    Label =  lignePin.label,
                    Address = lignePin.address,
                    Id = lignePin.id,
                    Id_xamarin = lignePin.id_xamarin,
                    Url = lignePin.url,
                    Icon = lignePin.icon,
                    Id_mysql = lignePin.id,
                    User_xamarin = lignePin.user_xamarin,
                   Religion = lignePin.religion,
                    Date_enregistrement = lignePin.date_enregistrement,
                    Date_modification = lignePin.date_modification,
                    Longitude = lignePin.longitude,
                    Latitude = lignePin.latitude
                };

                pin.Clicked += (sender, e) => { PinClicked(sender, e, pin); };
               // map.Pins.Add(pin);

               map.CustomPins.Add(pin);

                


                Debug.WriteLine("Address = " + pin.Address.ToString());
                Debug.WriteLine("Date_enregistrement = " + pin.Date_enregistrement.ToString());
                Debug.WriteLine("Date_modification = " + pin.Date_modification.ToString());
                Debug.WriteLine("Icon = " + pin.Icon.ToString());

              

                 Debug.WriteLine("IdMysql = " + pin.Id_mysql.ToString());
                Debug.WriteLine("Id_xamarin = " + pin.Id_xamarin.ToString());
                Debug.WriteLine("Label = " + pin.Label.ToString());
                Debug.WriteLine("Latitude = " + pin.Latitude.ToString());
                Debug.WriteLine("Longitude = " + pin.Longitude.ToString());
                Debug.WriteLine("Position = " + pin.Position.ToString());
                Debug.WriteLine("Religion = " + pin.Religion.ToString());
                Debug.WriteLine("Type = " + pin.Type.ToString());
                Debug.WriteLine("Url = " + pin.Url.ToString());
                Debug.WriteLine("User_xamarin = " + pin.User_xamarin.ToString());
                

                map.Pins.Add(pin);
               

            }

            
            map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromMiles(1)));
            map.MapType = MapType.Street;
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
} 



/* void prvPage(object s, EventArgs e)
         {
             Navigation.PopAsync();
         }*/
