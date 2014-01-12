using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;
using System.ServiceModel;
using System.Web.Http;

namespace Hibu.Sam.Concordance.WebApiServer
{
    class Program
    {
        const int BufferSize = 1024;
        const string BindingUrlFormat = "http://localhost:888/";

        // ConcordanceWebApiServer.exe "http://localhost:888/"
        // mabye need command? netsh http add iplisten ipaddress=0.0.0.0:888
        static void Main(string[] args)
        {
            if (args.Count() != 1)
            {
                DisplayUsageInfo();
                return;
            }

            HttpSelfHostServer server = null;
            try
            {
                string uri = args[0];

                HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(uri);
                config.HostNameComparisonMode = HostNameComparisonMode.Exact;

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                config.MaxReceivedMessageSize = 16L * 1024 * 1024 * 1024;
                config.ReceiveTimeout = TimeSpan.FromMinutes(5);
                config.TransferMode = TransferMode.StreamedRequest;

                server = new HttpSelfHostServer(config);

                server.OpenAsync().Wait();

                Console.WriteLine("Listening on " + uri);
                Console.WriteLine("Hit ENTER to stop and exit...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);

                DisplayUsageInfo();

                Console.WriteLine();
                Console.WriteLine("If you meet couldn't register URL error, pleases run this app with local administrator privilage or use command line like following format to register Reserved URL to system");
                Console.WriteLine("netsh http add urlacl url=http://localhost:888/ user=\\everyone listen=yes");
            }
            finally
            {
                if (server != null)
                {
                    server.CloseAsync().Wait();
                }
            }
        }

        private static void DisplayUsageInfo()
        {
            Console.WriteLine("Usage: ConcordanceWebApiServer.exe BindingUrl\n");
            Console.WriteLine("Sample as below:");
            Console.WriteLine("ConcordanceWebApiServer.exe \"{0}\"", BindingUrlFormat);
        }
    }
}
