using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Manager
    {
        public static LinkedList<_3Dpoint> WriteCubePoints(bool rand, int firstRand, int secondRand, int pointsPerEdge)
        {
            List<string> lines = new List<string>();
            LinkedList<string> xyFront = new LinkedList<string>();
            LinkedList<string> yzFront = new LinkedList<string>();
            LinkedList<string> xzFront = new LinkedList<string>();
            LinkedList<string> xyBack = new LinkedList<string>();
            LinkedList<string> yzBack = new LinkedList<string>();
            LinkedList<string> xzBack = new LinkedList<string>();
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            int x = 0, y = 0, z = 0;
            int scartBase = 0;
            int scartDep = 0;
            Random r = new Random();
            double xCord, yCord, zCord;

            //xyfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (y = 0; y < 3000; y = y + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (x = 0; x < 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    xCord = (double)x; yCord = (double)y;
                    points.AddLast(new _3Dpoint(xCord, yCord, 0.0));
                    xyFront.AddLast(xCord.ToString(CultureInfo.InvariantCulture) + " " + yCord.ToString(CultureInfo.InvariantCulture) + " " + "0.0");
                }
            }
            //xyback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (y = 0; y < 3000; y = y + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (x = 0; x < 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    xCord = (double)x; yCord = (double)y;
                    points.AddLast(new _3Dpoint(xCord, yCord, 3000.0));
                    xyBack.AddLast(xCord.ToString(CultureInfo.InvariantCulture) + " " + yCord.ToString(CultureInfo.InvariantCulture) + " " + "3000.0");
                }
            }
            //yzfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (z = 0; z < 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (y = 0; y < 3000; y = y + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    yCord = (double)y; zCord = (double)z;
                    points.AddLast(new _3Dpoint(3000.0, yCord, zCord));
                    yzFront.AddLast("3000.0 " + yCord.ToString(CultureInfo.InvariantCulture) + " " + zCord.ToString(CultureInfo.InvariantCulture));
                }
            }
            //yzback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (z = scartBase; z < 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (y = scartDep; y < 3000; y = y + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    yCord = (double)y; zCord = (double)z;
                    points.AddLast(new _3Dpoint(0.0, yCord, zCord));
                    yzBack.AddLast("0.0 " + yCord.ToString(CultureInfo.InvariantCulture) + " " + zCord.ToString(CultureInfo.InvariantCulture));
                }
            }
            //xzfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (z = 0; z < 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (x = 0; x < 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    xCord = (double)x; zCord = (double)z;
                    points.AddLast(new _3Dpoint(xCord, 3000.0, zCord));
                    xzFront.AddLast(xCord.ToString(CultureInfo.InvariantCulture) + " 3000.0 " + zCord.ToString(CultureInfo.InvariantCulture));
                }
            }
            //xzback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
            for (z = scartBase; z < 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                for (x = scartDep; x < 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, scartBase, scartDep);
                    xCord = (double)x; zCord = (double)z;
                    points.AddLast(new _3Dpoint(xCord, 0.0, zCord));
                    xzBack.AddLast(xCord.ToString(CultureInfo.InvariantCulture) + " 0.0 " + zCord.ToString(CultureInfo.InvariantCulture));
                }
            }
            lines = ((xyFront.Union(xyBack)).Union(yzFront.Union(yzBack)).Union(xzFront.Union(xzBack))).ToList();
            System.IO.File.WriteAllLines(@"C:\Users\feroc\Downloads\cubeCartRand.txt", lines.ToArray());
            return points;
        }

        public static LinkedList<_3Dpoint> ReadPoints(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            foreach (string line in lines)
            {
                string[] coos = line.Split(' ');
                _3Dpoint p1 = new _3Dpoint(double.Parse(coos[0]), double.Parse(coos[1]), double.Parse(coos[2]));
                points.AddLast(p1);
            }
            return points;
        }

        private static void DefineScarts(bool rand, int firstRand, int secondRand, int pointsPerEdge, int scartBase, int scartDep)
        {
            Random r = new Random();
            if (rand)
            {
                scartBase = r.Next(firstRand, secondRand); scartDep = r.Next(firstRand, secondRand);
            }
            else
            {
                scartBase = 3000 / pointsPerEdge; scartDep = scartBase;
            }
        }
        private static bool SameX(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetX() == p2.GetX()) && (p.GetX() == close.GetX())) return true;
            return false;
        }
        private static bool SameY(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetY() == p2.GetY()) && (p.GetY() == close.GetY())) return true;
            return false;
        }
        private static bool SameZ(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetZ() == p2.GetZ()) && (p.GetZ() == close.GetZ())) return true;
            return false;
        }
        public static void WriteTriangles(LinkedList<_3Dpoint> points, string path)
        {
            List<string> triangles = new List<string>();
            double dist;
            double minDist = -1;
            _3Dpoint close1 = new _3Dpoint(0, 0, 0);
            _3Dpoint close2 = new _3Dpoint(0, 0, 0);
            foreach (_3Dpoint p in points)
            {
                minDist = -1;
                foreach (_3Dpoint p2 in points)
                {
                    if (!p2.PointEquals(p))
                    {
                        dist = Math.Sqrt(Math.Pow(p.GetX() - p2.GetX(), 2) + Math.Pow(p.GetY() - p2.GetY(), 2) + Math.Pow(p.GetZ() - p2.GetZ(), 2));
                        if (minDist == -1)
                        {
                            minDist = dist;
                            close1 = p2;
                        }
                        if (!(p.HavingEdge(p2) || p2.HavingEdge(p)))
                        {
                            if (dist <= minDist)
                            {
                                minDist = dist;
                                close1 = p2;
                            }
                        }
                    }
                }
                p.AddEdge(close1);
                minDist = -1;
                foreach (_3Dpoint p2 in points)
                {
                    if (!(p2.PointEquals(p) || p2.PointEquals(close1)))
                    {
                        if (((!SameX(p, p2, close1)) && !(SameY(p, p2, close1))) || ((!SameY(p, p2, close1)) && !(SameZ(p, p2, close1))) || ((!SameX(p, p2, close1)) && !(SameZ(p, p2, close1))))
                        {
                            dist = Math.Sqrt(Math.Pow(p.GetX() - p2.GetX(), 2) + Math.Pow(p.GetY() - p2.GetY(), 2) + Math.Pow(p.GetZ() - p2.GetZ(), 2));
                            if (minDist == -1)
                            {
                                minDist = dist;
                                close2 = p2;
                            }
                            if (!(p.HavingEdge(p2) || p2.HavingEdge(p)))
                            {
                                if (dist <= minDist)
                                {
                                    minDist = dist;
                                    close2 = p2;
                                }
                            }
                        }

                    }
                }
                triangles.Add(p.PointToString() + " " + close1.PointToString() + " " + close2.PointToString());
                p.RemoveEdges();
            }
            System.IO.File.WriteAllLines(path, triangles.ToArray());
        }
    }
}
