using System;
using WASP.Domain;

namespace WASP.Server
{
    class Server : ServerAPI
    {
        private bool _initialized=false;
        private IBL bl = new BL();

        public string addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
            return bl.addModerator(user_ID, moderator_ID, sf_ID, term);  
        }

        public string confirmEmail(int user_ID)
        {
            return bl.confirmEmail(user_ID);
        }
        
        public string createForum(int user_ID, Forum forum)
        {
            return bl.createForum(forum);
            return "";
        }

        public string createPost(int user_ID, int thread_ID, Post post)
        {
            return bl.createPost( user_ID,  thread_ID,  post);
            return "";
        }

        public string createSubForum(int user_ID, Subforum sf)
        {
            return bl.createSubForum( user_ID,  sf);
        }

        public string createThread(int user_ID, int sf_ID, Thread thread)
        {
            return bl.createThread( user_ID,  sf_ID,  thread);
        }

        public string defineForumPolicy(int user_ID, Forum forum)
        {
            return bl.defineForumPolicy( user_ID,  forum);
        }

        public string deletePost(int user_ID, int thread_ID, Post post)
        {
            return bl.deletePost( user_ID,  thread_ID,  post);
        }

        public string getForum(int user_ID, int forum_ID)
        {
            return bl.getForum( user_ID,  forum_ID);
        }

        public string getSubForum(int user_ID, int sf_ID)
        {
            return bl.getSubForum( user_ID,  sf_ID);
        }

        public string getThread(int user_ID, int thread_ID)
        {
            return bl.getThread( user_ID,  thread_ID);
        }

        public string initialize()
        {
            return bl.initialize();
            if (!_initialized)
            {
                const string SUPERUSERNAME = "admin";
                const string SUPERPASSWORD = "wasp1234Sting";
                var superUser = SuperUser.CreateSuperUser();
                superUser.Password = SUPERPASSWORD;
                superUser.Username = SUPERUSERNAME;
                _initialized = true;
                return "system initialized";
            }
            return "already initialized. action failed.";
        }

        public string login(string password, string username)
        {
            return login( password,  username);
        }

        public string sendMessage(int user_ID, Message message)
        {
            return sendMessage( user_ID,  message);
        }

        public string sendMessage(User user, Message message)
        {
            return sendMessage( user,  message);
        }

        public string subscribeToForum(User user, int forum_ID)
        {
            return subscribeToForum( user,  forum_ID);
        }

        public string subscribeToForum(User user, Forum forum)
        {
            return subscribeToForum( user,  forum);
        }

        public string updateForum(int user_ID, Forum forum)
        {
            return updateForum( user_ID,  forum);
        }

        public string updateForum(User user, Forum forum)
        {
            return updateForum( user,  forum);
        }

        public string updateModeratorTerm(int user_ID, User moderator, int sf_ID, DateTime term)
        {
            return updateModeratorTerm( user_ID,  moderator,  sf_ID,  term);
        }
    }
}
