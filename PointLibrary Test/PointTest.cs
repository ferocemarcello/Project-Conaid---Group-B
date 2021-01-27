using LibraryModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PointLibrary_Test
{
    [TestClass]
    public class PointTest
    {
        //Expected results from http://keisan.casio.com/exec/system/1223527679

        double delta = 0.00000000001;

        [TestMethod]
        public void ToCartesianOk()
        {
            Point p = new Point(351.4219, 2876.0);
            double x = 2843.8275261446024;
            double y = -428.976691140988;
            p.ToCartesian();

            Assert.AreEqual(x, p.X,delta);
            Assert.AreEqual(y, p.Y,delta);
        }
    }
}
