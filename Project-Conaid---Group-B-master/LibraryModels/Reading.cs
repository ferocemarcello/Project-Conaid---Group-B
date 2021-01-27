using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryModels
{
    public class Reading
    {
        public Reading()
        {
        }

        public Reading(int id, string filePath)
        {
            Id = id;
            FilePath = filePath;
        }

        public int Id { get; set; }
        public string FilePath { get; set; }
        public string[] Content { get; set; }

        public void FillContent()
        {
            IEnumerable<string> enumerable = File.ReadLines(FilePath);
            string[] content = enumerable.ToArray();
            Content = content;
        }
    }
}