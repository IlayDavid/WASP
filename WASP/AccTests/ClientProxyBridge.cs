using System;
using System.Collections.Generic;
using Client.DataClasses;
using WASP.DataClasses;
using Forum = Client.DataClasses.Forum;
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

        public Client.DataClasses.Admin addAdmin(int newAdminID)
        {
            return proj.addAdmin(newAdminID);
        }

        public void addAnswers(int user_id, List<string> answers)
        {
            proj.addAnswers(user_id, answers);
        }

        public int addFriend(int friendID)
        {
            return proj.addFriend(friendID);
        }

        public Client.DataClasses.Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {
            return proj.addModerator(moderatorID, subForumID, term);
        }

        public int confirmEmail(int code)
        {
            return proj.confirmEmail(code);
        }

        public Forum createForum(string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            return proj.createForum(forumName, description, adminID, adminUserName, adminName, email, pass, policy);
        }

        public Post createReplyPost(string content, int replyToPost_ID)
        {
            return proj.createReplyPost(content, replyToPost_ID);
        }

        public Subforum createSubForum(string name, string description, int moderatorID, DateTime term)
        {
            return proj.createSubForum(name, description, moderatorID, term);
        }

        public Post createThread(string title, string content, int subForumID)
        {
            return proj.createThread(title, content, subForumID);
        }

        public int defineForumPolicy(Policy policy)
        {
            return proj.defineForumPolicy(policy);
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            return proj.deleteModerator(moderatorID, subForumID);
        }

        public int deletePost(int postID)
        {
            return proj.deletePost(postID);
        }

        public int editPost(int postID, string content)
        {
            return proj.editPost(postID, content);
        }

        public Client.DataClasses.Admin getAdmin(int AdminID)
        {
            throw new NotImplementedException();
        }

        public Client.DataClasses.Admin getAdmin(int AdminID, int forumID)
        {
            return proj.getAdmin(AdminID, forumID);
        }

        

        public List<Client.DataClasses.Admin> getAdmins(int forumID)
        {
            return proj.getAdmins(forumID);
        }

        public List<Forum> getAllForums()
        {
            return proj.getAllForums();
        }

        public List<Client.DataClasses.Notification> getAllNotificationses()
        {
            return proj.getAllNotificationses();
        }

        public Forum getForum(int forumID)
        {
            return proj.getForum(forumID);
        }

        public List<User> getFriends()
        {
            return proj.getFriends();
        }

        public List<User> getMembers(int forumID)
        {
            return proj.getMembers(forumID);
        }

        public List<Client.DataClasses.Moderator> getModerators(int subForumID)
        {
            return proj.getModerators(subForumID);
        }

        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            return proj.getModeratorTermTime(moderatorID, subforumID);
        }

        public List<Client.DataClasses.Notification> getNewNotificationses()
        {
            return proj.getNewNotificationses();
        }

        public List<Post> getReplys(int postID)
        {
            return proj.getReplys(postID);
        }

        public Subforum getSubforum(int subforumId)
        {
            return proj.getSubforum(subforumId);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            return proj.getSubforums(forumID);
        }

        public Post getThread(int threadId)
        {
            return proj.getThread(threadId);
        }

        public List<Post> getThreads(int subForumID)
        {
            return proj.getThreads(subForumID);
        }
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return proj.initialize(name, userName, ID, email, pass);
        }

        public void Clean()
        {
            proj.Clean();
        }

        public int isInitialize()
        {
            return proj.isInitialize();
        }

        public User login(string userName, string password, int forumID, string session)
        {
            return proj.login(userName, password, forumID, session);
        }

        public User loginBySession(string session)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            return proj.loginSU(userName, password);
        }

        public void logout()
        {
            proj.logout();
        }

        public List<User> membersInDifferentForums()
        {
            return proj.membersInDifferentForums();
        }

        public ModeratorReport moderatorReport()
        {
            return proj.moderatorReport();
        }

        public List<Post> postsByMember(int userID)
        {
            return proj.postsByMember(userID);
        }

        public string restorePasswordbyAnswer(int userid, string answer, string newPassword)
        {
            return proj.restorePasswordbyAnswer("", answer, newPassword);
        }

        public void restorePasswordbyAnswers(int userid, int forum_id, List<string> answers, string newPassword)
        {
            proj.restorePasswordbyAnswers(userid, forum_id, answers, newPassword);
        }

        public int sendMessage(int targetUserID, string message)
        {
            return proj.sendMessage(targetUserID, message);
        }

        public void setForumID(int forumID)
        {
            throw new NotImplementedException();
        }

        public int subForumTotalMessages(int subForumID)
        {
            return proj.subForumTotalMessages(subForumID);
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID, List<string> answers, bool online)
        {
            return proj.subscribeToForum(id, userName, name, email, pass, targetForumID, answers, online);
        }

        public int totalForums()
        {
            return proj.totalForums();
        }

        public int updateModeratorTerm(int subforumID, int moderatorID, DateTime term)
        {
            return proj.updateModeratorTerm(subforumID, moderatorID, term);
        }
    }
}
