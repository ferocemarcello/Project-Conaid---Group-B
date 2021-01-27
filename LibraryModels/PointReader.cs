using System.Collections.Generic;
using System.Globalization;

namespace LibraryModels
{
    public class PointReader
    {
        public static List<Point> Read(string file1Path)
        {
            string[] lines = System.IO.File.ReadAllLines(file1Path);
            List<Point> points = Parse(lines);
            return points;
        }

        private static List<Point> Parse(string[] lines)
        {
            int count = 0;
            List<Point> points = new List<Point>();
            foreach (string s in lines)
            {
                if (!s.StartsWith("#"))
                {
                    string[] polarData = s.Split(' ');
                    double angle = double.Parse(polarData[0], CultureInfo.InvariantCulture);
                    double distance = double.Parse(polarData[1], CultureInfo.InvariantCulture);
                    // let's ignore quality for now
                    // string quality = polarData[2];
                    Point point = new Point(angle, distance);
                    points.Add(point);
                }
                else
                {
                    if (s.StartsWith("#COUNT"))
                    {
                        count = int.Parse(s.Substring(7));
                    }
                }
            }

            if (points.Count != count)
            {
                throw new System.Exception("Count and number of points don't match");
            }

            return points;
        }

        public static List<Point> Read(string[] lines)
        {
            List<Point> points = Parse(lines);
            return points;
        }
    }
}