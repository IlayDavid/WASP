using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace AccTests
{
    internal class ProxyBridge : WASPBridge
    {
        private RealBridge proj;

        public ProxyBridge(RealBridge bridge)
        {
            proj = bridge;
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return ((ServerAPI) proj).initialize(name, userName, ID, email, pass);
        }

        public int isInitialize()
        {
            return ((ServerAPI) proj).isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName,
            string email, string pass, Policy policy)
        {
            return ((ServerAPI) proj).createForum(userID, forumName, description, adminID, adminUserName, adminName, email, pass, policy);
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            return ((ServerAPI) proj).defineForumPolicy(userID, forumID);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return ((ServerAPI) proj).subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            return ((ServerAPI) proj).createThread(userID, forumID, title, content, subForumID);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            return ((ServerAPI) proj).createReplyPost(userID, forumID, content, replyToPost_ID);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            return ((ServerAPI) proj).createSubForum(userID, forumID, name, description, moderatorID, term);
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            return ((ServerAPI) proj).sendMessage(userID, forumID, targetUserNameID, message);
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            return ((ServerAPI) proj).addModerator(userID, forumID, moderatorID, subForumID, term);
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            return ((ServerAPI) proj).updateModeratorTerm(userID, forumID, moderatorID, subforumID, term);
        }

        public int confirmEmail(int userID, int forumID)
        {
            return ((ServerAPI) proj).confirmEmail(userID, forumID);
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            return ((ServerAPI) proj).deletePost(userID, forumID, postID);
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            return ((ServerAPI) proj).editPost(userID, forumID, postID, content);
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            return ((ServerAPI) proj).deleteModerator(userID, forumID, moderatorID, subForumID);
        }

        public List<Message> getAllNotificationses(int userID, int forumID)
        {
            return ((ServerAPI) proj).getAllNotificationses(userID, forumID);
        }

        public List<Message> getNewNotificationses(int userID, int forumID)
        {
            return ((ServerAPI) proj).getNewNotificationses(userID, forumID);
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return ((ServerAPI) proj).subForumTotalMessages(userID, forumID, subForumID);
        }

        public List<Post> postsByMember(int adminID, int forumID, int userID)
        {
            return ((ServerAPI) proj).postsByMember(adminID, forumID, userID);
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            return ((ServerAPI) proj).moderatorReport(userID, forumID);
        }

        public int totalForums(int userID)
        {
            return ((ServerAPI) proj).totalForums(userID);
        }

        public List<User> membersInDifferentForums(int userID)
        {
            return ((ServerAPI) proj).membersInDifferentForums(userID);
        }

        public User login(string userName, string password, int forumID)
        {
            return ((ServerAPI) proj).login(userName, password, forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            return ((ServerAPI) proj).loginSU(userName, password);
        }

        public Post getThread(int forumID, int threadId)
        {
            return ((ServerAPI) proj).getThread(forumID, threadId);
        }

        public List<Post> getThreads(int forumID, int subForumID, int @from, int amount)
        {
            return ((ServerAPI) proj).getThreads(forumID, subForumID, @from, amount);
        }

        public List<Post> getReplays(int forumID, int subForumID, int postID)
        {
            return ((ServerAPI) proj).getReplays(forumID, subForumID, postID);
        }

        public Forum getForum(int forumID)
        {
            return ((ServerAPI) proj).getForum(forumID);
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            return ((ServerAPI) proj).getSubforum(forumID, subforumId);
        }

        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            return ((ServerAPI) proj).getModerators(forumID, subForumID);
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            return ((ServerAPI) proj).getModeratorTermTime(userID, forumID, moderatorID, subforumID);
        }

        public List<Forum> getAllForums()
        {
            return ((ServerAPI) proj).getAllForums();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            return ((ServerAPI) proj).getAdmins(userID, forumID);
        }

        public List<User> getMembers(int userID, int forumID)
        {
            return ((ServerAPI) proj).getMembers(userID, forumID);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            return ((ServerAPI) proj).getSubforums(forumID);
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            return ((ServerAPI) proj).getAdmin(userID, forumID, AdminID);
        }
    }
}
