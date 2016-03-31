using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP
{
    class SuperUser : User
    {
        public override Clearance GetClearance(Forum forum)
        {
            return Clearance.Superuser;
        }

        private static bool _initialized = false;

        private SuperUser()
        {
        }

        public static SuperUser CreateSuperUser()
        {
            if (!_initialized)
            {
                _initialized = true;
                return new SuperUser();
            }
            throw new Exception("Attempted to initialize a second SuperUser");
        }
        /// <summary>
        /// DO NOT USE! USED FOR TEST SUITS ONLY!
        /// </summary>
        public static void clear()
        {
            _initialized = false;
        }
    }
}