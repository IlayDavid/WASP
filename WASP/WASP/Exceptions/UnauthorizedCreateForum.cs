using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedCreateForum : UnauthorizedException
    {
        public UnauthorizedCreateForum(int userID, int forumID) 
            : base(userID, String.Format("create forum : {0}", forumID))
        {
        }
    }
}
