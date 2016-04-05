using System;
using WASP.Domain;

namespace WASP.Server
{
    // partial class of Server.
    // in charge of posts and other functions
    // server_user has explicit user methods
    // server_forum has explicit Forum methods
    partial class Server : ServerAPI
    {
        IBL bl = new BL();

        public int deletePost(Member member, Post post)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.deletePost(member, post);
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
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getThread(member, threadId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post createThread(Member author, String content, 
            DateTime now)
        {
            if (content.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(author.MemberForum);
                return forum_bl.createThread(author, content, now);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post createReplyPost(Member author, String title, String content, 
            DateTime now, Post inReplyTo, Subforum container)
        {
            if (title.Equals("") && content.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(author.MemberForum);
                return forum_bl.createReplyPost(author, title, content, now, inReplyTo, container);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public SuperUser initialize(String name, String userName, String email, String pass)
        {
            if (name.Equals("") || userName.Equals("") || !email.Contains("@") || !email.Contains(".") ||
                pass.Equals(""))
                return null;
            try
            {
                return bl.initialize(name, userName, email, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}