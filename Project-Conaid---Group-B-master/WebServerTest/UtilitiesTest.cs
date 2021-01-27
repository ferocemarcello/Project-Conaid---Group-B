using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebServer_Test
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void JsonDeserializeArrayOkay()
        {
            string body = "[\"#RPLIDAR SCAN DATA\",\"#COUNT=4\",\"#Angule\\tDistance\\tQuality\",\"351.4219 2876.0 47\",\"352.5938 2829.0 47\",\"353.7656 2777.0 47\",\"350.2188 2926.0 47\"]";
            string[] expected = { "#RPLIDAR SCAN DATA", "#COUNT=4", "#Angule\\tDistance\\tQuality", "351.4219 2876.0 47", "352.5938 2829.0 47", "353.7656 2777.0 47","350.2188 2926.0 47" };
            string[] result = WebServer.Utilities.DeserializeJsonStringArray(body);
            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);
        }

    }
}
