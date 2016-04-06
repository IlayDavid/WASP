using System;
using System.Linq;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP.Domain;

namespace WASP.Server
{
    // partial class of Server.
    // in charge of posts and other functions
    // server_user has explicit user methods
    // server_forum has explicit Forum methods
    public partial class Server : ServerAPI
    {
        IBL bl = new BL(new DAL());

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

        public Post createThread(Member author, String title, String content, 
            DateTime now, Subforum subforum)
        {
            if (content.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(author.MemberForum);
                return forum_bl.createThread(author,title, content, now, subforum);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post createReplyPost(Member author, String content, 
            DateTime now, Post inReplyTo)
        {
            if (content.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(author.MemberForum);
                return forum_bl.createReplyPost(author, content, now, inReplyTo);
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
        //TODO: in next revision, move from memory to DAL
        public SuperUser login(string userName, string password)
        {
            if (userName.Equals("") || password.Equals(""))
                return null;
            try
            {
                return bl.login(userName, password);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Member getAdmin(User user, Forum forum, string userName)
        {
            return getAdmins(user, forum).First((x) => x.UserName.Equals(userName));
        }
    }
}