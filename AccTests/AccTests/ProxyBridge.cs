using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccTests
{
    class ProxyBridge : WASPBridge
    {
        public RealBridge proj;

        public void addModerator(int userId, int userId1, int sfId, DateTime term)
        {
        }

        public void confirmEmail(int userId)
        { 
        }

        public int createForum(int userId, Forum forum)
        {
            return -1;
        }

        public void createPost(int userId, int threadId, Post post)
        {      
        }

        public Subforum createSubForum(int userId, Subforum sf)
        {
            return null;
        }

        public void createThread(int userId, int sfId, UserThread thread)
        {
        }

        public void defineForumPolicy(int userId, Forum forum)
        {
        }

        public void deletePost(int userId1, int threadId, int postId)
        {
        }

        public Forum getForum(int userId, int forumId)
        {
            return null;
        }

        public Subforum getSubforum(int userId, int subforumId)
        {
            return null;
        }

        public UserThread getThread(int userId, int threadId)
        {
            return null;
        }

        public User initialize()
        {
            return null;
        }

        public int login(string userName, string password)
        {
            return -1;
        }

        public void sendMessage(int userId, Message message)
        {
        }

        public void subscribeToForum(int userId, int forumId)
        {
        }

        public void updateForum(int userId, int forumId)
        {
        }

        public void updateModeratorTerm(int userId, int userId1, int sfId, DateTime term)
        {
        }
    }
}
