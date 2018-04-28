using System;
using System.Collections.Generic;
using System.Text;

namespace Memories
{
    public class Configuration
    {
        public Boolean isOffline = false;
        private static string ApiKey = "f2f833-cf0e32-c1df0b-78ea82-c3c38f";
        public static string Server = "http://memories.ovh/api.php";
        public static string ServerImages = "http://memories.ovh/";

        public static string fichierInfosUser = "infos_user.json";

        public static string langue = "fr-FR";
    }
}
