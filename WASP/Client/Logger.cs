using System;
using System.IO;

namespace WASP.LoggerPC
{
    public class Logger
    {
        private string filePath;

        public Logger (string path)
        {
            filePath = path;
        }

        public void writeToFile(string msg)
        {
            //if(!File.Exists(filePath))
            //    File.Create(filePath);
            msg = DateTime.Now.ToString() + ": " + msg; 
            
            StreamWriter tw = new StreamWriter(filePath, true);
            tw.WriteLine(msg);
            tw.Close();
        }

        public void deleteFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }
    }
}
