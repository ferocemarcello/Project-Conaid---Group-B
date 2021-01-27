using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Comparison;
using System.IO;
using System.Numerics;

namespace WebServer.Controllers.api
{
    public class FBXController : ApiController
    {
        #region api
        // GET: api/FBX
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FBX/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FBX
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FBX/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FBX/5
        public void Delete(int id)
        {
        }

        #endregion

        public double ComparScanToActual(string fbxFilename, string roomName, double delta, float scaleFactor, Vector3 translation)
        {
            string appDataFolder = @"App_Data\";
            //Convert FBX from binary to ASCII
            //FBX_converter.PythonRunner.RunPythonScript(Directory.GetCurrentDirectory() + @"\App_Data\PythonScripts\converter.py", fbxFilename);
            //Create RoomActual
            Comparison.RoomActual roomActual= new RoomActual(appDataFolder+@"FBXFiles/ASCII/"+fbxFilename, delta, scaleFactor);
            //Create RoomScan
            RoomScan roomScan = new RoomScan(appDataFolder +@"Scans/"+roomName);
            roomScan.Translate(translation);
            
            //Collision
            double perc = roomActual.Compare(roomScan);
            roomScan.SaveToTextFile("file");
            roomScan.SaveToFBXFile("file");
            return perc;
        }
    }
}
