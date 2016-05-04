using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class UnauthorizedException : WaspException
    {
        public UnauthorizedException(int userId, string msg) 
            : base(String.Format("User {0} is unauthorized to perform action {1}", userId, msg))
        {
        }
    }
}
