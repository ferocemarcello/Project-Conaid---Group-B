using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LibraryModels;

namespace WebServer.Models
{
    public class Repository
    {
        private readonly List<Reading> _readings = new List<Reading>();

        public Repository()
        {
            _getReadings();
        }

        private void _getReadings()
        {
            string filesPath = HttpContext.Current.Server.MapPath("~/App_Data/Files");
            string[] allFiles = Directory.GetFiles(filesPath);

            for (int i = 0; i < allFiles.Length; i++)
            {
                Reading reading = new Reading(i, allFiles[i]);
                AddReading(reading);
            }
        }

        public Reading GetReading(int id)
        {
            return _readings.Find(ele => ele.Id == id);
        }

        public void AddReading(Reading reading)
        {
            _readings.Add(reading);
        }

        public List<Reading> GetAllReadings()
        {
            return _readings;
        }

        public void RemoveReading(int id)
        {
            _readings.RemoveAll(ele => ele.Id == id);
        }
    }
}