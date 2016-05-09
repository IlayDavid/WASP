using System;
using System.Linq;
using WASP;
using WASP.DataClasses;

namespace AccTests.Tests
{
    public class Functions
    {
        
        public static SuperUser InitialSystem(WASPBridge proj)
        {
            return proj.initialize("Moshe", "SuperUser",0, "moshe@post.bgu.ac.il", "moshe123");
        }

        public static Tuple<Forum,Admin> CreateSpecForum(WASPBridge proj, SuperUser supervisor)
        {
            string userName = "admin";
            Forum forum = proj.createForum(0, "start-up", "ideas", 1,"david","david", "david@post.bgu.ac.il", "david123", new Policy());
            var admin = proj.getAdmin(0, forum.Id, 1);

            return new Tuple<Forum, Admin>(forum, admin);
        }

        public static Tuple<Subforum, User> CreateSpecSubForum(WASPBridge proj, Admin admin, Forum forum)
        {
            var user = proj.subscribeToForum(2,"ilanB", "ilan", "ilanB@post.bgu.ac.il",
                                        "ilan123",forum.Id);
            Subforum subforum =  proj.createSubForum(1, forum.Id, "sub1", "blah", 2, DateTime.Now.AddDays(100));
            var moderator = proj.login(user.Username, user.Password, forum.Id);
            return new Tuple<Subforum, User>(subforum, moderator);
        }

        public static User SubscribeSpecMember(WASPBridge proj, Forum forum)
        {
            return proj.subscribeToForum(3,"arielB", "ariel", "arielB@post.bgu.ac.il", "ariel123", forum.Id);
        }

        public static Tuple<Forum, Admin> CreateSpecForum2(WASPBridge proj, SuperUser supervisor)
        {
            string userName = "admin1";
            Forum forum = proj.createForum(0, "start-up", "ideas", 4,
                                            "ronen","ronen", "ronen@post.bgu.ac.il", "ronen123", new Policy());
            Admin admin = proj.getAdmin(0, forum.Id,4);

            return new Tuple<Forum, Admin>(forum, admin);
        }

        public static Tuple<Subforum, User> CreateSpecSubForum2(WASPBridge proj, Admin admin, Forum forum)
        {
            var user= proj.subscribeToForum(5,"amitB", "amit", "amitB@post.bgu.ac.il",
                                        "amit123", forum.Id);
            Subforum subforum = proj.createSubForum(4,forum.Id, "subbbbb2", "blah", 5, DateTime.Now.AddDays(100));
            var moderator = proj.login(user.Username, user.Password, forum.Id);
            return new Tuple<Subforum, User>(subforum, moderator);
        }

        public static User SubscribeSpecMember2(WASPBridge proj, Forum forum)
        {
            return proj.subscribeToForum(6,"shlomeD", "shlome", "shlomeD@post.bgu.ac.il", "shlome123", forum.Id);
        }

    }
}
