using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Logger
{
    static class Logger
    {
        private static string filePath;

        public static void Initialize(string path)
        {
            filePath = path;
        }

        public static void writeToFile(string msg)
        {
            if(!File.Exists(filePath))
                File.Create(filePath);
            msg = DateTime.Now.ToString() + ": " + msg; 

            StreamWriter tw = new StreamWriter(filePath);
            tw.WriteLine(msg);
            tw.Close();
        }
    }
}
