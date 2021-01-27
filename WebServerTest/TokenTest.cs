using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Text;
using WebServer.Controllers.api;

namespace WebServer_Test {
    [TestClass]
    public class TokenTest {
        

        [TestInitialize]
        public void Init()
        {
            //We create the users here
            WebServer.Models.Security.indexesPasswords.Add(9, "password");
            WebServer.Models.Security.indexesPasswords.Add(10, "password");
            WebServer.Models.Security.indexesPasswords.Add(11, "password");
        }

        [TestMethod]
        public void CorrectEncodeDecode() {
            var controller = new ConaidController();
            //Normal credentials
            int buildingIndex = 9;
            string password = "password";
            HttpResponseMessage tokenTest = controller.GetToken(buildingIndex, password);
            Assert.IsTrue(WebServer.Models.Security.CheckToken(Convert.ToBase64String(Encoding.ASCII.GetBytes(tokenTest.Content.ToString().ToCharArray()))));
        }

        [TestCleanup]
        public void Cleanup()
        {
            //We delete the users reated in Init here
            WebServer.Models.Security.indexesPasswords.Remove(9);
            WebServer.Models.Security.indexesPasswords.Remove(10);
            WebServer.Models.Security.indexesPasswords.Remove(11);
        }
    }
}
