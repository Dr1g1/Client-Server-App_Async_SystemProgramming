using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class WebServer
    {
        static HttpListener listener = new HttpListener();
        public async Task StartAsync()
        {
            listener.Prefixes.Add("http://localhost:5050/");
            listener.Start();
            Console.WriteLine("Server pokrenut na http://localhost:5050/");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                Task.Run(() => RequestHandler.ProcessRequestAsync(context));
            }
        }
    }
}

//HttpListenerContext context = listener.GetContext(); -> blokira.
