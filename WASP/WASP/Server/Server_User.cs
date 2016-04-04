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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getModeratorTermTime(member, moderator, subforum);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.addModerator(member, moderator, subforum, term);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Member login(string userName, string password, Forum forum)
        {
            try
            {
                IBL bl = null;
                forumsBL.TryGetValue(forum, out bl);

                return bl.login(userName, password);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.confirmEmail(member);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.updateModeratorTerm(member, moderator, subforum, term);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public Member subscribeToForum(String userName, String name, String email, String pass, Forum targetForum)
        {
            try
            {
                IBL bl = null;
                forumsBL.TryGetValue(targetForum, out bl);

                return bl.subscribeToForum(userName, name, email, pass);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            try
            {
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.sendMessage(member, targetMember, message);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
