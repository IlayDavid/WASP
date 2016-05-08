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
        public Moderator addModerator(int userId,int forumId, int moderatorId, int subforumId, DateTime term)
        {
            return _clientAPI.addModerator(userId,forumId, moderatorId, subforumId, term);
        }

        public int confirmEmail(int userId, int forumId)
        {
            return _clientAPI.confirmEmail(userId,forumId);
        }

        public int isInitialize()
        {
            return _clientAPI.isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName,
            string email, string pass, Policy policy)
        {
            return _clientAPI.createForum(userID, forumName, description,adminID, adminUserName,adminName, email, pass, policy);
        }
        
        public int defineForumPolicy(int userID, int forumID)
        {
            return _clientAPI.defineForumPolicy(userID, forumID);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return _clientAPI.subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public Post createReplyPost(int userId, int forumId, string content, int inReplyToId)
        {
            return _clientAPI.createReplyPost(userId,forumId, content, inReplyToId);
        }

        public Subforum createSubForum(int userId, int forumId,string name, string description, int moderatorId, DateTime term)
        {
            return _clientAPI.createSubForum(userId,forumId, name, description, moderatorId, term);
        }

        public Post createThread(int userId, int forumId, string title, string content, int containerId)
        {
            return _clientAPI.createThread(userId,forumId, title, content, containerId);
        }
        

        public int deletePost(int userId, int forumId, int postId)
        {
            return _clientAPI.deletePost(userId,forumId, postId);
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            return _clientAPI.editPost(userID, forumID, postID, content);
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            return _clientAPI.deleteModerator(userID, forumID, moderatorID, subForumID);
        }

        public List<Notifications> getAllNotificationses(int userID, int forumID)
        {
            return _clientAPI.getAllNotificationses(userID, forumID);
        }

        public List<Notifications> getNewNotificationses(int userID, int forumID)
        {
            return _clientAPI.getNewNotificationses(userID, forumID);
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return _clientAPI.subForumTotalMessages(userID, forumID, subForumID);
        }

        public List<Post> postsByMember(int adminID, int forumID, int userID)
        {
            return _clientAPI.postsByMember(adminID, forumID, userID);
        }


        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            return _clientAPI.moderatorReport(userID, forumID);
        }

        public int totalForums(int userID)
        {
            return _clientAPI.totalForums(userID);
        }

        public List<User> membersInDifferentForums(int userID)
        {
            return _clientAPI.membersInDifferentForums(userID);
        }
        

        public List<Admin> getAdmins(int userId, int forumId)
        {
            return _clientAPI.getAdmins(userId, forumId);
        }

        public List<Forum> getAllForums()
        {
            return _clientAPI.getAllForums();
        }

        public List<Post> getReplays(int forumID, int subForumID, int postID)
        {
            return _clientAPI.getReplays(forumID, subForumID, postID);
        }

        public Forum getForum(int forumId)
        {
            return _clientAPI.getForum( forumId);
        }

        public List<User> getMembers(int userId, int forumId)
        {
            return _clientAPI.getMembers(userId, forumId);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            return _clientAPI.getSubforums(forumID);
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            return _clientAPI.getAdmin(userID, forumID, AdminID);
        }

        public List<Moderator> getModerators(int forumId, int subforumId)
        {
            return _clientAPI.getModerators(forumId, subforumId);
        }

        public DateTime getModeratorTermTime(int userId, int forumId, int moderatorId, int subforumId)
        {
            return _clientAPI.getModeratorTermTime(userId,forumId, moderatorId, subforumId);
        }

        public Subforum getSubforum(int forumId, int subforumId)
        {
            return _clientAPI.getSubforum(forumId, subforumId);
            
        }

        public SuperUser loginSU(string userName, string password)
        {
            return _clientAPI.loginSU(userName, password);
        }

        public Post getThread(int forumId, int threadId)
        {
            return _clientAPI.getThread(forumId, threadId);
        }

        public List<Post> getThreads(int forumID, int subForumID, int @from, int amount)
        {
            return _clientAPI.getThreads(forumID, subForumID, @from, amount);
        }

        public SuperUser initialize(string name, string userName,int id, string email, string pass)
        {
            return _clientAPI.initialize(name, userName, id, email, pass);
        }

        public User login(string userName, string password, int forumId)
        {
            return _clientAPI.login(userName, password, forumId);
        }

        public int sendMessage(int userId, int forumId, string targetMemberId, string message)
        {
            return _clientAPI.sendMessage(userId,forumId, targetMemberId, message);
        }

        public int updateModeratorTerm(int userId, int forumId, int moderatorId, int subforumId, DateTime term)
        {
            return _clientAPI.updateModeratorTerm(userId,forumId, moderatorId, subforumId, term);
        }
    }
}
