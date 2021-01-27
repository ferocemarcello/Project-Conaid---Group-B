using System;
using LibraryModels;
using System.Collections.Generic;

namespace Drone_client_WebAPI
{
    class Controller
    {
        private FileHandler fileHandler = new FileHandler();
        static void Main()
        {
            Controller c = new Controller();
            c.startCycle();
        }
        private void startCycle()
        {
            while(true)
            {
                string path = fileHandler.CheckNewFolder();
                if(path != null)
                {
                    SendFiles(path);
                }
                System.Threading.Thread.Sleep(10000);
            }
        }
        public void SendFiles(string folderPath)
        {
            if(fileHandler.ValidateFolder(folderPath))
            {
                try
                {
                    //Buildingindex and password are hardcoded now.
                    ServerCoordinator.ConnectToServer(1,"password");
                    ServerCoordinator.PostReadingsAsync(fileHandler.GetAllReadings(folderPath));
                    fileHandler.AppendToLog(Utilities.GetFileName(folderPath) + " was successfully uploaded.");
                    fileHandler.MoveDirToDir(folderPath, "done");
                }
                catch(Exception e)
                {
                    fileHandler.AppendToLog("Error message on " + Utilities.GetFileName(folderPath) + " : " + e.Message);
                    fileHandler.MoveDirToDir(folderPath, "failed");
                }
            }
            else
            {
                fileHandler.MoveDirToDir(folderPath, "failed");
            }
        }

    }
}