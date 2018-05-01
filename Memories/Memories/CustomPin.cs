using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Memories
{
    public class CustomPin : Pin
    {
        public string Date_enregistrement { get; set; }
        public string Date_modification { get; set; }

        public string Religion { get; set; }

        public string Icon { get; set; }
        public new string Id { get; set; }
        public string User_xamarin { get; set; }

        public string Id_xamarin{ get; set; }

        public string Url { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string Id_mysql { get; set; }
    }
}
