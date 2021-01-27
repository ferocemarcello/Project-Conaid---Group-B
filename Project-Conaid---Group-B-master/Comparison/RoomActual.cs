using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Comparison
{
    public class RoomActual
    {
        List<Polyhedra> objects = new List<Polyhedra>();
        double delta;
        string fbxFileName;
        float scaleFactor;

        public RoomActual(string filename, double delta, float scaleFactor)
        {
            this.scaleFactor = scaleFactor;
            this.fbxFileName = filename;
            this.delta = delta;
            List<string> tmp = ReadVerticesFromFBX();
            InitializePolyhedras(tmp);
        }

        public List<string> ReadVerticesFromFBX()
        {
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\" +fbxFileName);
            List<string> vertices = new List<string>();

            string line;
            string previousLine="";

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Contains("Vertices: *"))
                {
                    bool pointsRemaining = true;
                    string toAdd = "";
                    while (pointsRemaining)
                    {
                        string tmp = sr.ReadLine();
                        if (!tmp.Contains("}"))
                            toAdd += tmp;
                        else
                            pointsRemaining = false;
                    }
                    vertices.Add(previousLine+"°"+ toAdd);
                }
                previousLine = line;
            }
            return vertices;
        }

        public void InitializePolyhedras(List<string> vertices)
        {
            foreach (string s in vertices)
            {

                List<Vector3> list = new List<Vector3>();
                //content type: name°\t\t\ta: p1.x, p1.y, p1.z, p2.x ...
                string[] tmp = s.Split('°');
                string name = tmp[0];
                tmp = tmp[1].Split(':');
                tmp = tmp[1].Split(',');

                for (int i = 0; i < tmp.Length; i += 3)
                {
                    list.Add(new Vector3(float.Parse(tmp[i]) * scaleFactor, float.Parse(tmp[i + 1]) * scaleFactor, float.Parse(tmp[i + 2]) * scaleFactor));
                }
                objects.Add(new Polyhedra(list, delta, name));
            }
        }

        public double Compare (RoomScan scan)
        {
            int counter = 0;
            
            foreach (ScannedPoint point in scan.AllPoints)
            {
                foreach (Polyhedra poly in objects)
                {
                    if (poly.IsInsideDelta(point.point))
                    {
                        point.HasCollidedWith(poly);
                        counter++;
                    }
                }
            }
            return (float)((counter / (float)(scan.AllPoints.Count)) * 100);
        }

    }
}
