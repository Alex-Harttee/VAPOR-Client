using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace VAPOR
{
    class PublicIP
    {
        public static String getPublicIP()
        {
            WebClient webClient = new WebClient();
            string publicIp = webClient.DownloadString("https://api.ipify.org");
            return publicIp;
        }
    }
}
