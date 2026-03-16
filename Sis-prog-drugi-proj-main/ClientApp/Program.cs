using System;
using System.Threading;

class ClientApp
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Unesite naziv fajla (npr. fajl.txt) ili 'exit' za izlaz:");
            string fileName = Console.ReadLine();

            if (fileName.ToLower() == "exit")
                break;

            await SendRequestAsync(fileName);
        }
    }
    static async Task SendRequestAsync(string fileName)
    {
        string url = $"http://localhost:5050/{fileName}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url); 
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[Task] Odgovor za '{fileName}':");
                Console.WriteLine(responseBody);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Task] Greška: {ex.Message}");
        }
    }

}