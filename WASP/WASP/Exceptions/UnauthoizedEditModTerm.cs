using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthoizedEditModTerm : UnauthorizedException
    {
        public UnauthoizedEditModTerm(int userID, int modId, int subForumID) 
            : base(userID, String.Format("edit term of management for mod: {0} , subForum : {1}", modId, subForumID))
        {
        }
    }
}
