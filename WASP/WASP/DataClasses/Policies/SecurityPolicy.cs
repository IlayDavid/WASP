using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public class SecurityPolicy : Policy
    {
        public SecurityPolicy(Policy next = null, double passwordDuration = Double.MaxValue)
        {
            
        }
    }
}
