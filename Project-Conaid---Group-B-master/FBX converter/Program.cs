using System;

namespace FBX_converter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PythonRunner.RunPythonScript("main.py", "simpletriangles");
                Console.WriteLine(":)");
            }
            catch (Exception e)
            {
                Console.WriteLine(":(");
                Console.Write(e.StackTrace);
            }
            Console.ReadKey();
        }
    }
}
