using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Exceptions
{
    public abstract class WaspException : Exception
    {
        public WaspException (String errorMsg) : base ("~ERROR: " + errorMsg)
        {
        }




    }
}
