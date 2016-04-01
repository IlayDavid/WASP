using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Domain
{
    interface IBL
    {
        string addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term);
        string confirmEmail(int user_ID);
        string createForum(Forum forum);
        string createPost(int user_ID, int thread_ID, Post post);
        string getThread(int user_ID, int thread_ID);
        string getSubForum(int user_ID, int sf_ID);
        string getForum(int user_ID, int forum_ID);
        string deletePost(int user_ID, int thread_ID, int post);
        string defineForumPolicy(int user_ID, Forum forum);
        string createThread(int user_ID, int sf_ID, Thread thread);
        string createSubForum(int user_ID, Subforum sf);
        string initialize();
        string sendMessage(int user_ID, Message message);
        string subscribeToForum(User user, int forum_ID);
        string updateForum(int user_ID, Forum forum);
        string updateModeratorTerm(int user_ID, int moderator_ID, int sf_ID, DateTime term);
    }
}
