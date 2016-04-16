using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public interface Policy
    {
        Policy Next { get; set; }

        void Validate(User user);
    }
}
