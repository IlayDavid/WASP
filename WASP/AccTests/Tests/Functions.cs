using System;
using WASP.DataClasses;


namespace AccTests.Tests
{
    public class Functions
    {
        public static SuperUser InitialSystem(WASPBridge proj)
        {
            return proj.initialize("Moshe", "SuperUser", "moshe@post.bgu.ac.il", "moshe123");
        }

        public static Tuple<Forum,Member> CreateSpecForum(WASPBridge proj, SuperUser supervisor)
        {
            string userName = "admin";
            Forum forum = proj.createForum(supervisor, "start-up", "ideas", userName,
                                            "david", "david@post.bgu.ac.il", "david123");
            Member admin = proj.getAdmin(supervisor, forum, userName);

            return new Tuple<Forum, Member>(forum, admin);
        }

        public static Tuple<Subforum, Member> CreateSpecSubForum(WASPBridge proj, Member admin, Forum forum)
        {
            Member moderator = proj.subscribeToForum("ilanB", "ilan", "ilanB@post.bgu.ac.il",
                                        "ilan123", forum);
            Subforum subforum =  proj.createSubForum(admin, "sub1", "blah", moderator, DateTime.Now.AddDays(100));

            return new Tuple<Subforum, Member>(subforum, moderator);
        }

        public static Member SubscribeSpecMember(WASPBridge proj, Forum forum)
        {
            return proj.subscribeToForum("arielB", "ariel", "arielB@post.bgu.ac.il", "ariel123", forum);
        }

        public static Tuple<Forum, Member> CreateSpecForum2(WASPBridge proj, SuperUser supervisor)
        {
            string userName = "admin1";
            Forum forum = proj.createForum(supervisor, "start-up", "ideas", userName,
                                            "ronen", "ronen@post.bgu.ac.il", "ronen123");
            Member admin = proj.getAdmin(supervisor, forum, userName);

            return new Tuple<Forum, Member>(forum, admin);
        }

        public static Tuple<Subforum, Member> CreateSpecSubForum2(WASPBridge proj, Member admin, Forum forum)
        {
            Member moderator = proj.subscribeToForum("amitB", "amit", "amitB@post.bgu.ac.il",
                                        "amit123", forum);
            Subforum subforum = proj.createSubForum(admin, "subbbbb2", "blah", moderator, DateTime.Now.AddDays(100));

            return new Tuple<Subforum, Member>(subforum, moderator);
        }

        public static Member SubscribeSpecMember2(WASPBridge proj, Forum forum)
        {
            return proj.subscribeToForum("shlomeD", "shlome", "shlomeD@post.bgu.ac.il", "shlome123", forum);
        }

    }
}
