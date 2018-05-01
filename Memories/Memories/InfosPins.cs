using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memories
{
    public class InfosPinsClass
    {
        public struct InfosPinStruct
        {
            public string id { get; set; }
            public string id_xamarin { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string label { get; set; }
            public string address { get; set; }
            public string url { get; set; }
            public string user_xamarin { get; set; }
            public string religion { get; set; }
            public string date_enregistrement { get; set; }
            public string date_modification { get; set; }
            public string icon { get; set; }
            public string id_mysql { get; set; }
        }
        public static InfosPinStruct ListInfosPinJson;
    }
}
