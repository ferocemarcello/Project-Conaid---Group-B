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
        public static LinkedList<_3Dpoint> WriteCubePoints(bool rand, int firstRand, int secondRand, int pointsPerEdge, string path)
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
            double xCord, yCord, zCord;

            //xyfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
            for (y = 0; y <= 3000; y = y + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (x = 0; x <= 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                    CubeCreatorAddPoint(x, y,0, points, xyFront);
                    
                }
            }
            //xyback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
            for (y = 0; y <= 3000; y = y + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (x = 0; x <= 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                    CubeCreatorAddPoint(x, y, 3000, points, xyBack);
                }
            }
            //yzfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
            for (z = 0; z <= 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (y = 0; y <= 3000; y = y + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                    CubeCreatorAddPoint(3000, y, z, points, yzFront);
                }
            }
            //yzback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge,ref scartBase, ref scartDep);
            for (z = 0/*scartBase*/; z <= 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (y =0 /*scartDep*/; y <= 3000; y = y + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge,ref scartBase,ref scartDep);
                    CubeCreatorAddPoint(0, y, z, points, yzBack);
                }
            }
            //xzfront
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
            for (z = 0; z <= 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (x = 0; x <= 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                    CubeCreatorAddPoint(x, 3000, z, points, xzFront);
                }
            }
            //xzback
            DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase,ref scartDep);
            for (z = 0/*scartBase*/; z < 3000; z = z + scartBase)
            {
                DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                for (x = 0/*scartDep*/; x < 3000; x = x + scartDep)
                {
                    DefineScarts(rand, firstRand, secondRand, pointsPerEdge, ref scartBase, ref scartDep);
                    CubeCreatorAddPoint(x, 0, z, points, xzBack);
                }
            }
            lines = ((xyFront.Union(xyBack)).Union(yzFront.Union(yzBack)).Union(xzFront.Union(xzBack))).ToList();
            System.IO.File.WriteAllLines(path, lines.ToArray());
            return points;
        }

        private static void CubeCreatorAddPoint(int x, int y, int z,LinkedList<_3Dpoint> points, LinkedList<string> xyFront)
        {
            double xCord = (double)x; double yCord = (double)y; double zCord = (double)z;
            if (!(Manager.LinkedListOfPointsContains(points,new _3Dpoint(xCord, yCord, zCord))))
            {
                points.AddLast(new _3Dpoint(xCord, yCord, zCord));
            }
            xyFront.AddLast(xCord.ToString(CultureInfo.InvariantCulture) + " " + yCord.ToString(CultureInfo.InvariantCulture) + " " + zCord.ToString(CultureInfo.InvariantCulture));
        }

        public static bool LinkedListOfPointsContains(LinkedList<_3Dpoint> points ,_3Dpoint _3Dpoint)
        {
            LinkedList<_3Dpoint>.Enumerator en = points.GetEnumerator();
            bool contained = false;
            while (en.MoveNext())
            {
                if (en.Current.PointEquals(_3Dpoint)) contained = true;
            }
            return contained;
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
        private static void DefineScarts(bool rand, int firstRand, int secondRand, int pointsPerEdge, ref int scartBase, ref int scartDep)
        {
            Random r = new Random();
            if (rand)
            {
                scartBase = r.Next(firstRand, secondRand); scartDep = r.Next(firstRand, secondRand);
            }
            else
            {
                scartBase = 3000 / (pointsPerEdge-1); scartDep = scartBase;
            }
        }
        public static bool SameX(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetX() == p2.GetX()) && (p.GetX() == close.GetX())) return true;
            return false;
        }
        public static bool SameY(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetY() == p2.GetY()) && (p.GetY() == close.GetY())) return true;
            return false;
        }

        public static bool SameZ(_3Dpoint p, _3Dpoint p2, _3Dpoint close)
        {
            if ((close.GetZ() == p2.GetZ()) && (p.GetZ() == close.GetZ())) return true;
            return false;
        }
        public static bool Remove3DpointFromLinkedList(_3Dpoint p,LinkedList<_3Dpoint> points)
        {
            LinkedList<_3Dpoint>.Enumerator en = points.GetEnumerator();
            bool inside = true;
            while (inside&&en.MoveNext())
            {
                if (en.Current.PointEquals(p))
                {
                    points.Remove(en.Current);
                    inside = false;
                    return true;
                }
            }
            return false;
        }
        public static void WriteTriangles(LinkedList<_3Dpoint> points, string path)
        {
            LinkedList<Triangle> triangles = new LinkedList<Triangle>();
            List<string> trianglesString = new List<string>();
            bool trianglesOver = false;
            while (!trianglesOver)
            {
                trianglesString=trianglesString.Union(LookForPoints(points, triangles)).ToList();
                trianglesOver = true;
            }
            System.IO.File.WriteAllLines(path, trianglesString.ToArray());
        }

        private static List<string> LookForPoints(LinkedList<_3Dpoint> points, LinkedList<Triangle> triangles)
        {
            _3Dpoint close1;
            List<string> trianglesString = new List<string>();
            LinkedList<_3Dpoint> buffer1;
            foreach (_3Dpoint p in points)
            {
                buffer1 = new LinkedList<_3Dpoint>(points);
                close1 = FindClosestPoint(p, points);
                Triangle t;
                bool found = false;
                while (buffer1.Count > 0 && (!found) && (close1 != null))
                {
                    LinkedList<_3Dpoint> pointsCopy = new LinkedList<_3Dpoint>(points);
                    t = TryChangingSecondClosestPointTriangle(p, close1, pointsCopy, triangles);
                    if (t == null) found = false; else found = true;
                    if (found)
                    {
                        triangles.AddLast(t);
                        trianglesString.Add(t.TriangleToString());
                    }
                    else
                    {
                        Manager.Remove3DpointFromLinkedList(close1, buffer1);
                        close1 = FindClosestPoint(p, buffer1);
                    }
                }

            }
            return trianglesString;
        }

        private static Triangle TryChangingSecondClosestPointTriangle(_3Dpoint p, _3Dpoint close1, LinkedList<_3Dpoint> buffer2, LinkedList<Triangle> triangles)
        {
            //_3Dpoint close2 = FindSecondClosestPoint(p, close1, buffer2);
            //Triangle t = new Triangle(p, close1, close2);
            //if (!t.OverLapAtLeastOneTriangle(triangles)) return t;
            bool found = false;
            Triangle t = new Triangle();
            _3Dpoint close2 = FindSecondClosestPoint(p, close1, buffer2);
            while (buffer2.Count > 2 && (!found)&&(close2!=null))
            {
                
                t = new Triangle(p, close1, close2);
                if (!t.OverLapAtLeastOneTriangle(triangles))
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
                Manager.Remove3DpointFromLinkedList(close2, buffer2);
                close2 = FindSecondClosestPoint(p, close1, buffer2);
            }
            if (found == true) return t; else return null;
        }
        public static _3Dpoint FindSecondClosestPoint(_3Dpoint p, _3Dpoint close1, LinkedList<_3Dpoint> buffer2)
        {
            double dist;
            double minDist = -1;
            _3Dpoint close2 = null;
            minDist = -1;
            if (buffer2.Count < 3) return null;
            foreach (_3Dpoint p2 in buffer2)
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
                        //if (!(p.HavingEdge(p2) || p2.HavingEdge(p)))
                        //{
                        if (dist < minDist)
                        {
                            minDist = dist;
                            close2 = p2;
                        }
                        //}
                    }
                }
            }
            return close2;
        }
        public static _3Dpoint FindClosestPoint(_3Dpoint p, LinkedList<_3Dpoint> buffer1)
        {
            double dist;
            double minDist = -1;
            _3Dpoint close1 = null;
            if (buffer1.Count < 2) return null;
            foreach (_3Dpoint p2 in buffer1)
            {
                if (!p2.PointEquals(p))
                {
                    dist = Math.Sqrt(Math.Pow(p.GetX() - p2.GetX(), 2) + Math.Pow(p.GetY() - p2.GetY(), 2) + Math.Pow(p.GetZ() - p2.GetZ(), 2));
                    if (minDist == -1)
                    {
                        minDist = dist;
                        close1 = p2;
                    }
                    //if (!(p.HavingEdge(p2) || p2.HavingEdge(p)))
                    //{
                    if (dist < minDist)
                    {
                        minDist = dist;
                        close1 = p2;
                    }
                    //}
                }
            }
            return close1;
        }
    }
}
