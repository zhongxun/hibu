using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Hibu.Sam.Concordance.Client
{
    class Program
    {
        const int BufferSize = 1024;
        const string ApiFormat = @"http://localhost:888/api/concordance/";
        const string FileFormat = @"C:\code\The Raven.txt";

        //" ConcordanceClient.exe "http://localhost:888/api/concordance" "C:\code\The Raven.txt"
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                DisplayUsageInfo();
                return;
            }

            try
            {
                string uri = args[0];
                string filename = args[1];

                Console.WriteLine("Web api address: {0}", uri);
                Console.WriteLine("Local file path: {0}", filename);

                RunClient(uri, filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error happens when trying to post file: {0}", e.GetBaseException().Message);
            }
        }

        private static void DisplayUsageInfo()
        {
            Console.WriteLine("Usage: ConcordanceClient.exe ApiAddress LocalFilePath\n");
            Console.WriteLine("Sample as below:");
            Console.WriteLine("ConcordanceClient.exe \"{0}\" \"{1}\"", ApiFormat, FileFormat);
        }

        private static void RunClient(string uri, string filename)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);

            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize))
            {
                StreamContent content = new StreamContent(fileStream, BufferSize);
                Uri address = new Uri(uri);

                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent("submit"), "sumitter");
                formData.Add(content, "filename", filename);

                HttpResponseMessage response = client.PostAsync(address, formData).Result;

                Console.WriteLine("Response from server:\n{0}", response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
