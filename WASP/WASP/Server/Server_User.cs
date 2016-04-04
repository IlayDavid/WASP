using System;

namespace WASP.Server
{
    partial class Server
    {
        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            try
            {
                return _bl.getModeratorTermTime(userName, subforumId);
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
                return _bl.addModerator(userId, userId1, sfId, term);
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
                return _bl.login(userName, password);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int confirmEmail(Member member)
        {
            try
            {
                _bl.confirmEmail(userId);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            try
            {
                return _bl.updateModeratorTerm(userName1, userName2, sfId, term);
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
                return _bl.subscribeToForum(member, forum_ID);

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            try
            {
                return _bl.sendMessage(userSend, userAcc, message);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
