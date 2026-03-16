using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class RequestHandler
    {
        static ConcurrentDictionary<string, string> cache = new ConcurrentDictionary<string, string>();
        static string rootFolder = @"D:\SisProgDrugiProj\Sis-prog-drugi-proj\ServerApp";
        public static async Task ProcessRequestAsync(object state)
        {
            var context = (HttpListenerContext)state;
            string filename = context.Request.Url.AbsolutePath.TrimStart('/');
            Logger.Log($"Zahtev za fajlom {filename}");
            if (cache.ContainsKey(filename))
            {
                Logger.Log($"Kesiran odgovor: {filename}");
                await SendResponseAsync(context, cache[filename]);
                return;
            }
            string filePath = FindFile(rootFolder, filename);
            if (filePath == null)
            {
                Logger.Log($"Fajl nije pronađen: {filename}");
                await SendErrorAsync(context, 404, "Fajl nije pronađen");
                return;
            }
            try
            {
                int count = await CountWordsAsync(filePath);
                string response = $"Broj reci koje pocinju velikim slovom, a duze su od 5 slova su: {count}";
                cache[filename] = response;
                Logger.Log($"Obradjen fajl: {filename} | Reci: {count}");
                await SendResponseAsync(context, response);
            }
            catch (Exception e)
            {
                Logger.Log($"Doslo je do greske {e.Message}");
                await SendErrorAsync(context, 500, "Greska u obradi");
            }
        }
        static string FindFile(string root, string targetFile)
        {
            foreach (var file in Directory.GetFiles(root, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(file).Equals(targetFile, StringComparison.OrdinalIgnoreCase))
                    return file;
            }
            return null;
        }
        static async Task<int> CountWordsAsync(string filePath)
        {
            int count = 0;
            string text = await File.ReadAllTextAsync(filePath);
            string[] words = text.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            //da ReadAllText ne blokira prilikom citanja nekih vecih fajlova.   
            foreach (var word in words)
            {
                if (word.Length > 5 && char.IsUpper(word[0]))
                    count++;
            }
            return count;
        }

        //da se ne bi blokiralo prilikom slanja odgovora i cekanja odgovora.
        static async Task SendResponseAsync(HttpListenerContext context, string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }
        static async Task SendErrorAsync(HttpListenerContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await SendResponseAsync(context, message);
        }
    }
}

//static int CountWords(string filePath) -> moze da bude async (primer sa vezbi).
//static void SendResponse(HttpListenerContext context, string message) -> moze da bude async (blokira).
//public static void ProcessRequest(object state) -> moze da bude async.



