using System;
using System.Linq;
using System.IO;
using LibraryModels;
using System.Collections.Generic;

namespace Drone_client_WebAPI {
    class FileHandler {
        //TODO: Folder structure is changed. Review every method.
        public FileHandler() {
            //Stuff to send
            if (!Directory.Exists("waiting")) {
                Directory.CreateDirectory("waiting");
            }
            //Stuff that's sent
            if (!Directory.Exists("done")) {
                Directory.CreateDirectory("done");
            }
            //Stuff that could not be sent
            if (!Directory.Exists("failed")) {
                Directory.CreateDirectory("failed");
            }
        }
        private string logPath = "log.txt";
        //4MB as max since the current maximum of an HTTP request is 4MB as well.
        int maxFileSize = 4000000;

        public string CheckNewFolder()
        {
            if (Directory.GetDirectories("waiting").Count() > 0)
            {
                return Directory.GetDirectories("waiting").First();
            }
            else
            {
                return null;
            }
        }    
        public List<Reading> GetAllReadings(string folderPath) {
            List<Reading> readings = new List<Reading>();
            int index = 1;
            foreach(string reading in Directory.GetFiles(folderPath)) {
                readings.Add(new Reading(index,folderPath));
                index++;
            }
            readings.ForEach(e => e.FillContent());
            return readings;
        } 
        public void AppendToLog(string msg) {
            using (StreamWriter write = File.AppendText(logPath)) {
                write.WriteLine(DateTime.Now + " - " + msg);
            }
        }
        public void MoveDirToDir(string folderPath, string destination) {
            string destPath = destination + @"\" + Utilities.GetFileName(folderPath);
            Directory.Move(folderPath, destPath);
            AppendToLog(Utilities.GetFileName(folderPath) + " has been moved to the " + destination + " folder.");
        }
        public bool ValidateFolder(string folderPath){
            bool correct = true;
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files) {
                if (File.GetAttributes(file) == FileAttributes.Temporary) {
                    AppendToLog(Utilities.GetFileName(file) + " seemed to be a temp file in the waiting folder. Program did not send.");
                    correct = false;
                }
                if (new FileInfo(file).Length > maxFileSize) {
                    AppendToLog(Utilities.GetFileName(folderPath) + " was suspiciously big. Program did not send.");
                    correct = false;
                }
            }
            return correct;
        }

    }
}
