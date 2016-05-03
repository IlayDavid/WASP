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

            Forum forum1 = createForum(_su.id, "forum1", "this is Forum number 1", 222, "aa", "eli", "eli@gmail.com", "1", null);
            User u11 = subscribeToForum(1234, "amitay140", "amitay", "amitay140@gmail.com", "1", forum1.ID);
            User u12 = subscribeToForum(1235, "moshe12", "moshe", "moshe@gmail.com", "1", forum1.ID);
            User u13 = subscribeToForum(1236, "kobi90", "kobi", "kobi@gmail.com", "1", forum1.ID);

            Forum forum2 = createForum(_su.id, "Sport", "This forum is about sport games", 333, "bb", "moshe", "moshe@gmail.com", "1", null);
            User u21 = subscribeToForum(1234, "amitay141", "amitay", "amitay141@gmail.com", "1", forum2.ID);
            User u22 = subscribeToForum(1235, "moshe13", "moshe", "moshe1@gmail.com", "1", forum2.ID);
            User u23 = subscribeToForum(1236, "kobi91", "kobi", "kobi1@gmail.com", "1", forum2.ID);

            Subforum sf11 = createSubForum(_su.id, forum1.ID, "Sub - forum 1", "this is SF 1", u11.id, DateTime.Now.AddDays(3));
            Subforum sf12 = createSubForum(_su.id, forum1.ID, "Sub - forum 2", "this is SF 2", u12.id, DateTime.Now.AddDays(3));
            Subforum sf13 = createSubForum(_su.id, forum1.ID, "Sub - forum 3", "this is SF 3", u13.id, DateTime.Now.AddDays(3));
            
            Subforum sf21 = createSubForum(_su.id, forum2.ID, "Sub - forum 1", "this is SF 1", u21.id, DateTime.Now.AddDays(3));
            Subforum sf22 = createSubForum(_su.id, forum2.ID, "Sub - forum 2", "this is SF 2", u21.id, DateTime.Now.AddDays(3));

            Post p111 = createThread(u11.id, forum1.ID, "Thread number 1", "this is the opening thread test 1", sf11.Id);
            Post p112 = createThread(u11.id, forum1.ID, "Thread number 2", "this is the opening thread test 2", sf11.Id);
            Post p113 = createThread(u11.id, forum1.ID, "Thread number 3", "this is the opening thread test 3", sf11.Id);

            Post p12 = createThread(u12.id, forum1.ID, "Thread number 2", "this is the opening thread test 2", sf12.Id);
            Post p13 = createThread(u13.id, forum1.ID, "Thread number 3", "this is the opening thread test 3", sf13.Id);

            Post p21 = createThread(u21.id, forum2.ID, "Thread number 1", "this is the opening thread test 1", sf21.Id);
            Post p22 = createThread(u21.id, forum2.ID, "Thread number 2", "this is the opening thread test 2", sf22.Id);

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
        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            List<Post> ts = forums[forumID].subforums[subForumID]._threads;
            int min = amount;
            if (ts.Count()-from < min)
                min = ts.Count();
            return ts.GetRange(from, min);
        }

        public Post getThread(int forumID, int threadId)
        {
            throw new NotImplementedException();
        }

        public List<Post> getReplays(int forumID, int subForumID, int postID)
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

        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            return forums[forumID].subforums[subForumID]._moderators.Values.ToList();
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
            return forums[forumID].subforums.Values.ToList();
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            throw new NotImplementedException();
        }
    }
}
