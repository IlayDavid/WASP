using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedEditPost : UnauthorizedException
    {
        public UnauthorizedEditPost(int userID,int postID, int subForumID) 
            : base(userID, String.Format("edit post  {0} at sub forum {1}", postID, subForumID))
        {
        }
    }
}
