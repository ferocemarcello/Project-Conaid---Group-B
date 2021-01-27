using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;

namespace FBX_converter_Test
{
    [TestClass]
    public class PythonRunnerTest
    {

        //Our path
        string mainProjDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [TestInitialize]
        public void CreatePythonFileInRightDirectory()
        {
            string pythonDir = mainProjDir + @"\FBXperiment\testScript.py";
            StreamWriter writer = File.CreateText(pythonDir);

            writer.AutoFlush = true;

            using (writer)
                writer.Write("print \"ok\"");

            string pythonDir2 = mainProjDir + @"\FBX converter Test\bin\Debug\scan.txt";
            StreamWriter writer2 = File.CreateText(pythonDir2);

            writer2.AutoFlush = true;

            using (writer2)
                writer2.Write("1 2 3 4 5 6 7 8 9");

        }

        [TestMethod]
        public void RunPythonScriptNoParam_OK()
        {
            string expected = "ok";
            string result = FBX_converter.PythonRunner.RunPythonScript(Directory.GetCurrentDirectory() + @"\..\..\..\FBXperiment\testScript.py", "");
            Assert.AreEqual(expected, result);
        }

        //Just tests that the parameter file is found
        [TestMethod]
        public void RunConversionScript_ParameterOk()
        {
            string expected = "<open file 'scan.txt', mode 'r'";
            string result = FBX_converter.PythonRunner.RunPythonScript(@"\FBXperiment\main.py", "scan");
            Assert.IsTrue(result.StartsWith(expected));
        }

        [TestMethod]
        public void RunPythonScript_FbxCreated()
        {
            FBX_converter.PythonRunner.RunPythonScript(@"\FBXperiment\main.py", "scan");
            var f = File.Open("scan.fbx", FileMode.Open);
            Assert.IsTrue(true);
            f.Close();
        }

        [TestMethod]
        public void BinaryToAscii_OK()
        {
            string expected = "True";
            string result = FBX_converter.PythonRunner.RunPythonScript(@"\FBXperiment\converter.py", "file1");
            Assert.AreEqual(expected,result);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("scan.fbx");
            File.Delete("scan.txt");
        }
    }
}
