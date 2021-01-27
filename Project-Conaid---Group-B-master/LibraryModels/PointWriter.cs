using System.Collections.Generic;
using System.IO;

namespace LibraryModels
{
    public class PointWriter
    {
        public static void WriteCartesianToFile(List<Point> points, string path)
        {
            StreamWriter writer = File.CreateText(path);
            writer.AutoFlush = true;
            using (writer)
            {
                points.ForEach(p => writer.WriteLine(p.ToCartesianString()));
            }
        }
    }
}