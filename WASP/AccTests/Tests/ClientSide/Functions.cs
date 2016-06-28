using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccTests.Tests
{
    public class ClientFunctions
    {

        public static SuperUser InitialSystem(WASPClientBridge proj)
        {
            return proj.initialize("Moshe", "SuperUser", 2, "moshe@post.bgu.ac.il", "moshe123");
        }

        public static Tuple<Forum, Admin> CreateSpecForum(WASPClientBridge proj, SuperUser supervisor)
        {
            string userName = "admin";
            Forum forum = proj.createForum("start-up", "ideas", 1, "david", "david", "david@post.bgu.ac.il", "david123", new Policy(5, 5, false, 5, 500));
            var admin = proj.getAdmin(1, forum.id);

            return new Tuple<Forum, Admin>(forum, admin);
        }

        public static Tuple<Subforum, User> CreateSpecSubForum(WASPClientBridge proj, Admin admin, Forum forum)
        {
            var user = proj.subscribeToForum(2, "ilanB", "ilan", "ilanB@post.bgu.ac.il",
                                        "ilan123", forum.id, new List<string>(), false);
            Subforum subforum = proj.createSubForum("sub1", "blah", 2, DateTime.Now.AddDays(100));
            var moderator = proj.login(user.userName, "ilan123", forum.id, "");
            //proj.logout();
            return new Tuple<Subforum, User>(subforum, moderator);
        }

        public static User SubscribeSpecMember(WASPClientBridge proj, Forum forum)
        {
            return proj.subscribeToForum(3, "arielB", "ariel", "arielB@post.bgu.ac.il", "ariel123", forum.id, new List<string>(), false);
        }

        public static Tuple<Forum, Admin> CreateSpecForum2(WASPClientBridge proj, SuperUser supervisor)
        {
            string userName = "admin1";
            Forum forum = proj.createForum("start-up", "ideas", 4,
                                            "ronen", "ronen", "ronen@post.bgu.ac.il", "ronen123", new Policy(5, 5, false, 5, 500));
            Admin admin = proj.getAdmin(4, forum.id);

            return new Tuple<Forum, Admin>(forum, admin);
        }

        public static Tuple<Subforum, User> CreateSpecSubForum2(WASPClientBridge proj, Admin admin, Forum forum)
        {
            var user = proj.subscribeToForum(5, "amitB", "amit", "amitB@post.bgu.ac.il",
                                        "amit123", forum.id, new List<string>(), false);
            Subforum subforum = proj.createSubForum("subbbbb2", "blah", 5, DateTime.Now.AddDays(100));
            var moderator = proj.login(user.userName, user.password, forum.id, "");
            //proj.logout();
            return new Tuple<Subforum, User>(subforum, moderator);
        }

        public static User SubscribeSpecMember2(WASPClientBridge proj, Forum forum)
        {
            return proj.subscribeToForum(6, "shlomeD", "shlome", "shlomeD@post.bgu.ac.il", "shlome123", forum.id, new List<string>(), false);
        }

    }
}
