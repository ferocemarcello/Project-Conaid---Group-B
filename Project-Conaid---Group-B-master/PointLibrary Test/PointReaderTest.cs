using System;
using LibraryModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PointLibrary_Test
{
    [TestClass]
    public class PointReaderTest
    {
        [TestMethod]
        public void ReadOkay()
        {
            string[] fileContent = {"#RPLIDAR SCAN DATA","#COUNT=3","#Angule	Distance	Quality",
                                    "351.9688 2930.0 47",
                                    "353.0625 2923.0 47",
                                    "354.2813 2879.0 47"};
            PointReader.Read(fileContent);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadWrongCount()
        {
            string[] fileContent = {"#RPLIDAR SCAN DATA","#COUNT=305","#Angule	Distance	Quality",
                                    "351.9688 2930.0 47",
                                    "353.0625 2923.0 47",
                                    "354.2813 2879.0 47"};
            PointReader.Read(fileContent);
            //shouldn't reach here, the exception should occour in the Read
            Assert.IsTrue(false);
        }
    }
}
