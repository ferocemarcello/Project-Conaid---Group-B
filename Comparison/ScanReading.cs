using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Comparison
{
    public class ScanReading : CustomFile
    {

        public List<ScannedPoint> Points = new List<ScannedPoint>();

        public ScanReading(string filePath) : base(filePath)
        {
            ReadFile();
            ParseScan(Content, Points, (float)1/(float)37.5);
        }

        private void ParseScan(string[] content, List<ScannedPoint> points, float scaling)
        {
            int count = 0;
            foreach (string s in content)
            {
                if (!s.StartsWith("#"))
                {
                    string[] polarData = s.Split(' ');
                    double angle = double.Parse(polarData[0], CultureInfo.InvariantCulture);
                    double distance = double.Parse(polarData[1], CultureInfo.InvariantCulture);
                    // let's ignore quality for now
                    // string quality = polarData[2];
                    Point point = new Point(angle, distance);
                    point.ToCartesian();
                    points.Add(new ScannedPoint(new Vector3((float)point.X * scaling, (float)point.Y * scaling, (float)point.Z * scaling)));
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
        }

    }
}
