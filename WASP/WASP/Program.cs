﻿using System;
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
            Dictionary<string, dynamic> data = jss.Deserialize<Dictionary<string, dynamic>>(GetRequestPostData(request));
            string response = "";
            try
            {
                response = routes[request.Url.ToString()](data);
                
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
            routes.Add(basePrefix + "defineForumPolicy/", ServiceFacade.defineForumPolicy);
            routes.Add(basePrefix + "subscribeToForum/", ServiceFacade.subscribeToForum);
            routes.Add(basePrefix + "createThread/", ServiceFacade.createThread);
            routes.Add(basePrefix + "createReplyPost/", ServiceFacade.createReplyPost);
            routes.Add(basePrefix + "createSubForum/", ServiceFacade.createSubForum);
            routes.Add(basePrefix + "sendMessage/", ServiceFacade.sendMessage);
            routes.Add(basePrefix + "addModerator/", ServiceFacade.addModerator);
            routes.Add(basePrefix + "updateModeratorTerm/", ServiceFacade.updateModeratorTerm);
            routes.Add(basePrefix + "confirmEmail/", ServiceFacade.confirmEmail);
            routes.Add(basePrefix + "deletePost/", ServiceFacade.deletePost);
            routes.Add(basePrefix + "editPost/", ServiceFacade.editPost);
            routes.Add(basePrefix + "deleteModerator/", ServiceFacade.deleteModerator);
            routes.Add(basePrefix + "addAdmin/", ServiceFacade.addAdmin);
            routes.Add(basePrefix + "getAllNotificationses/", ServiceFacade.getAllNotificationses);
            routes.Add(basePrefix + "getNewNotificationses/", ServiceFacade.getNewNotificationses);
            routes.Add(basePrefix + "subForumTotalMessages/", ServiceFacade.subForumTotalMessages);
            routes.Add(basePrefix + "postsByMember/", ServiceFacade.postsByMember);
            routes.Add(basePrefix + "moderatorReport/", ServiceFacade.moderatorReport);
            routes.Add(basePrefix + "totalForums/", ServiceFacade.totalForums);
            routes.Add(basePrefix + "membersInDifferentForums/", ServiceFacade.membersInDifferentForums);
            routes.Add(basePrefix + "login/", ServiceFacade.login);
            routes.Add(basePrefix + "loginSU/", ServiceFacade.loginSU);
            routes.Add(basePrefix + "getThread/", ServiceFacade.getThread);
            routes.Add(basePrefix + "getThreads/", ServiceFacade.getThreads);
            routes.Add(basePrefix + "getReplys/", ServiceFacade.getReplys);
            routes.Add(basePrefix + "getForum/", ServiceFacade.getForum);
            routes.Add(basePrefix + "getSubforum/", ServiceFacade.getSubforum);
            routes.Add(basePrefix + "getModerators/", ServiceFacade.getModerators);
            routes.Add(basePrefix + "getModeratorTermTime/", ServiceFacade.getModeratorTermTime);
            routes.Add(basePrefix + "getAllForums/", ServiceFacade.getAllForums);
            routes.Add(basePrefix + "getAdmins/", ServiceFacade.getAdmins);
            routes.Add(basePrefix + "getMembers/", ServiceFacade.getMembers);
            routes.Add(basePrefix + "getSubforums/", ServiceFacade.getSubforums);
            routes.Add(basePrefix + "getAdmin/", ServiceFacade.getAdmin);

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
