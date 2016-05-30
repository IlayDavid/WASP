using System;
using System.Net;
using WASP.Server;
using WASP.Service;
using WASP.Exceptions;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using WASP.DataClasses;
namespace WASP
{
    class Program
    {
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        static string basePrefix = "http://localhost:8080/";
        static Dictionary<string, Func<Dictionary<string, dynamic>, string>> routes = new Dictionary<string, Func<Dictionary<string, dynamic>, string>>();


        public static string SendResponse(HttpListenerRequest request)
        {
            Dictionary<string, dynamic> data;
            string url = request.Url.ToString();
            if (url.Contains("/Web/"))
            {
                data = new Dictionary<string, dynamic>();
                string filename = url.Substring(url.IndexOf("Web/"));
                int qsIndex = filename.IndexOf("?");
                if (qsIndex > -1)
                {
                    data["qs"] = filename.Substring(qsIndex + 1);
                    filename = filename.Substring(0, qsIndex);
                }
                data["file"] = filename;
                url = url.Substring(0, url.IndexOf("/Web/") + "/Web/".Length);
            }
            else
            {
                var dataStr = GetRequestPostData(request);
                Console.WriteLine(dataStr);
                data = jss.Deserialize<Dictionary<string, dynamic>>(dataStr);
            }


            string response = "";
            try
            {
                Console.WriteLine(url);
                response = routes[url](data);

            }
            catch (WaspException e)
            {
                response = e.Message;
            }

            return response;
        }

        static void Populate()
        {
            ServiceFacade.webInitialize();
            DAL2 myDal = new DALSQL();
            myDal.Clean();

            Policy policy = new Policy();
           
            Policy newpolicy = myDal.CreatePolicy(policy);
            Forum forum1 = new Forum(-1, "forum1", "description of forum1", newpolicy);
            Forum forum2 = new Forum(-1, "forum2", "description of forum2", newpolicy);
            Forum newforum1 = myDal.CreateForum(forum1);
            Forum newforum2 = myDal.CreateForum(forum2);

            User user1 = new User(10, "edan", "edan", "email@email.com", "123456", newforum1);
            User user2 = new User(11, "ariel", "ariel", "ariel@ariel.com", "123456", newforum1);
            User user3 = new User(12, "ilay", "ilay", "email2@email.com", "123456", newforum2);
            User user4 = new User(13, "matan", "matan", "email3@email.com", "123456", newforum2);
            User newuser1 = myDal.CreateUser(user1);
            User newuser2 = myDal.CreateUser(user2);
            User newuser3 = myDal.CreateUser(user3);
            User newuser4 = myDal.CreateUser(user4);

            Subforum sf1 = new Subforum(-1, "sf1", "desc1", newforum1);
            Subforum sf2 = new Subforum(-1, "sf2", "desc2", newforum1);
            Subforum sf3 = new Subforum(-1, "sf3", "desc3", newforum2);
            Subforum sf4 = new Subforum(-1, "sf4", "desc4", newforum2);
            Subforum newsf1 = myDal.CreateSubForum(sf1);
            Subforum newsf2 = myDal.CreateSubForum(sf2);
            Subforum newsf3 = myDal.CreateSubForum(sf3);
            Subforum newsf4 = myDal.CreateSubForum(sf4);


            Admin admin1 = new Admin(newuser1, newforum1);
            Admin admin2 = new Admin(newuser3, newforum2);
            Admin newadmin1 = myDal.CreateAdmin(admin1);
            Admin newadmin2 = myDal.CreateAdmin(admin2);

            Moderator mod1 = new Moderator(newuser2, DateTime.Now, newsf1, newadmin1);
            Moderator mod2 = new Moderator(newuser4, DateTime.Now, newsf3, newadmin2);
            Moderator newmod1 = myDal.CreateModerator(mod1);
            Moderator newmod2 = myDal.CreateModerator(mod2);

            Post post1 = new Post(-1, "title1", "someContent", newuser1, DateTime.Now, null, newsf1, DateTime.Now);
            Post newpost1 = myDal.CreatePost(post1);
            Post reply = new Post(-1, "title1", "someContent", newuser2, DateTime.Now, newpost1, newsf1, DateTime.Now);
            Post newreply = myDal.CreatePost(reply);

            Post anotherReply = new Post(-1, "title1", "oneMoreReply", newuser2, DateTime.Now, newpost1, newsf1, DateTime.Now);
            Post reply2reply = new Post(-1, "title", "reply2reply", newuser1, DateTime.Now, newreply, newsf1, DateTime.Now);
            Post newanotherReply = myDal.CreatePost(anotherReply);
            Post newreply2reply = myDal.CreatePost(reply2reply);

            Post reply2reply2 = new Post(-1, "title", "anotherReply2Reply", newuser1, DateTime.Now, newreply, newsf1, DateTime.Now);
            Post newreply2reply2 = myDal.CreatePost(reply2reply2);
            Post post2 = new Post(-1, "title2", "someContent2", newuser3, DateTime.Now, null, newsf2, DateTime.Now);
            Post newpost2 = myDal.CreatePost(post2);
            Post reply2 = new Post(-1, "title2", "someContent2", newuser4, DateTime.Now, newpost2, newsf2, DateTime.Now);
            myDal.CreatePost(reply2);
        }
        static void Main(string[] args)
        {

            //Populate(); //for web
            //add items to DB

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
            routes.Add(basePrefix + "getFriends/", ServiceFacade.getFriends);
            routes.Add(basePrefix + "addFriend/", ServiceFacade.addFriend);
            routes.Add(basePrefix + "Web/", ServiceFacade.GetWebFile);


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

