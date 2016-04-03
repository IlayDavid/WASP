using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Domain
{
    interface IBL
    {
        User initialize();
        Thread getThread(int userId, int threadId);
        Forum getForum(int userId, int forumId);
        Subforum getSubforum(int userId, int subforumId);
        int createThread(string userName, int subforumId, Thread thread);
        int createForum(string userName, Forum forum);
        List<User> getModerators(int subforumId);
        DateTime getModeratorTermTime(string userName, int subforumId);
        int createSubForum(string userName, int forumId, Subforum sf);
        List<Forum> getAllForums();
        int createPost(string userName, int threadId, Post post);
        int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term);
        int updateForum(int userId, int forumId);
        int defineForumPolicy(int userId, Forum forum);  //------------------------ policy object??
        string subscribeToForum(User user, int forumId);
        int sendMessage(string userSend, string userAcc, Message message);
        int addModerator(string userId, string userId1, int sfId, DateTime term);
        void confirmEmail(int userId);
        int deletePost(string userName, int threadId, int postId);        
        int login(string userName, string password);
        List<User> getAdmins(int forumId);        
        List<User> getMembers(int forumId);       
        List<Subforum> getSubforums(int forumId);
    }
}
