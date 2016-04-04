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
            try
            {
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.deletePost(member, post);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getThread(member, threadId);

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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.createThread(author, title, content, now, inReplyTo, container, editAt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post createReplyPost(Member author, String title, String content, 
            DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            try
            {
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.createReplyPost(author, title, content, now, inReplyTo, container, editAt);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public SuperUser initialize(String name, String userName, String email, String pass)
        {
            try
            {
                return BL.initialize();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}