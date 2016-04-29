using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedDeleteModerator : UnauthorizedException
    {
        public UnauthorizedDeleteModerator(int userID, int modID) 
            : base(userID, String.Format("delete moderator : {0}", modID))
        {
        }
    }
}
