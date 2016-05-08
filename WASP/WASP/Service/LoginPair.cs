using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Service
{
    public class LoginPair
    {
        private int userId, forumId;
        public LoginPair(int userId):this(userId, -1)
        {
        }

        public LoginPair(int userId, int forumId)
        {
            this.forumId = forumId;
            this.userId = userId;
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int ForumId
        {
            get { return forumId; }
            set { forumId = value; }
        }
    }
}
