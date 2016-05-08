using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public abstract class Authority
    {
        public enum Level : byte
        {
            Guest=0, User = 1, Mod = 2, Admin = 4
        }


        public abstract Level AuthorizationLevel();
    }
}
