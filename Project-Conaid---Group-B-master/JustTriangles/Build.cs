using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace JustTriangles
{
    public class Build
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("1. Triangle Generator");
            Console.WriteLine("2. Rand Cube Generator");
            Console.WriteLine("3. Simple Cube Generator");
            switch (Convert.ToInt32(Console.ReadLine())) {
                case 1: JustTriangles();break;
                case 2: RandCubeGenerator();break;
                case 3: SimpleCubeGenerator();break;
            }
            Console.WriteLine("Done.");
            System.Threading.Thread.Sleep(4000);
        }
        public static void JustTriangles() {
            string path = @"C:\Users\feroc\Downloads\complanari.txt";
            Utility.Manager.WriteTriangles(Utility.Manager.ReadPoints(path), @"C:\Users\feroc\Downloads\triangolicomplanari.txt");
        }
        public static void RandCubeGenerator() {
            LinkedList<_3Dpoint> points = Utility.Manager.WriteCubePoints(true, 50, 150, 0, @"C:\Users\feroc\Downloads\cubeCartRand.txt");
            Utility.Manager.WriteTriangles(points, @"C:\Users\feroc\Downloads\triangles.txt");
        }
        public static void SimpleCubeGenerator() {
            LinkedList<_3Dpoint> points = Utility.Manager.WriteCubePoints(false, 0, 0, 3, @"C:\Users\feroc\Downloads\simpleCubeCart.txt");
            Utility.Manager.WriteTriangles(points, @"C:\Users\feroc\Downloads\simpletriangles.txt");
        }
    }
}
