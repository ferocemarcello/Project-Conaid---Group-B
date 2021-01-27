using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison
{
    public abstract class CustomFile : IDisposable
    {

        string filePath;
        string fileName;
        public string[] Content { get; set; }

        public CustomFile(string filePath)
        {
            this.filePath = filePath;
        }

        protected void ReadFile()
        {
            Content = System.IO.File.ReadAllLines(filePath + fileName);
        }

        public void Dispose()
        {
            Content = null;
        }

        protected void WriteFile()
        {
            System.IO.File.WriteAllLines(filePath + fileName, Content);
        }
    }
}
