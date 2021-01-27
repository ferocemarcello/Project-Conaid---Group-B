using AnalyzeFbx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeFBX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Analyze FBX");

            string filename = "file1.fbx";

            LinkedList<string> tmp = FromFBX.ReadVerticesFromFBX(filename);
            LinkedList<LinkedList<Utility._3Dpoint>> points = new LinkedList<LinkedList<Utility._3Dpoint>>();


            foreach (string s in tmp)
            {
                points.AddLast(FromFBX.VerticesStringToPointList(s));
            }

            StreamWriter sw = new StreamWriter("file1.txt");
            LinkedList<Utility._3Dpoint> forOccourrence = new LinkedList<Utility._3Dpoint>();

            List<Polyhedra> pointMeshes = new List<Polyhedra>();

            foreach (LinkedList<Utility._3Dpoint> pointList in points)
            {
                pointMeshes.Add(new Polyhedra(pointList, 0.1));
                //forOccourrence = new LinkedList<Utility._3Dpoint>(forOccourrence.Concat(pointList).ToList());
            }

            /*
            LinkedList<Utility._3Dpoint> repetitions = VerticesAnalyzier.OccourrenceMoreThanOne(forOccourrence);

            foreach (Utility._3Dpoint p in repetitions)
            {
                sw.WriteLine(p.PointToString());
            }

            sw.Flush();
            sw.Close();

            /*
            string result = FBX_converter.PythonRunner.RunPythonScript("mainCubes.py", "file1");
            Console.WriteLine(result);
            */
            Console.ReadKey();
        }
    }
}
