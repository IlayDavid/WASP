using System;
using WASP;
using WAsp

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
            Subforum subforum =  proj.createSubForum(admin, "sub1", "blah", moderator);

            return new Tuple<Subforum, Member>(subforum, moderator);
        }
    }
}
