using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;

namespace ServerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WebServer server = new WebServer();
            await server.StartAsync();
        }
    }
}


//Taskove koristimo kada je potrebno odraditi vremenski i procesorski zahtevne operacije, ali bez da blokiramo
//nit koja je pozvala na izvrsenje te operacije.
//Posmatramo metode koje mogu da blokiraju nit i menjamo ih koristeci await/async.