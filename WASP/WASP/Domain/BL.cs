using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataAccess;
using WASP.DataClasses;
namespace WASP.Domain
{
    public class BL : IBL
    {
        private bool _initialized = false;
        private SuperUser _supervisor = null;
        Dictionary<int, ForumIBL> forumsIBL = new Dictionary<int, ForumIBL>();
        private IDAL _dal = null;

        public BL(IDAL dal)
        {
            _dal = dal;
        }

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass)
        {
            //create new forum with admin
            Forum newForum = new Forum(forumName, description, null);
            Member theAdmin = new Member(userName, adminName, email, pass, newForum);
            newForum.AddAdmin(theAdmin);
            //create the business logic for the new forum.
            ForumIBL newForumBL = new ForumBL(newForum, null);
            //add to dictionary
            forumsIBL.Add(newForumBL.getForum().Id, newForumBL);
            _dal.AddForum(newForum);

            return newForum;
        }

        public List<Forum> getAllForums()
        {
            return forumsIBL.Values.Select(forumBL => forumBL.getForum()).ToList();
        }

        public Forum getForum(Member member, int forumId)
        {
            return _dal.GetForum(forumId);
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
                _supervisor = new SuperUser(SUPERUSERNAME, "", "", SUPERPASSWORD);
                _initialized = true;
                return _supervisor;
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
