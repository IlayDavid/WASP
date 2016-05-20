using System;
using System.Collections.Generic;
using Client.BusinessLogic;
using Client.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccTests
{
    public class ClientRealBridge : WASPClientBridge
    {
        private IBL _clientAPI;

        public ClientRealBridge()
        {
            _clientAPI = new BL();
        }

        public Client.DataClasses.Admin addAdmin(int newAdminID)
        {
            return _clientAPI.addAdmin(newAdminID);
        }

        public Client.DataClasses.Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {
            return _clientAPI.addModerator(moderatorID, subForumID, term);
        }

        public int confirmEmail(int code)
        {
            return _clientAPI.confirmEmail(code);
        }

        public Forum createForum(string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            return _clientAPI.createForum(forumName, description, adminID, adminUserName, adminName, email, pass, policy);
        }

        public Post createReplyPost(string content, int replyToPost_ID)
        {
            return _clientAPI.createReplyPost(content, replyToPost_ID);
        }

        public Subforum createSubForum(string name, string description, int moderatorID, DateTime term)
        {
            return _clientAPI.createSubForum(name, description, moderatorID, term);
        }

        public Post createThread(string title, string content, int subForumID)
        {
            return _clientAPI.createThread(title, content, subForumID);
        }

        public int defineForumPolicy(Policy policy)
        {
            return _clientAPI.defineForumPolicy(policy);
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            return _clientAPI.deleteModerator(moderatorID, subForumID);
        }

        public int deletePost(int postID)
        {
            return _clientAPI.deletePost(postID);
        }

        public int editPost(int postID, string content)
        {
            return _clientAPI.editPost(postID, content);
        }

        public Client.DataClasses.Admin getAdmin(int AdminID, int forumID)
        {
            return _clientAPI.getAdmin(AdminID, forumID);
        }

        public List<Client.DataClasses.Admin> getAdmins(int forumID)
        {
            return _clientAPI.getAdmins(forumID);
        }

        public List<Forum> getAllForums()
        {
            return _clientAPI.getAllForums();
        }

        public List<Client.DataClasses.Notification> getAllNotificationses()
        {
            return _clientAPI.getAllNotificationses();
        }

        public Forum getForum(int forumID)
        {
            return _clientAPI.getForum(forumID);
        }

        public List<User> getMembers(int forumID)
        {
            return _clientAPI.getMembers(forumID);
        }

        public List<Client.DataClasses.Moderator> getModerators(int subForumID)
        {
            return _clientAPI.getModerators(subForumID);
        }

        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            return _clientAPI.getModeratorTermTime(moderatorID, subforumID);
        }

        public List<Client.DataClasses.Notification> getNewNotificationses()
        {
            return _clientAPI.getNewNotificationses();
        }

        public List<Post> getReplys(int postID)
        {
            return _clientAPI.getReplys(postID);
        }

        public Subforum getSubforum(int subforumId)
        {
            return _clientAPI.getSubforum(subforumId);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            return _clientAPI.getSubforums(forumID);
        }

        public Post getThread(int threadId)
        {
            return _clientAPI.getThread(threadId);
        }

        public List<Post> getThreads(int subForumID)
        {
            return _clientAPI.getThreads(subForumID);
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return _clientAPI.initialize(name, userName, ID, email, pass);
        }

        public int isInitialize()
        {
            return _clientAPI.isInitialize();
        }

        public User login(string userName, string password, int forumID)
        {
            return _clientAPI.login(userName, password, forumID);
        }

        public User loginBySession(string session)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            return _clientAPI.loginSU(userName, password);
        }

        public List<User> membersInDifferentForums()
        {
            return _clientAPI.membersInDifferentForums();
        }

        public ModeratorReport moderatorReport()
        {
            return _clientAPI.moderatorReport();
        }

        public List<Post> postsByMember(int userID)
        {
            return _clientAPI.postsByMember(userID);
        }

        public int sendMessage(int targetUserID, string message)
        {
            return _clientAPI.sendMessage(targetUserID, message);
        }

        public void setForumID(int forumID)
        {
            throw new NotImplementedException();
        }

        public int subForumTotalMessages(int subForumID)
        {
            return _clientAPI.subForumTotalMessages(subForumID);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return _clientAPI.subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public int totalForums()
        {
            return _clientAPI.totalForums();
        }

        public int updateModeratorTerm(int subforumID, int moderatorID, DateTime term)
        {
            return _clientAPI.updateModeratorTerm(subforumID, moderatorID, term);
        }

    }
}
