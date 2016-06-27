using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Domain;
using WASP.DataClasses;
using System.Web.Script.Serialization;
using System.IO;

namespace WASP.Service
{
    public static class ServiceFacade
    {
        private static IBL bl = null;
        
        private static Dictionary<string, LoginPair> loggedIn = new Dictionary<string, LoginPair>();
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        public static void webInitialize()
        {
            bl = new BLFacade();
        }
        private static string GenerateRandomHash()
        {
            return Guid.NewGuid().ToString();
        }
        public static bool isLoggedIn(string loginHash)
        {
            LoginPair lg;
            loggedIn.TryGetValue(loginHash, out lg);

            return lg != null;
        }
        
        public static LoginPair GetPair(string hash)
        {
            return loggedIn[hash];
        }
        public static string initialize(Dictionary<string, dynamic> data)
        {
            bl = new BLFacade();
            SuperUser su = ServiceFacade.bl.initialize(data["name"], data["username"], data["id"], data["email"], data["password"]);
            string key = GenerateRandomHash();
            su.Secret = key;
            loggedIn.Add(key, new LoginPair(su.Id));

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("auth", key);
            result.Add("id", su.Id);
            result.Add("username", su.Username);
            result.Add("password", su.Password);
            return jss.Serialize(result);
        }

        public static string isInitialize(Dictionary<string, dynamic> data)
        {
            int initialized = 1;
            if (ServiceFacade.bl == null || ServiceFacade.loggedIn == null || ServiceFacade.bl.isInitialize() == 0)
                initialized = 0;
            return initialized.ToString();
        }

        public static string createForum(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Policy policy = new Policy();
            Forum forum = bl.createForum(pair.UserId, data["forumname"], data["description"], data["adminid"], data["adminusername"], data["adminname"], data["email"], data["password"], policy);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("name", forum.Name);
            result.Add("description", forum.Description);
            result.Add("adminid", forum.GetAdmins()[0].Id);
            result.Add("forumid", forum.Id);
            return jss.Serialize(result);
        }



        public static string defineForumPolicy(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            int forumId = pair.ForumId;
            bool superUser = false;
            if (forumId == -1)
            {
                forumId = data["forum"];
                superUser = true;
            }
            bl.defineForumPolicy(pair.UserId, forumId, data["deletepost"], data["passperiod"], data["emailverf"], data["seniority"], data["usersload"], data["questions"], data["notifiyoffline"], superUser);

            return 1.ToString();
        }

        public static string subscribeToForum(Dictionary<string, dynamic> data)
        {
            User user = bl.subscribeToForum(data["userid"], data["username"], data["name"], data["email"], data["password"], data["forumid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("username", user.Username);
            result.Add("id", user.Id);
            result.Add("password", user.Password);
            result.Add("name", user.Name);
            result.Add("email", user.Email);
            return jss.Serialize(result);
        }

        public static string createThread(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Post post = bl.createThread(pair.UserId, pair.ForumId, data["title"], data["content"], data["subforumid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("title", post.Title);
            result.Add("postid", post.Id);
            result.Add("content", post.Content);
            result.Add("authorid", post.GetAuthor.Id);
            result.Add("subforumid", post.Subforum.Id);
            result.Add("replypostid", -1);
            return jss.Serialize(result);
        }

        public static string createReplyPost(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Post post = bl.createReplyPost(pair.UserId, pair.ForumId, data["content"], data["replytopostid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("title", post.Title);
            result.Add("postid", post.Id);
            result.Add("content", post.Content);
            result.Add("authorid", post.GetAuthor.Id);
            result.Add("subforumid", post.Subforum.Id);
            result.Add("replypostid", post.InReplyTo.Id);
            return jss.Serialize(result);
        }

        public static string createSubForum(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Subforum sf = bl.createSubForum(pair.UserId, pair.ForumId, data["name"], data["description"], data["moderatorid"], DateTime.Parse(data["termenddate"]));
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("name", sf.Name);
            result.Add("description", sf.Description);
            result.Add("moderatorid", data["moderatorid"]);
            result.Add("id", sf.Id);
            return jss.Serialize(result);
        }

        public static string sendMessage(Dictionary<string, dynamic> data)
        {
            //int userID, int forumID, int targetUserNameID, string message
            LoginPair pair = loggedIn[data["auth"]];
            bl.sendMessage(pair.UserId, pair.ForumId, data["target"], data["message"]);

            return 1.ToString();
        }

        public static string addModerator(Dictionary<string, dynamic> data)
        {   //subforumid, moderatorid , termenddate, auth
            LoginPair pair = loggedIn[data["auth"]];
            Moderator moderator = bl.addModerator(pair.UserId, pair.ForumId, data["moderatorid"], data["subforumid"], DateTime.Parse(data["termenddate"]));
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //moderatorid, appointedbyid, subforumid
            result.Add("moderatorid", moderator.Id);
            result.Add("appointedbyid", moderator.Appointer.Id);
            result.Add("subforumid", moderator.SubForum.Id);
            return jss.Serialize(result);
        }

        public static string updateModeratorTerm(Dictionary<string, dynamic> data)
        {   //subforumid, moderatorid, termenddate, auth
            LoginPair pair = loggedIn[data["auth"]];
            int result = bl.updateModeratorTerm(pair.UserId, pair.ForumId, data["moderatorid"], data["subforumid"], DateTime.Parse(data["termenddate"]));
            //ok message
            return result.ToString();
        }

        public static string confirmEmail(Dictionary<string, dynamic> data)
        {
            return "not yet implemented";
            //code (int) , auth
            LoginPair pair = loggedIn[data["auth"]];
            int moderator = bl.confirmEmail(pair.UserId, pair.ForumId);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //ok message
            return "not yet implemented";
        }

        public static string deletePost(Dictionary<string, dynamic> data)
        {   //postid , auth
            LoginPair pair = loggedIn[data["auth"]];
            int ok = bl.deletePost(pair.UserId, pair.ForumId, data["postid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //ok message
            return 1.ToString();
        }

        public static string editPost(Dictionary<string, dynamic> data)
        {   //postid, content, auth
            LoginPair pair = loggedIn[data["auth"]];
            int ok = bl.editPost(pair.UserId, pair.ForumId, data["postid"], data["content"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //ok message
            return 1.ToString();
        }

        public static string deleteModerator(Dictionary<string, dynamic> data)
        {   //subforumid, moderatorid, auth
            LoginPair pair = loggedIn[data["auth"]];
            int ok = bl.deleteModerator(pair.UserId, pair.ForumId, data["moderatorid"], data["subforumid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //ok message
            return 1.ToString();
        }

        public static string addAdmin(Dictionary<string, dynamic> data)
        {   //newadminid, auth
            LoginPair pair = loggedIn[data["auth"]];
            Admin admin = bl.addAdmin(pair.UserId, pair.ForumId, data["newadminid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //username, id, password, email, name
            result.Add("username", admin.User.Username);
            result.Add("id", admin.User.Id);
            result.Add("password", admin.User.Password);
            result.Add("email", admin.User.Email);
            result.Add("name", admin.User.Name);
            return jss.Serialize(result);
        }

        public static string getAllNotificationses(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Notification[] notifs = bl.getAllNotificationses(pair.UserId, pair.ForumId);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            foreach (Notification notif in notifs)
            {
                Dictionary<string, dynamic> ntf = new Dictionary<string, dynamic>();
                ntf.Add("type", notif.Type);
                ntf.Add("message", notif.Message);
                if (notif.Source == null)
                    ntf.Add("source", -1);
                else
                    ntf.Add("source", notif.Source.Id);
                ntf.Add("target", notif.Target.Id);
                ntf.Add("id", notif.Id);
                result.Add(ntf);
            }
            return jss.Serialize(result);
        }

        public static string getNewNotificationses(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Notification[] notifs = bl.getNewNotificationses(pair.UserId, pair.ForumId);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            foreach (Notification notif in notifs)
            {
                Dictionary<string, dynamic> ntf = new Dictionary<string, dynamic>();
                ntf.Add("type", notif.Type);
                ntf.Add("message", notif.Message);
                ntf.Add("source", notif.Source.Id);
                ntf.Add("target", notif.Target.Id);
                ntf.Add("id", notif.Id);
                result.Add(ntf);
            }
            return jss.Serialize(result);
        }

        public static string subForumTotalMessages(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            if (pair.ForumId > -1)
            {
                return "{ \"messagenumber\": " +
                bl.subForumTotalMessages(pair.UserId, pair.ForumId, data["subforumid"]) + "}";
            }
            else
            {
                return "{ \"messagenumber\": " +
                bl.subForumTotalMessages(pair.UserId, data["forum"], data["subforumid"], true) + "}";
            }
        }

        public static string postsByMember(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            int forumId = pair.ForumId;
            bool superUser = false;
            if (forumId == -1)
            {
                forumId = data["forum"];
                superUser = true;
            }
            Post[] posts = bl.postsByMember(pair.UserId, forumId, data["userid"], superUser);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            foreach (Post post in posts)
            {
                Dictionary<string, dynamic> postDict = new Dictionary<string, dynamic>();
                postDict.Add("title", post.Title);
                postDict.Add("content", post.Content);
                postDict.Add("authorid", post.GetAuthor.Id);
                postDict.Add("subforumid", post.Subforum.Id);
                postDict.Add("postid", post.Id);
                int irp = -1;
                if (post.InReplyTo != null)
                    irp = post.InReplyTo.Id;
                postDict.Add("replypostid", irp);
                result.Add(postDict);
            }
            return jss.Serialize(result);
        }

        public static string moderatorReport(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            int forumId = pair.ForumId;
            bool superUser = false;
            if (forumId == -1)
            {
                forumId = data["forum"];
                superUser = true;
            }
            DataClasses.Reports.ModeratorReport mr = bl.moderatorReport(pair.UserId, forumId, superUser);
            return jss.Serialize(mr.toJson());
        }

        public static string totalForums(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            return "{ \"forumnumber\": " +
                bl.totalForums(pair.UserId) + "}";
        }

        public static string membersInDifferentForums(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            User[] users = bl.membersInDifferentForums(pair.UserId);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            foreach (User user in users)
            {
                Dictionary<string, dynamic> postDict = new Dictionary<string, dynamic>();
                postDict.Add("username", user.Username);
                postDict.Add("password", user.Password);
                postDict.Add("id", user.Id);
                postDict.Add("email", user.Email);
                postDict.Add("name", user.Name);
                result.Add(postDict);
            }
            return jss.Serialize(result);
        }

        public static string login(Dictionary<string, dynamic> data)
        {
            User user = bl.login(data["username"], data["password"], data["forumid"]);
            if(user.Secret.Equals(""))
                user.Secret = GenerateRandomHash();
            else
            {
                dynamic key;
                data.TryGetValue("auth", out key);
                if (key == null || !user.Secret.Equals(key))
                    throw new WASP.Exceptions.LoginException("Secret key required for login.");
            }
            LoginPair pair = new LoginPair(user.Id, user.Forum.Id);
            loggedIn.Add(user.Secret, pair);

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("id", user.Id);
            result.Add("auth", user.Secret);
            result.Add("username", user.Username);
            result.Add("password", user.Password);
            result.Add("name", user.Name);
            result.Add("email", user.Email);
            result.Add("forum", pair.ForumId);
            return jss.Serialize(result);
        }

        public static string loginSU(Dictionary<string, dynamic> data)
        {
            SuperUser su = bl.loginSU(data["username"], data["password"]);
            if (su.Secret.Equals(""))
                su.Secret = GenerateRandomHash();
            else
            {
                dynamic key;
                data.TryGetValue("auth", out key);
                if (key == null || !su.Secret.Equals(key))
                    throw new WASP.Exceptions.LoginException("Secret key required for login.");
            }
            LoginPair pair = new LoginPair(su.Id);
            loggedIn.Add(su.Secret, pair);

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("auth", su.Secret);
            result.Add("id", su.Id);
            result.Add("forum", pair.ForumId);
            result.Add("username", su.Username);
            result.Add("password", su.Password);
            return jss.Serialize(result);
        }

        public static string loginHash(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (pair.ForumId == -1)
            {
                SuperUser su = SuperUser.Get(pair.UserId);
                result.Add("username", su.Username);
                result.Add("password", su.Password);
            }
            else
            {
                User user = User.Get(pair.UserId, pair.ForumId);
                result.Add("username", user.Username);
                result.Add("password", user.Password);
                result.Add("name", user.Name);
                result.Add("email", user.Email);
            }
            result.Add("auth", data["auth"]);
            result.Add("id", pair.UserId);
            result.Add("forum", pair.ForumId);
            return jss.Serialize(result);
        }

        public static string getThread(Dictionary<string, dynamic> data)
        {
            Post post = bl.getThread(-1, data["postid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("title", post.Title);
            result.Add("postid", post.Id);
            result.Add("content", post.Content);
            result.Add("authorid", post.GetAuthor.Id);
            result.Add("subforumid", post.Subforum.Id);
            result.Add("replypostid", -1);
            return jss.Serialize(result);
        }

        public static string getReplys(Dictionary<string, dynamic> data)
        {   //postid
            Post[] posts = bl.getReplys(-1, -1, data["postid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //title,  content,  authorid,  subforumid,  replypostid, postid
            foreach (Post p in posts)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("title", p.Title);
                res.Add("content", p.Content);
                res.Add("authorid", p.GetAuthor.Id);
                res.Add("subforumid", p.Subforum.Id);
                res.Add("replypostid", p.InReplyTo.Id);
                res.Add("postid", p.Id);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getThreads(Dictionary<string, dynamic> data)
        {   //subforumid
            Post[] posts = bl.getThreads(data["subforumid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //title,  content,  authorid,  subforumid,  replypostid
            foreach (Post p in posts)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("title", p.Title);
                res.Add("content", p.Content);
                res.Add("authorid", p.GetAuthor.Id);
                res.Add("subforumid", p.Subforum.Id);
                res.Add("replypostid", -1);
                res.Add("postid", p.Id);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getForum(Dictionary<string, dynamic> data)
        {   //forumid
            Forum f = bl.getForum(data["forumid"]);
            
            Dictionary<string, dynamic> policy = new Dictionary<string, dynamic>();
            //PostDeletePolicy deletePost, TimeSpan passwordPeriod, bool emailVerification, TimeSpan minimumSeniority, int usersLoad, string[] questions
            policy.Add("deletepost", f.Policy.SelectedPostDeletePolicy.ToString());
            policy.Add("passperiod", f.Policy.PasswordTimeSpan);
            policy.Add("emailverf", f.Policy.EmailVerfication);
            policy.Add("seniority", f.Policy.MinimumSeniority);
            policy.Add("usersload", f.Policy.UsersLoad);
            policy.Add("questions", f.Policy.Questions);

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //name, description, adminid
            result.Add("name", f.Name);
            result.Add("description", f.Description);
            result.Add("adminid", f.GetAdmins().ElementAt(0).Id);
            result.Add("forumid", f.Id);
            result.Add("policy", policy);
            return jss.Serialize(result);
        }

        public static string getSubforum(Dictionary<string, dynamic> data)
        {   //subforumid
            Subforum sf = bl.getSubforum(-1, data["subforumid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //name, description, moderatorid
            result.Add("name", sf.Name);
            result.Add("description", sf.Description);
            result.Add("moderatorid", sf.GetAllModerators().ElementAt(0).Id);
            return jss.Serialize(result);
        }

        public static string getModerators(Dictionary<string, dynamic> data)
        {   //subforumid
            Moderator[] moderators = bl.getModerators(-1, data["subforumid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //subforumid, appointedbyid, moderatorid
            foreach (Moderator m in moderators)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("subforumid", m.SubForum.Id);
                res.Add("appointedbyid", m.Appointer.Id);
                res.Add("moderatorid", m.Id);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getModeratorTermTime(Dictionary<string, dynamic> data)
        {   //subforumid, moderatorid
            DateTime date = bl.getModeratorTermTime(-1, -1, data["moderatorid"], data["subforumid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //termtime
            result.Add("termtime", date.ToString());
            return jss.Serialize(result);
        }

        public static string getAllForums(Dictionary<string, dynamic> data)
        {
            Forum[] forums = bl.getAllForums();
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //name, description, adminid
            foreach (Forum f in forums)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("name", f.Name);
                res.Add("description", f.Description);
                res.Add("adminid", f.GetAdmins().ElementAt(0).Id);
                res.Add("forumid", f.Id);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getAdmins(Dictionary<string, dynamic> data)
        {   //forumid, 
            Admin[] admins = bl.getAdmins(-1, data["forumid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //username, id, password, email, name
            foreach (Admin a in admins)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("username", a.User.Username);
                res.Add("id", a.User.Id);
                res.Add("password", a.User.Password);
                res.Add("email", a.User.Email);
                res.Add("name", a.User.Name);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getMembers(Dictionary<string, dynamic> data)
        {   //forumid, 
            User[] members = bl.getMembers(-1, data["forumid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //username, id, password, email, name
            foreach (User u in members)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("username", u.Username);
                res.Add("id", u.Id);
                res.Add("password", u.Password);
                res.Add("email", u.Email);
                res.Add("name", u.Name);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getSubforums(Dictionary<string, dynamic> data)
        {   //forumid
            Subforum[] subforums = bl.getSubforums(data["forumid"]);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            //subforumid, name, description
            foreach (Subforum sf in subforums)
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
                res.Add("name", sf.Name);
                res.Add("description", sf.Description);
                res.Add("subforumid", sf.Id);
                result.Add(res);
            }
            return jss.Serialize(result);
        }

        public static string getAdmin(Dictionary<string, dynamic> data)
        {   //adminid
            LoginPair pair = loggedIn[data["auth"]];
            Admin a = bl.getAdmin(pair.UserId, pair.ForumId, data["adminid"]);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            //username, id, password, email, name
            User u = a.User;
            result.Add("name", u.Name);
            result.Add("username", u.Username);
            result.Add("id", u.Id);
            result.Add("password", u.Password);
            result.Add("email", u.Email);
            return jss.Serialize(result);
        }

        public static string getFriends(Dictionary<string, dynamic> data)
        {   //adminid
            LoginPair pair = loggedIn[data["auth"]];
            User u = User.Get(pair.UserId, pair.ForumId);
            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            foreach (User friend in u.GetAllFriends())
            {
                Dictionary<string, dynamic> friendDict = new Dictionary<string, dynamic>();
                friendDict.Add("id", friend.Id);
                friendDict.Add("name", friend.Name);
                friendDict.Add("username", friend.Username);
                result.Add(friendDict);
            }

            return jss.Serialize(result);
        }

        public static string addFriend(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            return bl.addFriend(pair.UserId, pair.ForumId, data["friend"]).ToString();
        }

        public static string GetWebFile(Dictionary<string, dynamic> data)
        {
            return File.ReadAllText(data["file"]);
        }
    }
}
