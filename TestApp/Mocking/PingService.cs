using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace TestApp.Mocking
{
    public interface IPing
    {
        bool IsNetworkAvailable(string address);
    }

    // Adapter
    public class StandardPing : IPing
    {
        // Adaptee
        private Ping ping = new Ping();

        public bool IsNetworkAvailable(string address)
        {
            return ping.Send(address).Status == IPStatus.Success;
        }
    }
    public class ApiService
    {
        private readonly IPing ping;

        public ApiService(IPing ping)
        {
            this.ping = ping;
        }

        public string Send(string address, string message)
        {
           // Ping ping = new Ping();
            
            if (ping.IsNetworkAvailable(address))
            {
                Trace.WriteLine($"Send {message}");

                return "Pong";
            }

            else
            {
                Trace.WriteLine("Network failer");

                throw new NetworkInformationException();
            }

            

        }
    }
     
   
    public class InstallerHelper
    {
        private string _setupDestinationFile;

        public bool DownloadInstaller(string customerName, string installerName)
        {
            var client = new WebClient();
            try
            {
                client.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
