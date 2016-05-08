using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedDeleteSubForum : UnauthorizedException
    {
        public UnauthorizedDeleteSubForum(int userID) 
            : base(userID, "deleteSubForum")
        {
        }
    }
}
