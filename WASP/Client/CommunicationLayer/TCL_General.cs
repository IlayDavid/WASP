using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        Dictionary<int, Forum> forums;
        Dictionary<int, Subforum> subforums;
        Dictionary<int, Post> posts;
        private bool _isInitialize = false;
        private SuperUser _su = null;
        public TCL()
        {
            forums = new Dictionary<int, Forum>();
            subforums = new Dictionary<int, Subforum>();
            posts = new Dictionary<int, Post>();
            initForTesting();
        }

        private void initForTesting()
        {
            initialize("amitay", "a", 205857121, "amitay140@gmail.com", "a");

            Forum forum1 = createForum(_su.id, "forum1", "this is Forum number 1", 222, "aa", "eli", "eli@gmail.com", "1", null);
            User u11 = subscribeToForum(1234, "amitay140", "amitay", "amitay140@gmail.com", "1", forum1.id);
            User u12 = subscribeToForum(1235, "moshe12", "moshe", "moshe@gmail.com", "1", forum1.id);
            User u13 = subscribeToForum(1236, "kobi90", "kobi", "kobi@gmail.com", "1", forum1.id);

            Forum forum2 = createForum(_su.id, "Sport", "This forum is about sport games", 333, "bb", "moshe", "moshe@gmail.com", "1", null);
            User u21 = subscribeToForum(1234, "amitay141", "amitay", "amitay141@gmail.com", "1", forum2.id);
            User u22 = subscribeToForum(1235, "moshe13", "moshe", "moshe1@gmail.com", "1", forum2.id);
            User u23 = subscribeToForum(1236, "kobi91", "kobi", "kobi1@gmail.com", "1", forum2.id);

            Subforum sf11 = createSubForum(_su.id, forum1.id, "Sub - forum 1", "this is SF 1", u11.id, DateTime.Now.AddDays(3));
            Subforum sf12 = createSubForum(_su.id, forum1.id, "Sub - forum 2", "this is SF 2", u12.id, DateTime.Now.AddDays(3));
            Subforum sf13 = createSubForum(_su.id, forum1.id, "Sub - forum 3", "this is SF 3", u13.id, DateTime.Now.AddDays(3));
            
            Subforum sf21 = createSubForum(_su.id, forum2.id, "Sub - forum 1", "this is SF 1", u21.id, DateTime.Now.AddDays(3));
            Subforum sf22 = createSubForum(_su.id, forum2.id, "Sub - forum 2", "this is SF 2", u21.id, DateTime.Now.AddDays(3));

            Post p111 = createThread(u11.id, forum1.id, "Thread number 1", "this is the opening thread test 1", sf11.id);
            Post p112 = createThread(u11.id, forum1.id, "Thread number 2", "this is the opening thread test 2", sf11.id);
            Post p113 = createThread(u11.id, forum1.id, "Thread number 3", "this is the opening thread test 3", sf11.id);

            Post p12 = createThread(u12.id, forum1.id, "Thread number 2", "this is the opening thread test 2", sf12.id);
            Post p13 = createThread(u13.id, forum1.id, "Thread number 3", "this is the opening thread test 3", sf13.id);

            Post p21 = createThread(u21.id, forum2.id, "Thread number 1", "this is the opening thread test 1", sf21.id);
            Post p22 = createThread(u21.id, forum2.id, "Thread number 2", "this is the opening thread test 2", sf22.id);
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
            List<Post> ts = forums[forumID].subforums[subForumID].threads;
            int min = amount;
            if (ts.Count()-from < min)
                min = ts.Count();
            return ts.GetRange(from, min);
        }

        public Post getThread(int forumID, int threadId)
        {
            return posts[threadId];
        }

        public List<Post> getReplys(int forumID, int subForumID, int postID)
        {
            return posts[postID].replies;
        }

        public Forum getForum(int forumID)
        {
            return forums[forumID];
        }
        public Subforum getSubforum(int forumID, int subforumId)
        {
            return subforums[subforumId];
        }
        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            return subforums[subForumID].moderators.Values.ToList();
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
            //return forums[forumID].admins.Values.ToList();
        }
        public List<User> getMembers(int userID, int forumID)
        {
            return forums[forumID].members.Values.ToList();
        }
        public List<Subforum> getSubforums(int forumID)
        {
            return forums[forumID].subforums.Values.ToList();
        }
        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            //return forums[forumID].admins[AdminID];
            throw new NotImplementedException();
        }
    }
}
