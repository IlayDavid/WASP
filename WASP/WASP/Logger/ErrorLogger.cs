using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Logger
{
    class ErrorLogger : Logger
    {
        private string errorFilePath;

        public ErrorLogger(string filePath)
        {
            errorFilePath = filePath;
        }

        public void writeToFile(string err)
        {
            string currDate = DateTime.Now.ToString();
            err = currDate + ": " + err;

            if (!File.Exists(errorFilePath))
                File.Create(errorFilePath);

            StreamWriter tw = new StreamWriter(errorFilePath);
            tw.WriteLine(err);
            tw.Close();
        }
    }
}
