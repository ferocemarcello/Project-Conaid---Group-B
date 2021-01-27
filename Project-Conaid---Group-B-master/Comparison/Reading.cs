using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Comparison
{
    public class Reading
    {
        public Reading()
        {
        }

        public Reading(string filePath)
        {
            FilePath = filePath;
        }

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