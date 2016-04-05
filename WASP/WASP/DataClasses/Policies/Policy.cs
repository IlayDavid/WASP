using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public abstract class Policy
    {
        Policy next;
        public abstract void Validate(User user);
        public Policy Next
        {
            get
            {
                return this.next;
            }
            set
            {
                this.next = value;
            }
        }
    }
}
