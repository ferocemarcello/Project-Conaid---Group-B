using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Utility_Test
{
    [TestClass]
    public class SmallUtilsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector3 value = new Vector3(0, 0, 0);
            float distance = Utility.Triangle.ComputeDistance(value, new Plane(value, 10));
            Assert.AreEqual(10, distance);
        }

        [TestMethod]
        public void PointsEqualDelta()
        {
            Utility._3Dpoint p = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(0.01, 0.01, 0.01);
            Assert.IsTrue(p.PointEqualsDelta(p2, 0.1));
            Assert.IsTrue(p2.PointEqualsDelta(p, 0.1));
        }
    }
}
