using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class PolicyException : WaspException
    {
        public PolicyException(string msg)
            : base(msg)
        {

        }
    }
}
