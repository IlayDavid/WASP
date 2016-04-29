using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedAddNewThread : UnauthorizedException
    {
        public UnauthorizedAddNewThread(int userID, int subForumID) 
            : base(userID, String.Format("add thread at subForum : {0} ", subForumID))
        {
        }
    }
}
