using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    class DeletePostPolicy : Policy
    {
        public DeletePostPolicy(Policy next = null, bool user=true, bool moderator=false, bool admin=false)
        {
            
        }
    }
}
