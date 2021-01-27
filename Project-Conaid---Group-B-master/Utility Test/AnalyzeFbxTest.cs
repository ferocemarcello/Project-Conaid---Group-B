using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Utility;
using System.Numerics;
using AnalyzeFbx;
using AnalyzeFBX;

namespace Utility_Test
{
    [TestClass]
    public class AnalyzeFbxTest
    {
        [TestMethod]
        public void CanFindVerticesInFBX_OK()
        {
            LinkedList<string> vertices = new LinkedList<string>();
            string filename = "file.fbx";
            vertices = AnalyzeFbx.FromFBX.ReadVerticesFromFBX(filename);
            Assert.IsTrue(vertices.First.Value.Contains("a: -4.71190738677979,-1.10639142990112,-6.61744490042422e-024,-3.76046371459961,-1.10639142990112,6.61744490042422e-024,10.7408485412598,-1.10639142990112,6.61744490042422e-024,10.7408485412598,-1.10639142990112,8.20209980010986,-3.76046371459961,-1.10639142990112,8.20209980010986,-4.71190738677979,-1.10639142990112,8.20209980010986,-4.71190738677979,-0.154947906732559,0,-4.71190738677979,-0.154947906732559,8.20209980010986,10.7408485412598,-0.154947906732559,8.20209980010986,10.7408485412598,-0.154947906732559,0"));
            Assert.AreEqual(vertices.Count, 9);
        }

        [TestMethod]
        public void StringToList_OK()
        {
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            string s = "\t\t\ta: -4.71190738677979,-1.10639142990112,-6.61744490042422e-024,-3.76046371459961,-1.10639142990112,6.61744490042422e-024";
            points = AnalyzeFbx.FromFBX.VerticesStringToPointList(s);
            Assert.AreEqual(points.Count, 2);
            Assert.IsTrue(points.First.Value.PointEquals(new _3Dpoint(-4.71190738677979, -1.10639142990112, -6.61744490042422e-024)));
        }

        [TestMethod]
        public void FindDuplicates()
        {

            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            points.AddLast(new Utility._3Dpoint(0, 0, 0));
            points.AddLast(new Utility._3Dpoint(0, 1, 0));
            points.AddLast(new Utility._3Dpoint(0, 0, 0));
            int count = AnalyzeFbx.VerticesAnalyzier.OccourrenceMoreThanOne(points).Count;

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void PlanesFromPoints_ThreePointsOnePlane()
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(0, 1, 0));
            points.Add(new Vector3(1, 0, 0));

            Plane expected = Plane.CreateFromVertices(points[0],points[1],points[2]);

            List<Plane> result = AnalyzeFbx.VerticesAnalyzier.PlanesForPoints(points);

            Assert.AreEqual(expected, result[0]);
        }

        [TestMethod]
        public void FindNearestPlane_Stupid()
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(0.1f, 0, 0));
            points.Add(new Vector3(1, 0, 0));
            points.Add(new Vector3(0, 1, 0));
            points.Add(new Vector3(1, 1, 0));
            points.Add(new Vector3(0, 0, 1));
            points.Add(new Vector3(1, 0, 1));
            points.Add(new Vector3(1, 1, 1));
            points.Add(new Vector3(0, 0, 1));


            Plane expected = Plane.CreateFromVertices(points[0], points[2], points[3]);
            Plane result = AnalyzeFbx.VerticesAnalyzier.PlaneForWall(points);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PolyhedraConstruction()
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(0.1f, 0, 0));
            points.Add(new Vector3(1, 0, 0));
            points.Add(new Vector3(0, 1, 0));
            points.Add(new Vector3(1, 1, 0));
            points.Add(new Vector3(0, 0, 1));
            points.Add(new Vector3(1, 0, 1));
            points.Add(new Vector3(1, 1, 1));
            points.Add(new Vector3(0, 0, 1));

            Polyhedra p = new Polyhedra(points);

            Assert.IsTrue(p.IsInside(points[3]));
        }

        [TestMethod]
        public void PolyhedraConstruction_delta()
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(0.1f, 0, 0));
            points.Add(new Vector3(1, 0, 0));
            points.Add(new Vector3(0, 1, 0));
            points.Add(new Vector3(1, 1, 0));
            points.Add(new Vector3(0, 0, 1));
            points.Add(new Vector3(1, 0, 1));
            points.Add(new Vector3(1, 1, 1));
            points.Add(new Vector3(0, 0, 1));

            Polyhedra p = new Polyhedra(points, 0.1);

            Assert.IsTrue(p.IsInsideDelta(new Vector3(-0.01f, 0, 0)));
        }
    }
}
