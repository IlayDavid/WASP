using System;
using System.Collections.Generic;
using System.Linq;

namespace WASP.Domain
{
    public class BL : IBL
    {
        private bool _initialized = false;
        private SuperUser _supervisor = null;
        Dictionary<int, ForumIBL> forumsIBL = new Dictionary<int, ForumIBL>();

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass)
        {
            Forum newForum = new Forum(forumName, description);
            Member theAdmin = new Member(userName, adminName, email, pass, newForum);
            newForum.AddAdmin(theAdmin);

            ForumIBL newForumBL = new ForumBL(newForum);

            forumsIBL.Add(newForumBL.getForum().Id, newForumBL);

            return newForum;
        }

        public List<Forum> getAllForums()
        {
            return forumsIBL.Values.ToList().Select(forumBL => forumBL.getForum()).ToList();
        }

        public Forum getForum(Member member, int forumId)
        {
            ForumIBL ret;
            forumsIBL.TryGetValue(forumId, out ret);

            return ret.getForum();
        }

        public ForumIBL getForumIBL(Forum forum)
        {
            ForumIBL ret;
            forumsIBL.TryGetValue(forum.Id, out ret);
            return ret;
        }

        public SuperUser initialize(string name, string userName, string email, string pass)
        {
            if (!_initialized)
            {
                var superuser=new SuperUser(userName,name, email, pass);
                _supervisor = superuser;
                _initialized = true;
                return superuser;
            }
            return null;
        }
    }
}
