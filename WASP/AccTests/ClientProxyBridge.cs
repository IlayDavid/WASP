using System;
using System.Collections.Generic;
using Client.DataClasses;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using Forum = Client.DataClasses.Forum;
using Message = Client.DataClasses.Message;
using Policy = Client.DataClasses.Policy;
using Post = Client.DataClasses.Post;
using Subforum = Client.DataClasses.Subforum;
using SuperUser = Client.DataClasses.SuperUser;
using User = Client.DataClasses.User;

namespace AccTests
{
    public class ClientProxyBridge : WASPClientBridge
    {
        private ClientRealBridge proj;

        public ClientProxyBridge(ClientRealBridge bridge)
        {
            proj = bridge;
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return proj.initialize(name, userName, ID, email, pass);
        }

        public int isInitialize()
        {
            return proj.isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName,
            string email, string pass, Policy policy)
        {
            return proj.createForum(userID, forumName, description, adminID, adminUserName, adminName, email, pass,
                policy);
        }


        public int defineForumPolicy(int userID, int forumID)
        {
            return proj.defineForumPolicy(userID, forumID);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            return proj.subscribeToForum(id, userName, name, email, pass, targetForumID);
        }


        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            return proj.createThread(userID, forumID, title, content, subForumID);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            return proj.createReplyPost(userID, forumID, content, replyToPost_ID);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            return proj.createSubForum(userID, forumID, name, description, moderatorID, term);
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            return proj.sendMessage(userID, forumID, targetUserNameID, message);
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            return ((Client.BusinessLogic.IBL) proj).addModerator(userID, forumID, moderatorID, subForumID, term);
        }


        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            return proj.updateModeratorTerm(userID, forumID, moderatorID, subforumID, term);
        }

        public int confirmEmail(int userID, int forumID)
        {
            return proj.confirmEmail(userID, forumID);
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            return proj.deletePost(userID, forumID, postID);
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            return proj.editPost(userID, forumID, postID, content);
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            return proj.deleteModerator(userID, subForumID,moderatorID,subForumID);
        }

        public List<Message> getAllNotificationses(int userID, int forumID)
        {
            return ((Client.BusinessLogic.IBL) proj).getAllNotificationses(userID, forumID);
        }

        public List<Message> getNewNotificationses(int userID, int forumID)
        {
            return ((Client.BusinessLogic.IBL) proj).getNewNotificationses(userID, forumID);
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return proj.subForumTotalMessages(userID,forumID,subForumID);
        }

        public List<Post> postsByMember(int adminID, int forumID, int userID)
        {
            return ((Client.BusinessLogic.IBL) proj).postsByMember(adminID, forumID, userID);
        }


        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            return proj.moderatorReport(userID, forumID);
        }

        public int totalForums(int userID)
        {
            return proj.totalForums(userID);
        }

        public List<User> membersInDifferentForums(int userID)
        {
            return proj.membersInDifferentForums(userID);
        }

        public User login(string userName, string password, int forumID)
        {
            return proj.login(userName, userName, forumID);
        }

        public SuperUser loginSU(string userName, string password)
        {
            return proj.loginSU(userName, password);
        }

        public Post getThread(int forumID, int threadId)
        {
            return ((Client.BusinessLogic.IBL) proj).getThread(forumID, threadId);
        }

        public List<Post> getThreads(int forumID, int subForumID, int @from, int amount)
        {
            return ((Client.BusinessLogic.IBL) proj).getThreads(forumID, subForumID, @from, amount);
        }

        public List<Post> getReplays(int forumID, int subForumID, int postID)
        {
            return ((Client.BusinessLogic.IBL) proj).getReplays(forumID, subForumID, postID);
        }


        public Forum getForum(int forumID)
        {
            return proj.getForum(forumID);
        }


        public Subforum getSubforum(int forumID, int subforumId)
        {
            return proj.getSubforum(forumID, subforumId);
        }

        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            return ((Client.BusinessLogic.IBL) proj).getModerators(forumID, subForumID);
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            return proj.getModeratorTermTime(userID, forumID, moderatorID, subforumID);
        }

        public List<Forum> getAllForums()
        {
            return proj.getAllForums();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            return proj.getAdmins(userID, forumID);
        }

        public List<User> getMembers(int userID, int forumID)
        {
            return proj.getMembers(userID, forumID);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            return proj.getSubforums(forumID);
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            return proj.getAdmin(userID, forumID, AdminID);
        }

    }
}
