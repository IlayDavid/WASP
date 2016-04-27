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
        private bool _isInitialize = false;
        private SuperUser _su = null;
        public TCL()
        {
            forums = new Dictionary<int, Forum>();
            initForTesting();
        }

        private void initForTesting()
        {
            initialize("amitay", "a", 205857121, "amitay140@gmail.com", "a");

            createForum(_su.id, "forum1", "this is Forum number 1", 222, "aa", "eli", "eli@gmail.com", "1", null);
            createForum(_su.id, "Sport", "This forum is about sport games", 333, "bb", "moshe", "moshe@gmail.com", "1", null);
        }

        public User login(string userName, string password, int forumID)
        {
            Dictionary<int, User> members = forums[forumID].members;
            User user = members.Values.First(x => x.userName == userName);
            if (user.password.Equals(password))
                return user;
            else
                throw new Exception("ERROR: Password did not match");
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
            return forums.Values.ToList();
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
            return forums[forumID].subforums;
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
