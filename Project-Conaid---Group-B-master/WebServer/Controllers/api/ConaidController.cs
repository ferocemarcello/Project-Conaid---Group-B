using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using LibraryModels;
using WebServer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebServer.Controllers.api
{
    public class ConaidController : ApiController
    {
        [HttpGet] //Generates and returns a token
        public HttpResponseMessage GetToken(int buildingIndex, string password)
        {
            //Check if credentials are okay
            Security.VerifyUser(buildingIndex, password);
            //Save time in the beginning of token
            byte[] time = BitConverter.GetBytes(DateTime.Now.ToBinary());
            //Encrypt credentials using a private key
            char[] encryptedPassword = password.ToCharArray();
            char[] pKey = Utilities.tokenCipherKey.ToCharArray();
            int pKeyIndex = 0;
            for (int i = 0; i < encryptedPassword.Length; ++i) {
                encryptedPassword[i] = (char)(encryptedPassword[i]+pKey[pKeyIndex]);
                ++pKeyIndex;
                if (pKeyIndex == pKey.Length) pKeyIndex = 0;
            }
            byte[] key = Encoding.ASCII.GetBytes(encryptedPassword);
            //Append the buildingindex to the token
            byte[] key2 = BitConverter.GetBytes(buildingIndex);

            //Combine them into one byte array
            byte[] tokenData = new byte[time.Length + key.Length + key2.Length];
            Buffer.BlockCopy(time, 0, tokenData, 0, time.Length);
            Buffer.BlockCopy(key, 0, tokenData, time.Length, key.Length);
            Buffer.BlockCopy(key2, 0, tokenData, time.Length + key.Length, key2.Length);
            
            //Return to the body of the HTTP request
            return new HttpResponseMessage() {
                Content = new StringContent(Convert.ToBase64String(tokenData.ToArray()))
            };
        }

        [HttpPost]
        public void Create(string token)
        {
            if (Security.CheckToken(token))
            {
                int buildingIndex = Utilities.GetBuildingIndexFromToken(token);
                //Taking the content of the HttpPost request
                HttpContent hc = Request.Content;
                //take the http content as string[]
                string json = hc.ReadAsStringAsync().Result;
                //string[] lines = ((System.Collections.IEnumerable)hc).Cast<HttpContent>().Select(x => x.ToString()).ToArray();
                List<Reading> readings = JsonConvert.DeserializeObject<List<Reading>>(json);
                string path = @"./cart/" + buildingIndex;
                Directory.CreateDirectory(path);
                foreach (Reading reading in readings) {
                    List<Point> points = PointReader.Read(reading.Content);
                    points.ForEach(e => e.ToCartesian());
                    PointWriter.WriteCartesianToFile(points, path + "_" + reading.Id);
                }
            }
        }
    }
}