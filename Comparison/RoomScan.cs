using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace Comparison
{
    public class RoomScan
    {
        string directory;
        List<ScanReading> scans = new List<ScanReading>();
        public List<ScannedPoint> AllPoints = new List<ScannedPoint>();

        public RoomScan(string directory)
        {
            this.directory = directory;
            string[] filePaths = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory() +"\\"+ directory);
            foreach (string path in filePaths)
            {
                ScanReading sr = new ScanReading(path);
                scans.Add(sr);
                AllPoints.AddRange(sr.Points);
            }

        }

        public void SaveToTextFile(string filename)
        {
            StreamWriter sr = new StreamWriter(Directory.GetCurrentDirectory() + @"\App_Data\FBXFiles\" +filename + ".txt");
            foreach (ScannedPoint p in AllPoints)
            {
                if (p.collisions == 0)
                {
                    string content = p.point.ToString();
                    // <x, y, z>
                    //So we remove < and >, last and first char
                    content = content.Substring(1);
                    content = content.Substring(0, content.Length - 1);
                    sr.WriteLine(content);
                }
            }
            sr.Flush();
            sr.Close();
        }

        public void SaveToFBXFile(string filename)
        {

            string result = FBX_converter.PythonRunner.RunPythonScript(Directory.GetCurrentDirectory() + @"\App_Data\PythonScripts\mainCubes.py", filename);

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\App_Data\FBXFiles\" + filename + ".fbx");
            StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\App_Data\FBXFiles\" + filename +"WithGlobalSettings.fbx");

            while (!sr.EndOfStream)
            {
                string tmp = sr.ReadLine();
                if (tmp.StartsWith("GlobalSettings:  {")){
                    string settings = "";
                    while(!tmp.Contains("}"))
                    {
                        tmp = sr.ReadLine();
                    }
                    settings = @"GlobalSettings:  {
    settings = Version: 1000
	Properties70:  {
		P: ""UpAxis"", ""int"", ""Integer"", """",2
		P: ""UpAxisSign"", ""int"", ""Integer"", """",1
		P: ""FrontAxis"", ""int"", ""Integer"", """",1
		P: ""FrontAxisSign"", ""int"", ""Integer"", """",-1
		P: ""CoordAxis"", ""int"", ""Integer"", """",0
		P: ""CoordAxisSign"", ""int"", ""Integer"", """",1
		P: ""OriginalUpAxis"", ""int"", ""Integer"", """",-1
		P: ""OriginalUpAxisSign"", ""int"", ""Integer"", """",1
		P: ""UnitScaleFactor"", ""double"", ""Number"", """",30.48
		P: ""OriginalUnitScaleFactor"", ""double"", ""Number"", """",1
		P: ""AmbientColor"", ""ColorRGB"", ""Color"", """",0.5,0.5,0.5
		P: ""DefaultCamera"", ""KString"", """", """", ""Producer Perspective""
		P: ""TimeMode"", ""enum"", """", """",0
		P: ""TimeSpanStart"", ""KTime"", ""Time"", """",0
		P: ""TimeSpanStop"", ""KTime"", ""Time"", """",46186158000
		P: ""CustomFrameRate"", ""double"", ""Number"", """",-1
		P: ""TimeProtocol"", ""enum"", """", """",2
		P: ""SnapOnFrameMode"", ""enum"", """", """",0
		P: ""TimeMarker"", ""Compound"", """", """"
		P: ""CurrentTimeMarker"", ""int"", ""Integer"", """",-1

    }";
                    tmp = settings;
                }
                sw.WriteLine(tmp);
            }

            sw.Flush();
            sw.Close();
            sr.Close();
        }


        public void Translate(Vector3 translation)
        {
            foreach (ScannedPoint p in AllPoints)
            {
                p.point = Vector3.Add(p.point, translation);
            }
        }
    }
}
