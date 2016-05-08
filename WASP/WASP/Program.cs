using System;
using System.Net;
using WASP.Server;
using WASP.Service;
using WASP.Exceptions;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace WASP
{
    class Program
    {
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        static string basePrefix = "http://localhost:8080/";
        static Dictionary<string, Func<Dictionary<string, dynamic>, string>> routes = new Dictionary<string, Func<Dictionary<string, dynamic>, string>>();

        public static string SendResponse(HttpListenerRequest request)
        {
            const string pref = "http://localhost:8080/";
            Dictionary<string, dynamic> data = jss.Deserialize<Dictionary<string, dynamic>>(GetRequestPostData(request));
            string response = "";
            try
            {
                response = routes[request.Url.ToString()](data);
                /*
                switch (request.Url.ToString())
                {
                    case pref + "addModerator/": response = ServiceFacade.createForum(data); break;
                    case pref + "updateModeratorTerm/": response = ServiceFacade.createForum(data); break;
                    case pref + "confirmEmail/": response = ServiceFacade.createForum(data); break;
                    case pref + "deletePost/": response = ServiceFacade.createForum(data); break;
                    case pref + "editPost/": response = ServiceFacade.createForum(data); break;
                    case pref + "deleteModerator/": response = ServiceFacade.createForum(data); break;
                    case pref + "addAdmin/": response = ServiceFacade.createForum(data); break;
                    case pref + "getAllNotificationses/": response = ServiceFacade.createForum(data); break;
                    case pref + "getNewNotificationses": response = ServiceFacade.createForum(data); break;
                    case pref + "subForumTotalMessages": response = ServiceFacade.createForum(data); break;
                    case pref + "postsByMember": response = ServiceFacade.createForum(data); break;
                    case pref + "moderatorReport": response = ServiceFacade.createForum(data); break;
                    case pref + "totalForums": response = ServiceFacade.createForum(data); break;
                    case pref + "membersInDifferentForums": response = ServiceFacade.createForum(data); break;
                    case pref + "login": response = ServiceFacade.createForum(data); break;
                    case pref + "loginSU": response = ServiceFacade.createForum(data); break;
                    case pref + "getThread": response = ServiceFacade.createForum(data); break;
                    case pref + "getReplys": response = ServiceFacade.createForum(data); break;
                    case pref + "getForum": response = ServiceFacade.createForum(data); break;
                    case pref + "getSubforum": response = ServiceFacade.createForum(data); break;
                    case pref + "getModerators": response = ServiceFacade.createForum(data); break;
                    case pref + "getModeratorTermTime": response = ServiceFacade.createForum(data); break;
                    case pref + "getAllForums": response = ServiceFacade.createForum(data); break;
                    case pref + "getAdmins": response = ServiceFacade.createForum(data); break;
                    case pref + "getMembers": response = ServiceFacade.createForum(data); break;
                    case pref + "getSubforums": response = ServiceFacade.createForum(data); break;
                    case pref + "getAdmin": response = ServiceFacade.createForum(data); break;
                }
                */
            }
            catch (WaspException e)
            {
                response = e.Message;
            }

            return response;
        }
        static void Main(string[] args)
        {
            string basePrefix = "http://localhost:8080/";
            routes.Add(basePrefix + "initialize/", ServiceFacade.initialize);
            routes.Add(basePrefix + "isInitialize/", ServiceFacade.isInitialize);
            routes.Add(basePrefix + "createForum/", ServiceFacade.createForum);
            /*routes.Add(basePrefix + "defineForumPolicy/", ServiceFacade.defineForumPolicy);
            routes.Add(basePrefix + "subscribeToForum/", ServiceFacade.subscribeToForum);
            routes.Add(basePrefix + "createThread/", ServiceFacade.createThread);
            routes.Add(basePrefix + "createReplyPost/", ServiceFacade.createReplyPost);
            routes.Add(basePrefix + "createSubForum/", ServiceFacade.createSubForum);
            routes.Add(basePrefix + "sendMessage/", ServiceFacade.sendMessage);
            */
            string[] prefixes = System.Linq.Enumerable.ToArray(routes.Keys);
            WebServer ws = new WebServer(SendResponse, prefixes);
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }

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
    }
}
