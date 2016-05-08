using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    class InitializeException : WaspException
    {
        public InitializeException(string msg)
            : base(msg)
        {
        }

    }
}
