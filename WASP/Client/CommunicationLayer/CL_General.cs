using Client.DataClasses;
using System;
using System.Collections.Generic;

//using System.Net;
//using System.Text;  // for class Encoding

namespace Client.CommunicationLayer
{
    public partial class CL : ICL
    {
        public CL()
        {
            
        }
        public User login(string userName, string password, int forumID)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            throw new NotImplementedException();
        }

        //---------------------------------Getters----------------------------------------------
        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            throw new NotImplementedException();
        }
        public Post getThread(int forumID, int threadId)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int forumID)
        {
            throw new NotImplementedException();
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

        public List<Subforum> getSubforums(int forumID)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
