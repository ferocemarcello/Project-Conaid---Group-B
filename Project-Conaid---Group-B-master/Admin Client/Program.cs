using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;
using LibraryModels;

namespace Admin_Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            while(true)
            {
                List<Reading> readings;

                _populateReadings(out readings);
                _displayReadings(readings);
                int selectedId = _selectReadingToDownload(readings);
                Reading reading = new Reading { Id = selectedId };
                _downloadReadingContent(reading).Wait();

                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        private void _displayReadings(List<Reading> readings)
        {
            foreach(Reading reading in readings)
            {
                Console.WriteLine($"{reading.Id}\t{reading.FilePath}\t");
                Console.WriteLine("-----------------------------------------------------------");
            }
        }

        private void _populateReadings(out List<Reading> readings)
        {
            readings = _performGetRequest<List<Reading>>("api/File").Result;
        }

        public int _selectReadingToDownload(List<Reading> readings)
        {
            int selectedReadingId;
            Console.WriteLine("\n\nInsert ID of a file you want to download: ");
            int.TryParse(Console.ReadLine(), out selectedReadingId);

            if(selectedReadingId > -1 && selectedReadingId < readings.Count)
            {
                return selectedReadingId;
            }
            else
            {
                Console.WriteLine("This file does not exist");
                throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<Reading> _downloadReadingContent(Reading reading)
        {
            reading = await _performGetRequest<Reading>("api/File", reading.Id.ToString());
            Console.WriteLine("Your file has been downloaded!");
            SaveFile(reading);
            Console.WriteLine("Your file has been saved!");
            return reading;
        }

        private void SaveFile(Reading reading)
        {
            string name = reading.FilePath.Substring(reading.FilePath.LastIndexOf('\\') + 1);
            File.WriteAllLines(name, reading.Content);
            // the moment we save to text file we can scrap the reading content
            reading = null;
        }

        private async Task<T> _performGetRequest<T>(string uri, string param = "")
        {
            HttpClient client;
            using(client = _setClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{uri}/{param}");
                Task<T> content = response.Content.ReadAsAsync<T>();
                return content.Result;
            }
        }
        private async void _performPostRequest<T>(string uri, T content)
        {
            HttpClient client;
            using(client = _setClient())
            {
                HttpContent httpcontent = new ObjectContent(typeof(T), content, new JsonMediaTypeFormatter());
                await client.PostAsync(uri, httpcontent);
            }
        }
        private HttpClient _setClient()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:15195") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}