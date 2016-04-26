using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        private bool _isInitialize = false;
        private SuperUser _su = null;
        //---------------------------Version 1 Use Cases Start------------------------------------
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            _su = new SuperUser(name, userName, ID, email, pass);
            _isInitialize = true;
            return _su;
        }
        public int isInitialize()
        {
            return _isInitialize ? 1 : 0;
        }

        public Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            if (userID != _su.id)
                throw new Exception("User with id - "+userID+" cannot add forum");
            User user = new User() { userName = adminUserName, name = adminName, email = email, password = pass };
            List<User> admins = new List<User>();
            admins.Add(user);
            Forum forum = new Forum() { Name = forumName, Description = description, admins = admins };
            forums.Add(forum.ID, forum);
            return forum;
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(string userName, string name, string email, string pass, int targetForumID)
        {
            throw new NotImplementedException();
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            throw new NotImplementedException();
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            throw new NotImplementedException();
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}
