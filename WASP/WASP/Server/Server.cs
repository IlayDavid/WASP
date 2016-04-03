using System;
using System.Collections.Generic;
using WASP.Domain;

namespace WASP.Server
{
    class Server : ServerAPI
    {
        private IBL bl = new BL();


        public int addModerator(string userId, string userId1, int sfId, DateTime term)
        {
            return bl.addModerator(userId, userId1, sfId, term);
        }

        public int deletePost(string userName, int threadId, int postId)
        {
            return bl.deletePost(userName, threadId, postId);
        }

        public int login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<User> getAdmins(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            throw new NotImplementedException();
        }
        public void confirmEmail(int userId)
        {
            bl.confirmEmail(userId);
        }


        public int updateForum(int userId, int forumId)
        {
            return bl.updateForum(userId, forumId);
        }

        public int defineForumPolicy(int userId, Forum forum)
        {
            return bl.defineForumPolicy(userId, forum);
        }



        public Thread getThread(int userId, int threadId)
        {
            return bl.getThread(userId, threadId);
        }

        public Forum getForum(int userId, int forumId)
        {
            return bl.getForum(userId, forumId);
        }

        public Subforum getSubforum(int userId, int subforumId)
        {
            return bl.getSubforum(userId, subforumId);
        }

        public int createThread(string userName, int subforumId, Thread thread)
        {
            return bl.createThread(userName, subforumId, thread);
        }

        public int createForum(string userName, Forum forum)
        {
            return bl.createForum(userName, forum);
        }

        public List<User> getModerators(int subforumId)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(string userName, int subforumId)
        {
            throw new NotImplementedException();
        }

        public int createSubForum(string userName, int forumId, Subforum sf)
        {
            return bl.createSubForum(userName, forumId, sf);
        }

        public List<Forum> getAllForums()
        {
            throw new NotImplementedException();
        }

        public int createPost(string userName, int threadId, Post post)
        {
            return bl.createPost(userName, threadId, post);
        }

        public int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term)
        {
            return bl.updateModeratorTerm(userName1, userName2, sfId, term);
        }

        public User initialize()
        {
            return bl.initialize();
        }

        public string subscribeToForum(User user, int forum_ID)
        {
            return bl.subscribeToForum( user,  forum_ID);
        }

        public int sendMessage(string userSend, string userAcc, Message message)
        {
            return bl.sendMessage(userSend, userAcc, message);
        }

    }
}
