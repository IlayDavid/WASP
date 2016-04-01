using System;
using WASP.Domain;

namespace WASP.Server
{
    class Server : ServerAPI
    {
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
        }

        public string createPost(int user_ID, int thread_ID, Post post)
        {
            return bl.createPost( user_ID,  thread_ID,  post);
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

        public string deletePost(int user_ID, int thread_ID, int post)
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
           
        }

        public string login(string password, string username)
        {
            return login( password,  username);
        }

        public string sendMessage(int user_ID, Message message)
        {
            return bl.sendMessage( user_ID,  message);
        }


        public string subscribeToForum(User user, int forum_ID)
        {
            return bl.subscribeToForum( user,  forum_ID);
        }

        public string updateForum(int user_ID, Forum forum)
        {
            return bl.updateForum( user_ID,  forum);
        }

        public string updateModeratorTerm(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
            return bl.updateModeratorTerm( user_ID,  moderator_ID,  sf_ID,  term);
        }
    }
}
