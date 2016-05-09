using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP.DataClasses.Reports;
using WASP.Domain;
using WASP.Server;
using Policy = WASP.DataClasses.Policy;

namespace AccTests
{
    class RealBridge : WASPBridge
    {
        private IBL _serverAPI;

        public RealBridge()
        {
            _serverAPI = new BLFacade();
        }

        public void Clean()
        {
            _serverAPI.Clean();
        }

        public void Restore()
        {
            _serverAPI.Restore();
        }

        public void Backup()
        {
            _serverAPI.Backup();
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return _serverAPI.initialize(name, userName, ID, email, pass);
        }

        public int isInitialize()
        {
            return _serverAPI.isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName,
            string email, string pass, Policy policy)
        {
            return _serverAPI.createForum(userID, forumName, description, adminID, adminUserName, adminName, email, pass, policy);
        }

        public int defineForumPolicy(int userID, int forumID, Policy policy)
        {
            return _serverAPI.defineForumPolicy(userID, forumID, policy);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return _serverAPI.subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            return _serverAPI.createThread(userID, forumID, title, content, subForumID);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            return _serverAPI.createReplyPost(userID, forumID, content, replyToPost_ID);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            return _serverAPI.createSubForum(userID, forumID, name, description, moderatorID, term);
        }

        public int sendMessage(int userID, int forumID, int targetUserNameID, string message)
        {
            return _serverAPI.sendMessage(userID, forumID, targetUserNameID, message);
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            return _serverAPI.addModerator(userID, forumID, moderatorID, subForumID, term);
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            return _serverAPI.updateModeratorTerm(userID, forumID, moderatorID, subforumID, term);
        }

        public int confirmEmail(int userID, int forumID)
        {
            return _serverAPI.confirmEmail(userID, forumID);
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            return _serverAPI.deletePost(userID, forumID, postID);
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            return _serverAPI.editPost(userID, forumID, postID, content);
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            return _serverAPI.deleteModerator(userID, forumID, moderatorID, subForumID);
        }

        public Admin addAdmin(int userID, int forumID, int adminId)
        {
            return _serverAPI.addAdmin(userID, forumID, adminId);
        }

        public Notification[] getAllNotificationses(int userID, int forumID)
        {
            return _serverAPI.getAllNotificationses(userID, forumID);
        }

        public Notification[] getNewNotificationses(int userID, int forumID)
        {
            return _serverAPI.getNewNotificationses(userID, forumID);
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return _serverAPI.subForumTotalMessages(userID, forumID, subForumID);
        }

        public Post[] postsByMember(int adminID, int forumID, int userID)
        {
            return _serverAPI.postsByMember(adminID, forumID, userID);
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            return _serverAPI.moderatorReport(userID, forumID);
        }

        public int totalForums(int userID)
        {
            return _serverAPI.totalForums(userID);
        }

        public User[] membersInDifferentForums(int userID)
        {
            return _serverAPI.membersInDifferentForums(userID);
        }

        public User login(string userName, string password, int forumID)
        {
            return _serverAPI.login(userName, password, forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            return _serverAPI.loginSU(userName, password);
        }

        public Post getThread(int forumID, int threadId)
        {
            return _serverAPI.getThread(forumID, threadId);
        }

        public Post[] getThreads(int subForumID)
        {
            return _serverAPI.getThreads(subForumID);
        }

        public Post[] getReplys(int forumID, int subForumID, int postID)
        {
            return _serverAPI.getReplys(forumID, subForumID, postID);
        }

        public Forum getForum(int forumID)
        {
            return _serverAPI.getForum(forumID);
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            return _serverAPI.getSubforum(forumID, subforumId);
        }

        public Moderator[] getModerators(int forumID, int subForumID)
        {
            return _serverAPI.getModerators(forumID, subForumID);
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            return _serverAPI.getModeratorTermTime(userID, forumID, moderatorID, subforumID);
        }

        public Forum[] getAllForums()
        {
            return _serverAPI.getAllForums();
        }

        public Admin[] getAdmins(int userID, int forumID)
        {
            return _serverAPI.getAdmins(userID, forumID);
        }

        public User[] getMembers(int userID, int forumID)
        {
            return _serverAPI.getMembers(userID, forumID);
        }

        public Subforum[] getSubforums(int forumID)
        {
            return _serverAPI.getSubforums(forumID);
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            return _serverAPI.getAdmin(userID, forumID, AdminID);
        }
    }
}
