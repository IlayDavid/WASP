using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Logger
{
    class ActionLogger : Logger
    {
        private string actionFilePath;

        public ActionLogger(string filePath)
        {
            actionFilePath = filePath;
        }

        public void writeToFile(string msg)
        {
            string currDate = DateTime.Now.ToString();
            msg = currDate+": " + msg; 

            if (!File.Exists(actionFilePath)) 
                File.Create(actionFilePath);

            StreamWriter tw = new StreamWriter(actionFilePath);
            tw.WriteLine(msg);
            tw.Close();
        }

    }
}
