using System.Net.Sockets;
using System.Net;

namespace WebServer.Services
{

    public class IPWhitelistOptions
    {
        public List<string> Whitelist { get; set; }


        //helper method
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

}
