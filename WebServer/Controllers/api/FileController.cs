using System.Collections.Generic;
using System.Web.Http;
using LibraryModels;
using WebServer.Models;

namespace WebServer.Controllers.api
{
    public class FileController : ApiController
    {
        private readonly Repository _repository = new Repository();

        // GET: api/File
        public IEnumerable<Reading> Get()
        {
            List<Reading> result = _repository.GetAllReadings();
            return result;
        }

        // GET: api/File/5
        public Reading Get(int id)
        {
            Reading reading = _repository.GetReading(id);
            reading.FillContent();
            return reading;
        }

        // POST: api/File
        public void Post([FromBody]Reading reading)
        {
            _repository.AddReading(reading);
        }

        // PUT: api/File/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/File/5
        public void Delete(int id)
        {
            _repository.RemoveReading(id);
        }
    }
}
