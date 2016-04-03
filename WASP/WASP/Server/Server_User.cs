using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Server
{
    partial class Server
    {
        public DateTime getModeratorTermTime(string userName, int subforumId)
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
        public int addModerator(string userId, string userId1, int sfId, DateTime term)
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

        public int login(string userName, string password)
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
        public void confirmEmail(int userId)
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
        public int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term)
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
        public string subscribeToForum(User user, int forum_ID)
        {
            try
            {
                return _bl.subscribeToForum(user, forum_ID);

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public int sendMessage(string userSend, string userAcc, Message message)
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
