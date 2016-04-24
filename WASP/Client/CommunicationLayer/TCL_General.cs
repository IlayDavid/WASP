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
        Dictionary<int, User> users; 
        Dictionary<int, Forum> forums;

        public TCL()
        {
            users = new Dictionary<int, User>();
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
            throw new NotImplementedException();
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
