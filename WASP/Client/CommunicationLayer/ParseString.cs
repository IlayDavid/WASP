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
            return su;
        }

        public SuperUser parseStringToSuperUser(string res, CL c)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string username = dict["username"];
            int id = dict["id"];
            string password = dict["password"];
            c._auth = dict["auth"];
            string name = dict["name"];
            string email = dict["email"];
            SuperUser su = new SuperUser(name, username, id, email, password);
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
                ret.Add(new Post(cl.title, cl.content, author, cl.container, inReplyTo));
            }
            return ret;
        }

        public Post parseStringToPost(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string title = dict["title"];
            string content = dict["content"];
            int authorid = dict["authorid"];
            User author = new User();
            author.id = authorid;
            int container = dict["subforumid"];
            int replypostid = dict["replypostid"];
            Post inReplyTo = new Post();
            inReplyTo.id = replypostid;
            Post p = new Post(title, content, author, container, inReplyTo);
            return p;
        }

        public Forum parseStringToForum(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string name = dict["title"];
            string description = dict["description"];
            int adminid = dict["authorid"];
            User user = new User();
            user.id = adminid;
            Forum f = new Forum(name, description, user, new Policy(0, 0, false, 0, 0));
            return f;
        }

        public Subforum parseStringToSubforum(string res)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(res);
            string name = dict["title"];
            string description = dict["description"];
            int moderatorid = dict["moderatorid"];
            Moderator mod = new Moderator();
            User user = new User();
            mod.user.id = moderatorid;
            Subforum sf = new Subforum(name, description, mod, new DateTime());
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
            Moderator mod = new Moderator(user, new DateTime(), appoint);
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
                ret.Add(new Forum(cl.name, cl.description, user, new Policy(0, 0, false, 0, 0)));
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
    }
}
