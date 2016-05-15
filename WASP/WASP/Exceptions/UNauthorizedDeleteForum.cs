using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedDeleteForum : UnauthorizedException
    {
        public UnauthorizedDeleteForum(int userID,int forumID) 
            : base(userID, String.Format("delete forum {0}",forumID))
        {
        }
    }
}
