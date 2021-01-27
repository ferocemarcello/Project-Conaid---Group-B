using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Utility;

namespace TestProject
{
    [TestClass]
    public class TestEdges
    {
        _3Dpoint p1;
        _3Dpoint p2;
        _3Dpoint p3;
        _3Dpoint p4;
        _3Dpoint p5;
        _3Dpoint p6;
        _3Dpoint p7;
        _3Dpoint p8;
        _3Dpoint p9;

        Triangle t1;
        Triangle t2;
        Triangle t3;
        Triangle t4;
        Triangle t5;
        Triangle t6;
        Triangle t7;
        Triangle t8;
        Triangle t9;
        Triangle t10;
        Triangle t11;
        Triangle t12;
        [TestMethod]
        public void TestEqualsEdges()
        {
            string path = @"C:\Users\feroc\Downloads\simpletriangles.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            List<Edge> edges = new List<Edge>();
            List<Triangle> triangles = new List<Triangle>();
            foreach (string line in lines)
            {
                //edges.Add(GetFirstEdgeFromLine(line));
                //edges.Add(GetSecondEdgeFromLine(line));
                triangles.Add(GetTriangleFromLine(line));
            }
            /*for(int x = 0; x < lines.Length; x++)
            {
                if(GetTriangleFromLine(lines[x]).TriangleEquals(new Triangle(new _3Dpoint(0,0,0),new _3Dpoint(0,117,0),new _3Dpoint(139, 0, 0))))
                {
                    Console.WriteLine(x + 1);
                }
            }*/
            foreach(Triangle t in triangles)
            {
                List<Triangle> fia = triangles.FindAll(item => item.TriangleEquals(t));
                if (triangles.FindAll(item => item.TriangleEquals(t)).Count != 1)
                {
                    Assert.Fail();
                }
            }
            Assert.IsTrue(true);
        }

        private Triangle GetTriangleFromLine(string line)
        {
            string[] coos = line.Split(' ');
            _3Dpoint p1 = new _3Dpoint(double.Parse(coos[0]), double.Parse(coos[1]), double.Parse(coos[2]));
            _3Dpoint p2 = new _3Dpoint(double.Parse(coos[3]), double.Parse(coos[4]), double.Parse(coos[5]));
            _3Dpoint p3 = new _3Dpoint(double.Parse(coos[6]), double.Parse(coos[7]), double.Parse(coos[8]));
            return new Triangle(p1, p2, p3);
        }

        private Edge GetFirstEdgeFromLine(string line)
        {
            string[] coos = line.Split(' ');
            //double d = double.Parse(coos[0]);
            _3Dpoint p1 = new _3Dpoint(double.Parse(coos[0]), double.Parse(coos[1]), double.Parse(coos[2]));
            _3Dpoint p2 = new _3Dpoint(double.Parse(coos[3]), double.Parse(coos[4]), double.Parse(coos[5]));

            return new Edge(p1, p2);
        }
        private Edge GetSecondEdgeFromLine(string line)
        {
            string[] coos = line.Split(' ');
            _3Dpoint p1 = new _3Dpoint(double.Parse(coos[3]), double.Parse(coos[4]), double.Parse(coos[5]));
            _3Dpoint p2 = new _3Dpoint(double.Parse(coos[6]), double.Parse(coos[7]), double.Parse(coos[8]));
            return new Edge(p1, p2);
        }
        [TestMethod]
        public void TestDistances()
        {
            _3Dpoint p1 = new _3Dpoint(3000, 2184, 0);
            _3Dpoint p2 = new _3Dpoint(2961, 2911, 3000);
            _3Dpoint p3 = new _3Dpoint(3000, 770, 100);
            double d12 = Math.Sqrt(Math.Pow(p1.GetX() - p2.GetX(), 2) + Math.Pow(p1.GetY() - p2.GetY(), 2) + Math.Pow(p1.GetZ() - p2.GetZ(), 2));
            double d13 = Math.Sqrt(Math.Pow(p1.GetX() - p3.GetX(), 2) + Math.Pow(p1.GetY() - p3.GetY(), 2) + Math.Pow(p1.GetZ() - p3.GetZ(), 2));
            double d23 = Math.Sqrt(Math.Pow(p2.GetX() - p3.GetX(), 2) + Math.Pow(p2.GetY() - p3.GetY(), 2) + Math.Pow(p2.GetZ() - p3.GetZ(), 2));
            Assert.IsTrue(d12< 3087.077906+0.000001|| d12> 3087.077906 - 0.000001);
            Assert.IsTrue(d13 < 1417.531657 + 0.000001 || d13> 1417.531657 - 0.000001);
            Assert.IsTrue( d23 < 3604.913591 + 0.000001 || d23 > 3604.913591 - 0.000001);
        }
        [TestMethod]
        public void TestNotEqualPoints()
        {
            string path = @"C:\Users\feroc\Downloads\cubeCartRand.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            LinkedList<_3Dpoint> points = Manager.ReadPoints(path);
            List<_3Dpoint> lisPoints = new List<_3Dpoint>(points);
            foreach (_3Dpoint p in lisPoints)
            {
                List<_3Dpoint> fia = lisPoints.FindAll(item => item.PointEquals(p));
                if (fia.Count != 1) Assert.Fail();
            }
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestRemoveFromLinkedList()
        {
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            points.AddLast(new _3Dpoint(1, 2, 3));
            points.AddLast(new _3Dpoint(2, 3, 4));
            points.AddLast(new _3Dpoint(3, 4, 5));
            Manager.Remove3DpointFromLinkedList(new _3Dpoint(2, 3, 4), points);
            Assert.IsTrue(points.Count == 2);
        }
        [TestMethod]
        public void TestSameCoordinates()
        {
            Assert.IsTrue(Manager.SameX(new _3Dpoint(2, 3, 4), new _3Dpoint(2, 4, 5), new _3Dpoint(2, 434, 45)));
            Assert.IsFalse(Manager.SameX(new _3Dpoint(24, 23, 35), new _3Dpoint(34, 253, 533), new _3Dpoint(27, 60, 234)));
            Assert.IsTrue(Manager.SameY(new _3Dpoint(242, 3, 466), new _3Dpoint(552, 3, 4425), new _3Dpoint(2324, 3, 4524)));
            Assert.IsFalse(Manager.SameY(new _3Dpoint(24, 23, 35), new _3Dpoint(34, 253, 533), new _3Dpoint(27, 60, 234)));
            Assert.IsTrue(Manager.SameZ(new _3Dpoint(422, 3,4), new _3Dpoint(42, 24, 4), new _3Dpoint(121, 867, 4)));
            Assert.IsFalse(Manager.SameZ(new _3Dpoint(24, 23, 35), new _3Dpoint(34, 253, 533), new _3Dpoint(27, 60, 234)));
        }
        [TestMethod]
        public void TestClosestPoint()
        {
            _3Dpoint p1 = new _3Dpoint(0, 0, 0);
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            points.AddLast(new _3Dpoint(1, 1, 6));
            points.AddLast(new _3Dpoint(1, 2, 5));
            points.AddLast(new _3Dpoint(2, 3, 1));
            Assert.IsTrue(Manager.FindClosestPoint(p1, points).PointEquals(new _3Dpoint(2, 3, 1)));
        }
        [TestMethod]
        public void TestSecondClosestPoint()
        {
            _3Dpoint p1 = new _3Dpoint(0, 0, 0);
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            points.AddLast(new _3Dpoint(1, 1, 6));
            points.AddLast(new _3Dpoint(1, 2, 5));
            points.AddLast(new _3Dpoint(2, 3, 1));
            points.AddLast(new _3Dpoint(4, 2, 1));
            Assert.IsTrue(Manager.FindSecondClosestPoint(p1, Manager.FindClosestPoint(p1,points),points).PointEquals(new _3Dpoint(4, 2, 1)));
        }
         [TestInitialize]
        public void TestOverLappingTriangles()
        {
            LinkedList<Triangle> triangles = new LinkedList<Triangle>();
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            p1 = new _3Dpoint(200, 100, 0);
            p2 = new _3Dpoint(400, 100, 0);
            p3 = new _3Dpoint(700, 100, 0);
            p4 = new _3Dpoint(200, 200, 0);
            p5 = new _3Dpoint(400, 200, 0);
            p6 = new _3Dpoint(700, 200, 0);
            p7 = new _3Dpoint(200, 300, 0);
            p8 = new _3Dpoint(400, 300, 0);
            p9 = new _3Dpoint(700, 300, 0);

            t1 = new Triangle(p1, p2, p4);
            t2 = new Triangle(p1, p2, p5);
            t3 = new Triangle(p1, p4, p5);
            t4 = new Triangle(p2, p4, p5);
            t5 = new Triangle(p2, p3, p6);
            t6 = new Triangle(p4, p5, p7);
            t7 = new Triangle(p2, p4, p6);
            t8 = new Triangle(p2, p5, p6);
            t9 = new Triangle(p8, p9, p6);
            t10 = new Triangle(p5, p6, p9);
            t11 = new Triangle(p3, p5, p6);
            t12 = new Triangle(p7, p9, p6);
            

            //t1 overlap t2 and t3 but not t4
            //t2 overlap t1 and t4 but not t3
            //t3 overlap t1 and t4 but not t2
            //t4 overlap t2 and t3 but not t1
            /*Assert.IsFalse(new Edge(p1, p2).Intersect(new Edge(p1, p5)));
            Assert.IsFalse(new Edge(p1, p2).Intersect(new Edge(p2, p5)));
            Assert.IsFalse(new Edge(p1, p2).Intersect(new Edge(p1, p2)));
            Assert.IsFalse(new Edge(p2, p4).Intersect(new Edge(p1, p2)));
            Assert.IsFalse(new Edge(p2, p4).Intersect(new Edge(p2, p5)));
            Assert.IsTrue(new Edge(p2, p4).Intersect(new Edge(p1, p5)));*/
        }
        [TestMethod]
        public void TestOverlapEdges()
        {
            Assert.IsFalse(new Edge(p7,p5).Intersect(new Edge(p2,p3)));
            Assert.IsFalse(new Edge(p2, p3).Intersect(new Edge(p7, p5)));
        }
        [TestMethod]
        public void TestOverlapEdges2()
        {
            _3Dpoint p = Edge.IntersectionPoint(new Edge(p2, p6), new Edge(p2, p4));
            Assert.IsTrue(Edge.IntersectionPoint(new Edge(p2, p6), new Edge(p2, p4)).PointEquals(new _3Dpoint(400, 100, 0)));
        }
        [TestMethod]
        public void TestOverlapEdges3()
        {
            _3Dpoint p = Edge.IntersectionPoint(new Edge(p3, p5), new Edge(p2, p6));
            Assert.IsNotNull(p);
        }
        [TestMethod]
        public void LineContainsPoint()
        {
            _3Dpoint p = new _3Dpoint(550, 150, 0);
            StraightLine2D line = new StraightLine2D(p3, p5);
            Assert.IsTrue(line.Contains(p));
        }
        [TestMethod]
        public void LineContainsPoint2()
        {
            _3Dpoint p = new _3Dpoint(550, 150, 0);
            StraightLine2D line = new StraightLine2D(p2, p6);
            Assert.IsTrue(line.Contains(p));
        }

        [TestMethod]
        public void TestOverlapTriangle7and4()
        {
            Assert.IsTrue(t7.OverLapWith(t4));
            Assert.IsTrue(t4.OverLapWith(t7));
        }

        [TestMethod]
        public void TestOverlapTriangle12and10()
        {
            Assert.IsTrue(t12.OverLapWith(t10));
            Assert.IsTrue(t10.OverLapWith(t12));
        }

        [TestMethod]
        public void TestOverlapTriangle11and8()
        {
            Assert.IsTrue(t11.OverLapWith(t8));
        }
        [TestMethod]
        public void TestOverlapTriangle8and11()
        {
            Assert.IsTrue(t8.OverLapWith(t11));
        }
        [TestMethod]
        public void TestOverlapTriangle9and10()
        {
            Assert.IsTrue(t9.OverLapWith(t10));
        }
        [TestMethod]
        public void TestOverlapTriangle10and9()
        {
            Assert.IsTrue(t10.OverLapWith(t9));
        }
        [TestMethod]
        public void TestOverlapTriangle9and5()
        {
            Assert.IsFalse(t9.OverLapWith(t5));
        }
        [TestMethod]
        public void TestOverlapTriangle5and9()
        {
            Assert.IsFalse(t5.OverLapWith(t9));
        }
        [TestMethod]
        public void TestOverlapTriangle7and8()
        {
            Assert.IsTrue(t7.OverLapWith(t8));
            Assert.IsTrue(t8.OverLapWith(t7));
        }
        [TestMethod]
        public void TestOverlapTriangle1and2()
        {
            Assert.IsTrue(t1.OverLapWith(t2));
        }
        [TestMethod]
        public void TestIntersectLine()
        {
            StraightLine2D line1 = new StraightLine2D(p7, p4);
            StraightLine2D line2 = new StraightLine2D(p2, p6);
            Assert.IsTrue(StraightLine2D.IntersectionPoint(line1,line2).GetX()==200);
        }
        [TestMethod]
        public void TestIntersectLine2()
        {
            StraightLine2D line1 = new StraightLine2D(p2, p3);
            StraightLine2D line2 = new StraightLine2D(p5, p7);
            Assert.IsTrue(StraightLine2D.IntersectionPoint(line1, line2).GetX() == 600);
            Assert.IsTrue(StraightLine2D.IntersectionPoint(line1, line2).GetY() == 100);
        }
        [TestMethod]
        public void TestOverlapTriangle5and6()
        {
            Assert.IsFalse(t5.OverLapWith(t6));
            Assert.IsFalse(t6.OverLapWith(t5));
        }
        [TestMethod]
        public void TestOverlapTriangle1and3()
        {
            Assert.IsTrue(t1.OverLapWith(t3));
        }
        [TestMethod]
        public void TestOverlapTriangle1and5()
        {
            Assert.IsFalse(t1.OverLapWith(t5));
        }
        [TestMethod]
        public void TestOverlapTriangle5and1()
        {
            Assert.IsFalse(t5.OverLapWith(t1));
        }
        [TestMethod]
        public void TestOverlapTriangle1and4()
        {
            Assert.IsFalse(t1.OverLapWith(t4));
        }
        [TestMethod]
        public void TestOverlapTriangle2and1()
        {
            Assert.IsTrue(t2.OverLapWith(t1));
        }
        [TestMethod]
        public void TestOverlapTriangle2and3()
        {
            Assert.IsFalse(t2.OverLapWith(t3));
        }
        [TestMethod]
        public void TestOverlapTriangle2and4()
        {
            Assert.IsTrue(t2.OverLapWith(t4));
        }
        [TestMethod]
        public void TestOverlapTriangle3and1()
        {
            Assert.IsTrue(t3.OverLapWith(t1));
        }
        [TestMethod]
        public void TestOverlapTriangle3and4()
        {
            Assert.IsTrue(t3.OverLapWith(t4));
        }
        [TestMethod]
        public void TestOverlapTriangle3and2()
        {
            Assert.IsFalse(t3.OverLapWith(t2));
        }
        [TestMethod]
        public void TestOverlapTriangle4and2()
        {
            Assert.IsTrue(t4.OverLapWith(t2));
        }
        [TestMethod]
        public void TestOverlapTriangle4and3()
        {
            Assert.IsTrue(t4.OverLapWith(t3));
        }
        [TestMethod]
        public void TestOverlapTriangle4and5()
        {
            Assert.IsFalse(t4.OverLapWith(t5));
        }
        [TestMethod]
        public void TestOverlapTriangle5and4()
        {
            Assert.IsFalse(t5.OverLapWith(t4));
        }
        [TestMethod]
        public void TestDecimalSubtract()
        {
            Assert.AreEqual(decimal.Subtract((decimal)3.1, (decimal)0.2), (decimal)2.9);
        }
        [TestMethod]
        public void TestOverlapTriangle4and1()
        {
            Assert.IsFalse(t4.OverLapWith(t1));
        }
    }
}
