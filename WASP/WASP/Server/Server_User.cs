using System;
using WASP.Domain;

namespace WASP.Server
{
    partial class Server
    {
        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getModeratorTermTime(member, moderator, subforum);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }
        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.addModerator(member, moderator, subforum, term);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Member login(string userName, string password, Forum forum)
        {
            if (userName.Equals("") || password.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(forum);
                return forum_bl.login(userName, password);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int confirmEmail(Member member)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.confirmEmail(member);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.updateModeratorTerm(member, moderator, subforum, term);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public Member subscribeToForum(String userName, String name, String email, String pass, Forum targetForum)
        {
            if (userName.Equals("") || name.Equals("") || !email.Contains("@") || !email.Contains(".") ||
                pass.Equals(""))
                return null;
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(targetForum);
                return forum_bl.subscribeToForum(userName, name, email, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.sendMessage(member, targetMember, message);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
