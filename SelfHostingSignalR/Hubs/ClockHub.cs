using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNet.SignalR;

namespace SelfHostingSignalR.Hubs
{
    public class ClockHub : Hub
    {
        private Timer _timer;

        public override Task OnConnected()
        {
            Console.WriteLine($"Client connected");

            if (_timer == null)
            {
                _timer= new Timer(1000);
                _timer.Elapsed += (sender, args) =>
                {
                    Clients.All.showTime(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));
                };

                _timer.Start();
            }

            return base.OnConnected();
        } 

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine($"Client disconnected");

            return base.OnDisconnected(stopCalled);
        }
    }
}
