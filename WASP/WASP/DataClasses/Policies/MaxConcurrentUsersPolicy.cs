using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public class MaxConcurrentUsersPolicy :Policy
    {
        public MaxConcurrentUsersPolicy(Policy next, int max = 101)
        {
            
        }
    }
}
