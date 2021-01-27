using System;
using System.Diagnostics;
using System.IO;

namespace FBX_converter
{
    public class PythonRunner
    {
        public static string RunPythonScript(string pythonScriptPath, string args)
        {
            string mainProjDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Console.WriteLine(mainProjDir);

            string python = @"C:\Python26\python.exe";

            string myPythonApp = pythonScriptPath;

            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st argument is pointer to itself, 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = $"{myPythonApp} {args}";

            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();

            // write the output we got from python app 
            //Console.WriteLine("Value received from script: " + myString);
            return myString;
        }
    }
}
