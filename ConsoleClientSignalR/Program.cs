using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace ConsoleClientSignalR
{
    class Program
    {
        private static int _counter;

        static void Main(string[] args)
        {
            var url = "http://localhost:8080";
            var completeEvent = new ManualResetEvent(false);

            var connection = new HubConnection(url);
            connection.StateChanged += change => Console.WriteLine($"Connection state changed.  New state: {change.NewState}");
            connection.ConnectionSlow += () => Console.WriteLine("Warning.  Connection is slow");

            var hub = connection.CreateHubProxy("clockHub");
            hub.On<string>("showTime", s =>
            {
                _counter++;
                Console.WriteLine($"{_counter}: ClockHub.ShowTime({s}) event");

                if (_counter >= 10) completeEvent.Set();
            });


            connection.Start();

            Console.WriteLine("Started.  Waiting for 10 events...or 30 sec timeout");
            if (!completeEvent.WaitOne(30000)) Console.WriteLine("Timeout while waiting for ClockHub.ShowTime() events");

            connection.Stop();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
