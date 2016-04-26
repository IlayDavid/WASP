using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        Dictionary<int, Forum> forums;

        public TCL()
        {
            forums = new Dictionary<int, Forum>();
        }
        public User login(string userName, string password, int forumID)
        {
            try
            {
                Dictionary<string, User> members = forums[forumID].members;

            }
            catch (Exception) { }
            return null;
        }

        public SuperUser loginSU(string userName, string password)
        {
            if (_isInitialize)
            {
                if (_su.userName.Equals(userName) && _su.password.Equals(password))
                    return _su;
                else
                    throw new Exception("ERROR: user name or password did not match!");
            }
            else
                throw new Exception("ERROR: system is not initialized");
        }

        //---------------------------------Getters----------------------------------------------

        public Post getThread(int userID, int forumID, int threadId)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int forumID)
        {
            return forums[forumID];
        }

        public Subforum getSubforum(int userID, int forumID, int subforumId)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int userID, int forumID, int subForumID)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            throw new NotImplementedException();
        }

        public List<Forum> getAllForums()
        {
            List<Forum> ret = new List<Forum>();
            ret.Add(new Forum() {ID=1, Name = "Sport", Description = "This forum is about sport games" });
            return ret;
            throw new NotImplementedException();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
