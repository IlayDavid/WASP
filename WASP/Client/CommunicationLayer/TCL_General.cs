using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        private bool _isInitialize = false;
        private SuperUser _su = null;

        private int forumID = -1, userID = -1;
        Dictionary<int, Forum> forums;
        Dictionary<int, Subforum> subforums;
        Dictionary<int, Post> posts;
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

            Policy policy1 = new Policy(Policy.all, 10, false, 0, 100);
            Policy policy2 = new Policy(Policy.all, 10, false, 0, 100);
            Policy policy3 = new Policy(Policy.all, 10, false, 0, 100);

            loginSU(_su.userName, _su.password);
            Forum forum1 = createForum("forum1", "this is Forum number 1", 222, "aa", "eli", "eli@gmail.com", "1", policy1);
            User u11 = subscribeToForum(1234, "amitay140", "amitay", "amitay140@gmail.com", "1", forum1.id);
            User u12 = subscribeToForum(1235, "moshe12", "moshe", "moshe@gmail.com", "1", forum1.id);
            User u13 = subscribeToForum(1236, "kobi90", "kobi", "kobi@gmail.com", "1", forum1.id);

            Forum forum2 = createForum("Sport", "This forum is about sport games", 333, "bb", "moshe", "moshe@gmail.com", "1", policy2);
            User u21 = subscribeToForum(1234, "amitay141", "amitay", "amitay141@gmail.com", "1", forum2.id);
            User u22 = subscribeToForum(1235, "moshe13", "moshe", "moshe1@gmail.com", "1", forum2.id);
            User u23 = subscribeToForum(1236, "kobi91", "kobi", "kobi1@gmail.com", "1", forum2.id);

            login(_su.userName, _su.password, forum1.id);
            Subforum sf11 = createSubForum("Sub - forum 1", "this is SF 1", u11.id, DateTime.Now.AddDays(3));
            Subforum sf12 = createSubForum("Sub - forum 2", "this is SF 2", u12.id, DateTime.Now.AddDays(3));
            Subforum sf13 = createSubForum("Sub - forum 3", "this is SF 3", u13.id, DateTime.Now.AddDays(3));

            login(_su.userName, _su.password, forum2.id);
            Subforum sf21 = createSubForum("Sub - forum 1", "this is SF 1", u21.id, DateTime.Now.AddDays(3));
            Subforum sf22 = createSubForum("Sub - forum 2", "this is SF 2", u21.id, DateTime.Now.AddDays(3));

            login(u11.userName, u11.password, forum1.id);
            Post p111 = createThread("Thread number 1", "this is the opening thread test 1", sf11.id);
            Post p112 = createThread("Thread number 2", "this is the opening thread test 2", sf11.id);
            Post p113 = createThread("Thread number 3", "this is the opening thread test 3", sf11.id);

            login(u12.userName, u12.password, forum1.id);
            Post p12 = createThread("Thread number 2", "this is the opening thread test 2", sf12.id);

            login(u13.userName, u13.password, forum1.id);
            Post p13 = createThread("Thread number 3", "this is the opening thread test 3", sf13.id);

            login(u21.userName, u21.password, forum2.id);
            Post p21 = createThread("Thread number 1", "this is the opening thread test 1", sf21.id);
            Post p22 = createThread("Thread number 2", "this is the opening thread test 2", sf22.id);
        }

        public User login(string userName, string password, int forumID)
        {
            Dictionary<int, User> members = forums[forumID].members;
            User user = members.Values.First(x => x.userName == userName);
            if (user.password.Equals(password))
            {
                userID = user.id;
                this.forumID = forumID;
                return user;
            }
            else
                throw new Exception("ERROR: Password did not match");
        }

        public SuperUser loginSU(string userName, string password)
        {
            if (_isInitialize)
            {
                if (_su.userName.Equals(userName) && _su.password.Equals(password))
                {
                    userID = _su.id;
                    return _su;
                }
                else
                    throw new Exception("ERROR: user name or password did not match!");
            }
            else
                throw new Exception("ERROR: system is not initialized");
        }

        //---------------------------------Getters----------------------------------------------
        public List<Post> getThreads(int subForumID)
        {
            List<Post> ts = subforums[subForumID].threads;
            return ts;
        }

        public Post getThread(int threadId)
        {
            return posts[threadId];
        }

        public List<Post> getReplys(int postID)
        {
            return posts[postID].replies;
        }

        public Forum getForum(int forumID)
        {
            return forums[forumID];
        }
        public Subforum getSubforum(int subforumId)
        {
            return subforums[subforumId];
        }
        public List<Moderator> getModerators(int subForumID)
        {
            return subforums[subForumID].moderators.Values.ToList();
        }
        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            throw new NotImplementedException();
        }
        public List<Forum> getAllForums()
        {
            return forums.Values.ToList();
        }
        public List<Admin> getAdmins(int forumID)
        {
            return forums[forumID].admins.Values.ToList();
        }
        public List<User> getMembers(int forumID)
        {
            return forums[forumID].members.Values.ToList();
        }
        public List<Subforum> getSubforums(int forumID)
        {
            return forums[forumID].subforums.Values.ToList();
        }
        public Admin getAdmin(int AdminID)
        {
            //return forums[forumID].admins[AdminID];
            throw new NotImplementedException();
        }
    }
}
