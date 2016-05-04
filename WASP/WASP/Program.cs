using System;
using System.Net;
using WASP.Server;
using WASP.Service;
using System.Web.Script.Serialization;
namespace WASP
{
    class Program
    {
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        static string basePrefix = "http://localhost:8080/";
        static string[] prefixes = {
                basePrefix + "isinitialize/",
                basePrefix + "initialize/"
        };
        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static string SendResponse(HttpListenerRequest request)
        {
            
            const string pref = "http://localhost:8080/";
            switch (request.Url.ToString())
            {
                case pref + "isinitialize/": return ServiceFacade.isInitialize(); break;
                //case pref + "initialize/": return ServiceFacade.Initialize(jss.Deserialize<Dictionary<string, dynamic>>(request.);
            }
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
        }
        static void Main(string[] args)
        {


            WebServer ws = new WebServer(SendResponse, prefixes);
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
    }
}
