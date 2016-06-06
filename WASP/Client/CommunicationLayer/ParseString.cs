using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.DataClasses;
using System.Web.Script.Serialization;

namespace Client.CommunicationLayer
{
    class ParseString
    {

        public ParseString()
        {
        }
        public User parseStringToUser(string json, bool auth, CL c)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(json);
            string username = dict["username"];
            int id = dict["id"];
            string password = dict["password"];
            if (auth)
                c._auth = dict["auth"];
            string name = dict["name"];
            string email = dict["email"];
            User su = new User(id, name, username, email, password);
            su.client_session = c._auth;
            return su;
        }

        public int parseStringToNum(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            int number = dict["number"];
            return number;
        }

        public SuperUser parseStringToSuperUser(string res, CL c)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string username = dict["username"];
            int id = dict["id"];
            string password = dict["password"];
            c._auth = dict["auth"];
            SuperUser su = new SuperUser("", username, id, "", password);
            su.client_session = c._auth;
            return su;
        }

        public SuperUser parseStringToSuperUser(string res, string email, string name, CL c)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string username = dict["username"];
            int id = dict["id"];
            string password = dict["password"];
            c._auth = dict["auth"];
            SuperUser u = new SuperUser(name, username, id, email, password);
            return u;

        }

        public List<Post> parseStringToPosts(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLPost>>(res);
            List<Post> ret = new List<Post>();
            foreach (CLPost cl in dict)
            {
                User author = new User();
                author.id = cl.authorid;
                Post inReplyTo = new Post();
                inReplyTo.id = cl.replypostid;
                Post p = new Post(cl.title, cl.content, author, cl.container, inReplyTo);
                p.id = cl.postid;
                ret.Add(p);
            }
            return ret;
        }

        public Post parseStringToPost(string res, bool reply = false)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string title = dict["title"];
            string content = dict["content"];
            int authorid = dict["authorid"];
            User author = new User();
            author.id = authorid;
            int container = dict["subforumid"];
            Post irp = new Post();
            irp.id = dict["replypostid"];
            if (irp.id == -1)
                irp = null;
            Post p = new Post(title, content, author, container, irp);
            p.id = dict["postid"];
            if (irp != null)
                irp.replies.Add(p);
            return p;
        }

        public Forum parseStringToForum(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string name = dict["title"];
            string description = dict["description"];
            int adminid = dict["adminid"];
            int forumid = dict["forumid"];
            User user = new User();
            user.id = adminid;
            Forum f = new Forum(name, description, user, new Policy(0, 0, false, 0, 0));
            f.id = forumid;
            return f;
        }

        public Subforum parseStringToSubforum(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string name = dict["name"];
            string description = dict["description"];
            int moderatorid = dict["moderatorid"];
            int sfid = dict["id"];
            Moderator mod = new Moderator();
            User user = new User();
            mod.user = user;
            mod.user.id = moderatorid;
            Subforum sf = new Subforum(name, description, mod, new DateTime());
            sf.id = sfid;
            return sf;
        }

        public List<Moderator> parseStringToModerators(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLModerator>>(res);
            List<Moderator> ret = new List<Moderator>();
            foreach (CLModerator cl in dict)
            {
                User user = new User();
                user.id = cl.moderatorid;
                User appoint = new User();
                appoint.id = cl.appointedbyid;
                Moderator mod = new Moderator(user, new DateTime(), appoint);
                mod.subForumID = cl.subforumid;
                ret.Add(mod);
            }
            return ret;
        }

        public Moderator parseStringToModerator(string res, DateTime dt)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            int modid = dict["moderatorid"];
            int subforumid = dict["subforumid"];
            int appointedbyid = dict["appointedbyid"];
            User user = new User();
            user.id = modid;
            User appoint = new User();
            appoint.id = appointedbyid;
            Moderator mod = new Moderator(user, dt, appoint);
            mod.subForumID = subforumid;
            return mod;
        }

        public Moderator parseStringToModerator(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            int modid = dict["moderatorid"];
            int subforumid = dict["subforumid"];
            int appointedbyid = dict["appointedbyid"];
            User user = new User();
            user.id = modid;
            User appoint = new User();
            appoint.id = appointedbyid;
            Moderator mod = new Moderator(user, DateTime.Now, appoint);
            mod.subForumID = subforumid;
            return mod;
        }

        public List<Forum> parseStringToForums(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLForum>>(res);
            List<Forum> ret = new List<Forum>();
            foreach (CLForum cl in dict)
            {
                User user = new User();
                user.id = cl.adminid;
                Forum f = new Forum(cl.name, cl.description, user, new Policy(0, 0, false, 0, 0));
                f.id = cl.forumid;
                ret.Add(f);
            }
            return ret;
        }

        public List<Admin> parseStringToAdmins(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLUser>>(res);
            List<Admin> ret = new List<Admin>();
            foreach (CLUser cl in dict)
            {
                User u = new User(cl.id, cl.name, cl.username, cl.email, cl.password);
                Admin ad = new Admin(u);
                ret.Add(ad);
            }
            return ret;
        }

        public List<Subforum> parseStringToSubforums(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLSubforum>>(res);
            List<Subforum> ret = new List<Subforum>();
            foreach (CLSubforum cl in dict)
            {
                Subforum sf = new Subforum();
                sf.id = cl.subforumid;
                sf.name = cl.name;
                sf.description = cl.description;
                ret.Add(sf);
            }
            return ret;
        }

        public Admin parseStringToAdmin(string res, CL c)
        {
            Admin admin = new Admin(parseStringToUser(res, false, c));
            return admin;
        }

        public List<User> parseStringToUsers(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLUser>>(res);
            List<User> ret = new List<User>();
            foreach (CLUser cl in dict)
            {
                User u = new User(cl.id, cl.name, cl.username, cl.email, cl.password);
                ret.Add(u);
            }
            return ret;
        }

        public List<User> parseStringToFriends(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLFriend>>(res);
            List<User> ret = new List<User>();
            if (dict != null)
            {
                foreach (CLFriend cl in dict)
                {
                    User u = new User(cl.id, cl.name, cl.username, null, null);
                    ret.Add(u);
                }
            }
            return ret;
        }

        public DateTime parseStringToDate(string res)
        {   //termtime
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            return DateTime.Parse(dict["termtime"]);
        }

        public List<Notification> parseStringToMessages(string res, bool isnew)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<List<CLNotification>>(res);
            List<Notification> ret = new List<Notification>();
            if (dict != null)
            {
                foreach (CLNotification cl in dict)
                {
                    Notification.Types t;
                    if (cl.type == CLNotification.Types.Message)
                        t = Notification.Types.Message;
                    else
                        t = Notification.Types.Post;
                    Notification n = new Notification(cl.id, cl.message, isnew, cl.source, cl.target, t);
                    ret.Add(n);
                }
            }
            return ret;
        }

        public ModeratorReport parseStringToModeratorReport(string res)
        {
            ModeratorReport mr = new ModeratorReport();
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            int forumid = dict["forum"];
            var moderators = dict["moderators"];
            foreach(var d in moderators)
            {
                int modid = d["id"];
                int sfid = d["subforum"];
                User mod = new User();
                mod.userName = d["username"];
                mod.id = modid;
                User appointedBy = new User();
                appointedBy.id = d["appointer"];
                Moderator m = new Moderator(mod, d["startdate"], appointedBy, sfid);
                mr.moderators.Add(m);
                var posts = d["posts"];
                List<Post> modposts = new List<Post>();
                foreach (var dp in posts)
                {
                    int pid = dp["postid"];
                    string title = dp["title"];
                    string content = dp["content"];
                    User author = new User();
                    author.id = modid;
                    var cont = dp["subforum"];
                    int container = cont["Id"];
                    Post inreply = new Post();
                    //inreply.id = dp["inreplyto"];
                    Post p = new Post(pid, title, content, author, container,null);
                    p.publishedAt = dp["publishedat"];
                    modposts.Add(p);
                }
                mr.ModeratorInsubForum.Add(modid, sfid);
                mr.moderatorsPosts.Add(modid, modposts);

            }

            return mr;
        }
    }
}
