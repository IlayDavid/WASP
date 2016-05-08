using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnautorizedDeletePost : UnauthorizedException
    {
        public UnautorizedDeletePost(int userID,int PostID) 
            : base(userID, String.Format("deletePost {0}",PostID))
        {
        }
    }
}
