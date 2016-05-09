using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;
using WASP.DataClasses.Reports;
using Policy = WASP.DataClasses.Policy;

namespace AccTests
{
    internal class ProxyBridge : WASPBridge
    {
        private RealBridge proj;

        public ProxyBridge(RealBridge bridge)
        {
            proj = bridge;
        }

        public void Clean()
        {
            ((WASP.Domain.IBL) proj).Clean();
        }

        public void Restore()
        {
            ((WASP.Domain.IBL) proj).Restore();
        }

        public void Backup()
        {
            ((WASP.Domain.IBL) proj).Backup();
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return ((WASP.Domain.IBL) proj).initialize(name, userName, ID, email, pass);
        }

        public int isInitialize()
        {
            return ((WASP.Domain.IBL) proj).isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName,
            string email, string pass, Policy policy)
        {
            return ((WASP.Domain.IBL) proj).createForum(userID, forumName, description, adminID, adminUserName, adminName, email, pass, policy);
        }

        public int defineForumPolicy(int userID, int forumID, Policy policy)
        {
            return ((WASP.Domain.IBL) proj).defineForumPolicy(userID, forumID, policy);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return ((WASP.Domain.IBL) proj).subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            return ((WASP.Domain.IBL) proj).createThread(userID, forumID, title, content, subForumID);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            return ((WASP.Domain.IBL) proj).createReplyPost(userID, forumID, content, replyToPost_ID);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            return ((WASP.Domain.IBL) proj).createSubForum(userID, forumID, name, description, moderatorID, term);
        }

        public int sendMessage(int userID, int forumID, int targetUserNameID, string message)
        {
            return ((WASP.Domain.IBL) proj).sendMessage(userID, forumID, targetUserNameID, message);
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            return ((WASP.Domain.IBL) proj).addModerator(userID, forumID, moderatorID, subForumID, term);
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            return ((WASP.Domain.IBL) proj).updateModeratorTerm(userID, forumID, moderatorID, subforumID, term);
        }

        public int confirmEmail(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).confirmEmail(userID, forumID);
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            return ((WASP.Domain.IBL) proj).deletePost(userID, forumID, postID);
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            return ((WASP.Domain.IBL) proj).editPost(userID, forumID, postID, content);
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            return ((WASP.Domain.IBL) proj).deleteModerator(userID, forumID, moderatorID, subForumID);
        }

        public Admin addAdmin(int userID, int forumID, int adminId)
        {
            return ((WASP.Domain.IBL) proj).addAdmin(userID, forumID, adminId);
        }

        public Notification[] getAllNotificationses(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).getAllNotificationses(userID, forumID);
        }

        public Notification[] getNewNotificationses(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).getNewNotificationses(userID, forumID);
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return ((WASP.Domain.IBL) proj).subForumTotalMessages(userID, forumID, subForumID);
        }

        public Post[] postsByMember(int adminID, int forumID, int userID)
        {
            return ((WASP.Domain.IBL) proj).postsByMember(adminID, forumID, userID);
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).moderatorReport(userID, forumID);
        }

        public int totalForums(int userID)
        {
            return ((WASP.Domain.IBL) proj).totalForums(userID);
        }

        public User[] membersInDifferentForums(int userID)
        {
            return ((WASP.Domain.IBL) proj).membersInDifferentForums(userID);
        }

        public User login(string userName, string password, int forumID)
        {
            return ((WASP.Domain.IBL) proj).login(userName, password, forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            return ((WASP.Domain.IBL) proj).loginSU(userName, password);
        }

        public Post getThread(int forumID, int threadId)
        {
            return ((WASP.Domain.IBL) proj).getThread(forumID, threadId);
        }

        public Post[] getThreads(int subForumID)
        {
            return ((WASP.Domain.IBL) proj).getThreads(subForumID);
        }

        public Post[] getReplys(int forumID, int subForumID, int postID)
        {
            return ((WASP.Domain.IBL) proj).getReplys(forumID, subForumID, postID);
        }

        public Forum getForum(int forumID)
        {
            return ((WASP.Domain.IBL) proj).getForum(forumID);
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            return ((WASP.Domain.IBL) proj).getSubforum(forumID, subforumId);
        }

        public Moderator[] getModerators(int forumID, int subForumID)
        {
            return ((WASP.Domain.IBL) proj).getModerators(forumID, subForumID);
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            return ((WASP.Domain.IBL) proj).getModeratorTermTime(userID, forumID, moderatorID, subforumID);
        }

        public Forum[] getAllForums()
        {
            return ((WASP.Domain.IBL) proj).getAllForums();
        }

        public Admin[] getAdmins(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).getAdmins(userID, forumID);
        }

        public User[] getMembers(int userID, int forumID)
        {
            return ((WASP.Domain.IBL) proj).getMembers(userID, forumID);
        }

        public Subforum[] getSubforums(int forumID)
        {
            return ((WASP.Domain.IBL) proj).getSubforums(forumID);
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            return ((WASP.Domain.IBL) proj).getAdmin(userID, forumID, AdminID);
        }
    }
}
