using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataAccess;

namespace WASP.Domain
{
    public class BL : IBL
    {
        private bool _initialized = false;
        private SuperUser supervisor = null;
        Dictionary<int, ForumIBL> forumsIBL = new Dictionary<int, ForumIBL>();
        private IDAL _dal;
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
            List<Forum> retForums = new List<Forum>();
            foreach(ForumIBL forumBL in forumsIBL.Values.ToList())
            {
                retForums.Add(forumBL.getForum());
            }
            return retForums;
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
                const string SUPERUSERNAME = "admin";
                const string SUPERPASSWORD = "wasp1234Sting";
                supervisor = new SuperUser(SUPERUSERNAME, "", "", SUPERPASSWORD);
                _initialized = true;
                return supervisor;
            }
            return null;
        }

        public SuperUser login(string username, string password)
        {
            if (_supervisor.UserName.Equals(username) && _supervisor.Password.Equals(password))
            {
                return _supervisor;
            }
            return null;
        }
    }
}
