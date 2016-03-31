using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP
{
    public class User
    {

        private Dictionary<Forum, Clearance> _clearances;
        public string Username { get; set; }
        public string Password { get; set; }

        public void SetClearance(Forum forum, Clearance clearance)
        {
            _clearances[forum] = clearance;
        }

        public virtual Clearance GetClearance(Forum forum)
        {
            return _clearances[forum];
        }
    }

    public enum Clearance
    {
        Guest, Normal, Moderator, Administrator, Superuser
    }
}
