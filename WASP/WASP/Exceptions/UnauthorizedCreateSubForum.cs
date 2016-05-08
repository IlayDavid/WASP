using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedCreateSubForum :UnauthorizedException
    {
        public UnauthorizedCreateSubForum(int userID, int forumID) 
            : base(userID, String.Format("create subForum at forum: {0}", forumID))
        {
        }
    }
}
