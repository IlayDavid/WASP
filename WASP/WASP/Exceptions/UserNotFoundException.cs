using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UserNotFoundException : WaspException
    {
        public UserNotFoundException (int userID, int forumID) 
            : base(String.Format("User {0} in forum {1} not found!", userID, forumID))
        {
        }
    }
}
