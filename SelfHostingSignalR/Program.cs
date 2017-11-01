using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using SelfHostingSignalR.SignalR;

namespace SelfHostingSignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server is running on: {url}");
                
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
