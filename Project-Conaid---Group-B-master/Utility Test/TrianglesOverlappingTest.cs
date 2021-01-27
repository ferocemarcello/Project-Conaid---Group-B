using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utility_Test
{
    [TestClass]
    public class TrianglesOverlappingTest
    {
        [TestMethod]
        public void TrianglesOverlappingCoplanar_FALSE()
        {
            Utility._3Dpoint p1 = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(1, 0, 0);
            Utility._3Dpoint p3 = new Utility._3Dpoint(0, 1, 0);
            Utility.Triangle t1 = new Utility.Triangle(p1, p2, p3);

            Utility._3Dpoint q1 = new Utility._3Dpoint(2, 2, 0);
            Utility._3Dpoint q2 = new Utility._3Dpoint(3, 2, 0);
            Utility._3Dpoint q3 = new Utility._3Dpoint(2, 3, 0);
            Utility.Triangle t2 = new Utility.Triangle(q1, q2, q3);

            if (t1.OverLapWith(t2))
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TrianglesOverlappingCoplanar_TRUE()
        {
            Utility._3Dpoint p1 = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(1, 0, 0);
            Utility._3Dpoint p3 = new Utility._3Dpoint(0, 1, 0);
            Utility.Triangle t1 = new Utility.Triangle(p1, p2, p3);

            Utility._3Dpoint q1 = new Utility._3Dpoint(.5, .5, 0);
            Utility._3Dpoint q2 = new Utility._3Dpoint(1.5, 0.5, 0);
            Utility._3Dpoint q3 = new Utility._3Dpoint(0.5, 1.5, 0);
            Utility.Triangle t2 = new Utility.Triangle(q1, q2, q3);

            if (t1.OverLapWith(t2))
            {
                Assert.IsTrue(true);
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TrianglesOverlappingNonCoplanar_FALSE()
        {
            Utility._3Dpoint p1 = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(1, 0, 0);
            Utility._3Dpoint p3 = new Utility._3Dpoint(0, 1, 0);
            Utility.Triangle t1 = new Utility.Triangle(p1, p2, p3);

            Utility._3Dpoint q1 = new Utility._3Dpoint(2, 2, 2);
            Utility._3Dpoint q2 = new Utility._3Dpoint(3, 2, 2);
            Utility._3Dpoint q3 = new Utility._3Dpoint(2, 3, 2);
            Utility.Triangle t2 = new Utility.Triangle(q1, q2, q3);

            if (t1.OverLapWith(t2))
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TrianglesOverlappingNonCoplanarNotPlaneSeparated_FALSE()
        {
            Utility._3Dpoint p1 = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(1, 0, 0);
            Utility._3Dpoint p3 = new Utility._3Dpoint(1, 1, 0);
            Utility.Triangle t1 = new Utility.Triangle(p1, p2, p3);

            Utility._3Dpoint q1 = new Utility._3Dpoint(10, -2, 0);
            Utility._3Dpoint q2 = new Utility._3Dpoint(12, -2, 0);
            Utility._3Dpoint q3 = new Utility._3Dpoint(11, 3, 2);
            Utility.Triangle t2 = new Utility.Triangle(q1, q2, q3);

            if (t1.OverLapWith(t2))
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TrianglesOverlappingNonCoplanar_TRUE()
        {
            Utility._3Dpoint p1 = new Utility._3Dpoint(0, 0, 0);
            Utility._3Dpoint p2 = new Utility._3Dpoint(1, 0, 0);
            Utility._3Dpoint p3 = new Utility._3Dpoint(0.5, -1, 0);
            Utility.Triangle t1 = new Utility.Triangle(p1, p2, p3);

            Utility._3Dpoint q1 = new Utility._3Dpoint(0, -0.5, 1);
            Utility._3Dpoint q2 = new Utility._3Dpoint(0.5, -0.5, 1);
            Utility._3Dpoint q3 = new Utility._3Dpoint(1, -0.5, 1);
            Utility.Triangle t2 = new Utility.Triangle(q1, q2, q3);

            if (t1.OverLapWith(t2))
            {
                Assert.IsTrue(true);
            }
            Assert.IsTrue(false);
        }
    }
}
