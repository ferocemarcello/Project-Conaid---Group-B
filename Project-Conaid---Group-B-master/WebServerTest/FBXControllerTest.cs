using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Numerics;

namespace WebServer_Test
{
    [TestClass]
    public class FBXControllerTest
    {
        [TestMethod]
        public void JustDoIt()
        {
            WebServer.Controllers.api.FBXController controller = new WebServer.Controllers.api.FBXController();
            //controller.JustDoIt("file1.fbx", "room1", 0.1, 30.48f);
        }

        [TestMethod]
        public void JustDoItNoCollision()
        {
            WebServer.Controllers.api.FBXController controller = new WebServer.Controllers.api.FBXController();
            Assert.AreEqual( controller.ComparScanToActual("file3.fbx", "room1", 1, 1f, new Vector3(3f,-6.66f,0)), 0);
        }

        [TestMethod]
        public void PythonRunFromDifferentFolder()
        {
            string expected = "ok";
            string result = FBX_converter.PythonRunner.RunPythonScript(Directory.GetCurrentDirectory() + @"\App_Data\PythonScripts\testScript.py", "");
            Assert.AreEqual(expected, result);
        }

    }
}
