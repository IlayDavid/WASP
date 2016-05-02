using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedAddAdmin : UnauthorizedException
    {
        public UnauthorizedAddAdmin(int userID, int forumID) 
            : base(userID, String.Format("add Admin at subForum: {0}", forumID))
        {
        }
    }
}
