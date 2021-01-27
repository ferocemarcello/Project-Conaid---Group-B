using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeFbx
{
    public class FromFBX
    {
        public static LinkedList<string> ReadVerticesFromFBX(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            LinkedList<string> vertices = new LinkedList<string>();

            string line;
            
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Contains("Vertices: *"))
                {
                    vertices.AddLast(sr.ReadLine());
                } 
            }
            return vertices;
        }

        public static LinkedList<Utility._3Dpoint> VerticesStringToPointList(string s)
        {
            LinkedList<Utility._3Dpoint> list = new LinkedList<Utility._3Dpoint>();
            //content type: \t\t\ta: p1.x, p1.y, p1.z, p2.x ...
            string [] tmp = s.Split(':');
            tmp = tmp[1].Split(',');

            for (int i = 0; i < tmp.Length; i+=3)
            {
                double[] point = new double[3];

                for (int j = 0; j < 3; j++)
                {
                    /*if (tmp[i + j].Contains("e"))
                    {
                        var a = tmp[i + j].Split('e');
                        double x = double.Parse(a[0]) * Math.Pow(10, double.Parse(a[1]));
                        point[j] = 0;
                    }
                    else*/
                        point[j] = double.Parse(tmp[i + j]);
                }


                list.AddLast(new Utility._3Dpoint(
                    point[0],point[1],point[2]
                    /*
                    double.Parse(tmp[i]),
                    double.Parse(tmp[i + 1]),
                    double.Parse(tmp[i + 2])*/
                    ));
            }
            return list;
        }
    }
}
