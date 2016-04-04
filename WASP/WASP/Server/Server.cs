using System;
using System.Collections.Generic;
using WASP.Domain;

namespace WASP.Server
{
    // partial class of Server.
    // in charge of posts and other functions
    // server_user has explicit user methods
    // server_forum has explicit forum methods
    partial class Server : ServerAPI
    {
        Dictionary<Forum, IBL> forumsBL = new Dictionary<Forum, IBL>();

        public int deletePost(Member member, Post post)
        {
            IBL bl = null;
            forumsBL.TryGetValue(member.MemberForum, out bl);
            try
            {
                return _bl.deletePost(userName, threadId, postId);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Post getThread(Member member, int threadId)
        {
            try
            {
                return _bl.getThread(userId, threadId);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post createThread(Member author, String title, String content, 
            DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            try
            {
                return _bl.createThread(userName, subforumId, thread);

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Post createReplyPost(Member Author, String title, String content, 
            DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            try
            {
                return _bl.createPost(userName, threadId, post);
            }
            catch (Exception)
            {
                return -1;                
            }
        }

        public SuperUser initialize(String name, String userName, String email, String pass)
        {
            try
            {
                return _bl.initialize();
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}