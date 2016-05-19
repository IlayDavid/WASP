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
            _cl = new CL();
        }
        public void setForumID(int forumID)
        {
            _cl.setForumID(forumID);
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
        public List<Post> getThreads(int subForumID)
        {
            if (subForumID < 0) throw new Exception("ERROR: id is illegal");
            return _cl.getThreads(subForumID);
        }
        public List<Post> getReplys(int postID)
        {
            if (postID < 0) throw new Exception("ERROR: id is illegal");
            return _cl.getReplys(postID);
        }

        public Post getThread(int threadId)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int forumID)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int subforumId)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int subForumID)
        {
            if (subForumID < 0 ) throw new Exception("ERROR: id is illegal");
            return _cl.getModerators(subForumID);
        }

        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            throw new NotImplementedException();
        }

        public List<Forum> getAllForums()
        {
             List < Forum > f= _cl.getAllForums();
            return f;
        }

        public List<Admin> getAdmins(int forumID)
        {
            if (forumID >= 0)
                return _cl.getAdmins(forumID);
            else
                throw new Exception("ERROR: id is negative.");
        }

        public List<User> getMembers(int forumID)
        {
            if (forumID < 0) throw new Exception("ERROR: id is illegal");
            return _cl.getMembers(forumID);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            if (forumID >= 0)
                return _cl.getSubforums(forumID);
            else
                throw new Exception("ERROR: illegal id");
        }

        public Admin getAdmin(int adminID)
        {
            throw new NotImplementedException();
        }
    }
}
