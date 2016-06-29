using System;
using System.Net;
using WASP.Server;
using WASP.Service;
using WASP.Exceptions;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using WASP.DataClasses;
using System.Threading;

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
            Console.WriteLine(url);
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

            DAL2 myDal = new DALSQL();
            myDal.Clean();
            ServiceFacade.webInitialize();
            Policy policy = new Policy();
            string[] strs = new string[5];
            Policy policy2 = new Policy(999, Policy.PostDeletePolicy.ModeratorAndAdmin, TimeSpan.MaxValue, false, TimeSpan.MinValue, 100, strs, true);
            Policy newpolicy = myDal.CreatePolicy(policy);
            Policy newpolicy2= myDal.CreatePolicy(policy2);
            Forum forum1 = new Forum(-1, "Sports", "All about sports", newpolicy);
            Forum forum2 = new Forum(-1, "General", "Everything else goes here", newpolicy2);
            Forum newforum1 = myDal.CreateForum(forum1);
            Forum newforum2 = myDal.CreateForum(forum2);
            /*
            User user1 = new User(10, "edan", "edan", "email@email.com", "123456", newforum1);
            User user2 = new User(11, "ariel", "ariel", "ariel@ariel.com", "123456", newforum1);
            User user3 = new User(12, "ilay", "ilay", "email2@email.com", "123456", newforum2);
            User user4 = new User(13, "matan", "matan", "email3@email.com", "123456", newforum2);
            User newuser1 = myDal.CreateUser(user1);
            User newuser2 = myDal.CreateUser(user2);
            User newuser3 = myDal.CreateUser(user3);
            User newuser4 = myDal.CreateUser(user4);
            */
            var hashPass = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";
            User user1 = new User(10, "admin_1_sports", "admin_1_sports", "email@email.com", hashPass, newforum1);
            User user2 = new User(11, "moderator_1_sports", "moderator_1_sports", "ariel@ariel.com", hashPass, newforum1);
            User user3 = new User(12, "admin_1_general", "admin_1_general", "email2@email.com", hashPass, newforum2);
            User user4 = new User(13, "moderator_1_general", "moderator_1_general", "email3@email.com", hashPass, newforum2);
            User user5 = new User(14, "ariel", "ariel", "ar2ie2l@ariel.com", hashPass, newforum1);
            User user6 = new User(15, "ilay", "ilay", "email32@email.com", hashPass, newforum2);
            User newuser1 = myDal.CreateUser(user1);
            User newuser2 = myDal.CreateUser(user2);
            User newuser3 = myDal.CreateUser(user3);
            User newuser4 = myDal.CreateUser(user4);
            User newuser5 = myDal.CreateUser(user5);
            User newuser6 = myDal.CreateUser(user6);
            Subforum sf1 = new Subforum(-1, "Soccer", "Soccer games", newforum1);
            Subforum sf2 = new Subforum(-1, "Tenis", "Tenis games", newforum1);
            Subforum sf3 = new Subforum(-1, "Cats", "Cats are fun", newforum2);
            Subforum sf4 = new Subforum(-1, "Cities", "Talk about cities here", newforum2);
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

            Post post1 = new Post(-1, "we meet tommrow", "everyone are welcome", newuser1, DateTime.Now, null, newsf1, DateTime.Now);
            Post newpost1 = myDal.CreatePost(post1);
            Post reply = new Post(-1, "we meet tommrow", "where?", newuser2, DateTime.Now, newpost1, newsf1, DateTime.Now);
            Post newreply = myDal.CreatePost(reply);

            Post post3 = new Post(-1, "game cancelled", "no game today", newuser1, DateTime.Now, null, newsf1, DateTime.Now);
            Post newpost3 = myDal.CreatePost(post3);
            Post reply3 = new Post(-1, "game cancelled", "why", newuser2, DateTime.Now, newpost3, newsf1, DateTime.Now);
            Post newreply3 = myDal.CreatePost(reply3);

            Post anotherReply = new Post(-1, "we meet tommrow", "at the football field", newuser2, DateTime.Now, newpost1, newsf1, DateTime.Now);
            Post reply2reply = new Post(-1, "we meet tommrow", "when?", newuser1, DateTime.Now, newreply, newsf1, DateTime.Now);
            Post newanotherReply = myDal.CreatePost(anotherReply);
            Post newreply2reply = myDal.CreatePost(reply2reply);

            Post reply2reply2 = new Post(-1, "we meet tommrow", "coming", newuser1, DateTime.Now, newreply, newsf1, DateTime.Now);
            Post newreply2reply2 = myDal.CreatePost(reply2reply2);

            

            Post post5 = new Post(-1, "Ni hao", "i like cats", newuser3, DateTime.Now, null, newsf3, DateTime.Now);
            Post newpost5 = myDal.CreatePost(post5);
            Post reply5 = new Post(-1, "Ni hao", "me too", newuser4, DateTime.Now, newpost5, newsf3, DateTime.Now);
            Post newreply5 = myDal.CreatePost(reply5);

            Post post6 = new Post(-1, "my cat is cute", "right?", newuser3, DateTime.Now, null, newsf3, DateTime.Now);
            Post newpost6 = myDal.CreatePost(post6);
            Post reply6 = new Post(-1, "my cat is cute", "yes", newuser4, DateTime.Now, newpost6, newsf3, DateTime.Now);
            Post newreply6 = myDal.CreatePost(reply6);



            Post post7 = new Post(-1, "Tennis tommrow", "anybody?", newuser1, DateTime.Now, null, newsf2, DateTime.Now);
            Post newpost7 = myDal.CreatePost(post7);
            Post reply7 = new Post(-1, "Tennis tommrow", "sure, where?", newuser2, DateTime.Now, newpost7, newsf2, DateTime.Now);
            Post newreply7 = myDal.CreatePost(reply7);

            Post post8 = new Post(-1, "game cancelled", "no tennis game today", newuser1, DateTime.Now, null, newsf2, DateTime.Now);
            Post newpost8 = myDal.CreatePost(post8);
            Post reply8 = new Post(-1, "game cancelled", "say so earlier next time", newuser2, DateTime.Now, newpost8, newsf2, DateTime.Now);
            Post newreply8 = myDal.CreatePost(reply8);

            Post anotherReply9 = new Post(-1, "Tennis tommrow", "at the sports center", newuser2, DateTime.Now, newpost7, newsf2, DateTime.Now);
            Post reply2reply9 = new Post(-1, "Tennis tommrow", "when?", newuser1, DateTime.Now, newreply7, newsf2, DateTime.Now);
            Post newanotherReply9 = myDal.CreatePost(anotherReply9);
            Post newreply2reply9 = myDal.CreatePost(reply2reply9);

            Post reply2reply10 = new Post(-1, "Tennis tommrow", "coming", newuser1, DateTime.Now, newreply8, newsf2, DateTime.Now);
            Post newreply2reply10 = myDal.CreatePost(reply2reply10);
            

            Post post12 = new Post(-1, "Rome", "is a city in italy", newuser3, DateTime.Now, null, newsf4, DateTime.Now);
            Post newpost12 = myDal.CreatePost(post12);
            Post reply12 = new Post(-1, "Rome", "ORLY", newuser4, DateTime.Now, newpost12, newsf4, DateTime.Now);
            Post newreply12 = myDal.CreatePost(reply12);

            Post post13 = new Post(-1, "Jerusalem", "who wants to come", newuser3, DateTime.Now, null, newsf4, DateTime.Now);
            Post newpost13 = myDal.CreatePost(post13);
            Post reply13 = new Post(-1, "Jerusalem", "I do", newuser4, DateTime.Now, newpost13, newsf4, DateTime.Now);
            Post newreply13 = myDal.CreatePost(reply13);
        }
        static void Main(string[] args)
        {

            Populate(); //for web
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
            routes.Add(basePrefix + "loginHash/", ServiceFacade.loginHash);
            routes.Add(basePrefix + "restorePasswordByAnswers/", ServiceFacade.restorePasswordByAnswers);
            routes.Add(basePrefix + "logout/", ServiceFacade.logout);
            routes.Add(basePrefix + "Web/", ServiceFacade.GetWebFile);
            routes.Add(basePrefix + "Clean/", ServiceFacade.Clean);


            string[] prefixes = System.Linq.Enumerable.ToArray(routes.Keys);
            WebServer ws = new WebServer(SendResponse, prefixes);
            Thread notificationServerThread = new Thread(NotificationServer.Run);
            notificationServerThread.Start();

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

