using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class LoginException : WaspException
    {
        public LoginException(string msg)
            : base(msg)
        {

        }
    }
}
