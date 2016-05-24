using Client.CommunicationLayer;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            if (_cl is TCL)
                initForTesting();
        }
        private void initForTesting()
        {
            string su_userName = "a";
            string su_password = "a";
            string userPassword = "1";

            initialize("amitay", su_userName, 205857121, "amitay140@gmail.com", su_password);

            Policy policy1 = new Policy(Policy.all, 10, false, 0, 100);
            Policy policy2 = new Policy(Policy.all, 10, false, 0, 100);
            Policy policy3 = new Policy(Policy.all, 10, false, 0, 100);

            loginSU(su_userName, su_password);
            Forum forum1 = createForum("forum1", "this is Forum number 1", 222, "aa", "eli", "eli@gmail.com", "1", policy1);
            User u11 = subscribeToForum(1234, "amitay140", "amitay", "amitay140@gmail.com", userPassword, forum1.id);
            User u12 = subscribeToForum(1235, "moshe12", "moshe", "moshe@gmail.com", userPassword, forum1.id);
            User u13 = subscribeToForum(1236, "kobi90", "kobi", "kobi@gmail.com", userPassword, forum1.id);

            Forum forum2 = createForum("Sport", "This forum is about sport games", 333, "bb", "moshe", "moshe@gmail.com", "1", policy2);
            User u21 = subscribeToForum(1234, "amitay141", "amitay", "amitay141@gmail.com", userPassword, forum2.id);
            User u22 = subscribeToForum(1235, "moshe13", "moshe", "moshe1@gmail.com", userPassword, forum2.id);
            User u23 = subscribeToForum(1236, "kobi91", "kobi", "kobi1@gmail.com", userPassword, forum2.id);

            login(su_userName, su_password, forum1.id);
            Subforum sf11 = createSubForum("Sub - forum 1", "this is SF 1", u11.id, DateTime.Now.AddDays(3));
            Subforum sf12 = createSubForum("Sub - forum 2", "this is SF 2", u12.id, DateTime.Now.AddDays(3));
            Subforum sf13 = createSubForum("Sub - forum 3", "this is SF 3", u13.id, DateTime.Now.AddDays(3));

            login(su_userName, su_password, forum2.id);
            Subforum sf21 = createSubForum("Sub - forum 1", "this is SF 1", u21.id, DateTime.Now.AddDays(3));
            Subforum sf22 = createSubForum("Sub - forum 2", "this is SF 2", u21.id, DateTime.Now.AddDays(3));

            login(u11.userName, userPassword, forum1.id);
            Post p111 = createThread("Thread number 1", "this is the opening thread test 1", sf11.id);
            Post p112 = createThread("Thread number 2", "this is the opening thread test 2", sf11.id);
            Post p113 = createThread("Thread number 3", "this is the opening thread test 3", sf11.id);

            login(u12.userName, userPassword, forum1.id);
            Post p12 = createThread("Thread number 2", "this is the opening thread test 2", sf12.id);

            login(u13.userName, userPassword, forum1.id);
            Post p13 = createThread("Thread number 3", "this is the opening thread test 3", sf13.id);

            login(u21.userName, userPassword, forum2.id);
            Post p21 = createThread("Thread number 1", "this is the opening thread test 1", sf21.id);
            Post p22 = createThread("Thread number 2", "this is the opening thread test 2", sf22.id);
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
            return _cl.login(userName, sha256_hash(password), forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            if (IsStrValid(userName) && IsPasswordValid(password))
                return _cl.loginSU(userName, sha256_hash(password));
            else
                throw new Exception("ERROR: user name or password are illegal");
        }
        private static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
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
            if (threadId >= 0)
                return _cl.getThread(threadId);
            else
                throw new Exception("ERROR: illegal id");
        }

        public Forum getForum(int forumID)
        {
            if (forumID >= 0)
                return _cl.getForum(forumID);
            else
                throw new Exception("ERROR: illegal id");
        }

        public Subforum getSubforum(int subforumId)
        {
            if (subforumId >= 0)
                return _cl.getSubforum(subforumId);
            else
                throw new Exception("ERROR: illegal id");
        }

        public List<Moderator> getModerators(int subForumID)
        {
            if (subForumID < 0) throw new Exception("ERROR: id is illegal");
            return _cl.getModerators(subForumID);
        }

        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            if (subforumID >= 0 && moderatorID >= 0)
                return _cl.getModeratorTermTime(moderatorID, subforumID);
            else
                throw new Exception("ERROR: illegal id");
        }

        public List<Forum> getAllForums()
        {
            List<Forum> f = _cl.getAllForums();
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

        public Admin getAdmin(int adminID, int forumID)
        {
            if (adminID >= 0)
                return _cl.getAdmin(adminID, forumID);
            else
                throw new Exception("ERROR: illegal id");
        }

        public Admin getAdmin(int adminID)
        {
            throw new NotImplementedException();
        }
    }
}
