using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedAddModerator : UnauthorizedException
    {
        public UnauthorizedAddModerator(int userID, int subForumID) 
            : base(userID, String.Format("add moderator at subForum: {0}", subForumID))
        {
        }
    }
}
