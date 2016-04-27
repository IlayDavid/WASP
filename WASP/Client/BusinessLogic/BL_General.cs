using Client.CommunicationLayer;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public partial class BL : IBL
    {
        private ICL _cl;
        public BL()
        {
            _cl = new TCL();
        }
        public User login(string userName, string password, int forumID)
        {
            if (!IsStrValid(userName)) throw new Exception("ERROR: username is empty or iilegal");
            if (!IsStrValid(password)) throw new Exception("ERROR: password is empty or iilegal");
            if (forumID < 0) throw new Exception("ERROR: forum id is iilegal");
            return _cl.login(userName, password, forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            if (IsStrValid(userName) && IsPasswordValid(password))
                return _cl.loginSU(userName, password);
            else
                throw new Exception("ERROR: user name or password are illegal");
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
            return _cl.getForum(forumID);
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
            return _cl.getAllForums();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            if (userID >= 0 && forumID >= 0)
                return _cl.getAdmins(userID, forumID);
            else
                throw new Exception("ERROR: id is negative.");
        }

        public List<User> getMembers(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(int forumID)
        {
            if (forumID >= 0)
                return _cl.getSubforums(forumID);
            else
                throw new Exception("ERROR: illegal id");
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
