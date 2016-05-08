using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedAddReply : UnauthorizedException
    {
        public UnauthorizedAddReply(int userID, int postID) 
            : base(userID, String.Format("add reply to post : {0}", postID))
        {
        }
    }
}
