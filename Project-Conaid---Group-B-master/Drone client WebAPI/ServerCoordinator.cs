using System;
using System.Net.Http;
using System.Net.Http.Headers;
using LibraryModels;
using System.Collections.Generic;
using System.Linq;

namespace Drone_client_WebAPI {
    class ServerCoordinator {
        
        private static HttpClient client = new HttpClient();
        private static bool connection = false;
        private static string token;

        public static async void ConnectToServer(int buildingIndex, string password) {
            if (connection) {
                byte[] tokenData = Convert.FromBase64String(token);
                if (DateTime.FromBinary(BitConverter.ToInt64(tokenData.Take(8).ToArray(), 0)) < DateTime.Now.AddHours(-24)) {
                    connection = false;
                }
            }
            if (!connection) {
                InitConnection("http://localhost:15195/", "application/json");
                token = await client.GetStringAsync("api/Conaid/?buildingIndex="+buildingIndex+"&password="+password);
                connection = true;
            }

        }
        private static void InitConnection(string baseAddress, string format) {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(format));
        }
        public static async void PostReadingsAsync(List<Reading> readings)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Conaid/?token="+token, readings);
            response.EnsureSuccessStatusCode();
        }

    }
}
