using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public class NewModeratorPolicy :Policy
    {
        public NewModeratorPolicy(Policy next=null, double minimumMembership=0)
        {
            
        }
    }
}
