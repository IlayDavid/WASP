using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP
{
    public class User
    {

        public User()
        {
            DataBaseManager.Instance.AddUser(this);
        }
        private Dictionary<Forum, Clearance> _clearances=new Dictionary<Forum, Clearance>();
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

        internal void sendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }

    public enum Clearance
    {
        Guest, Normal, Moderator, Administrator, Superuser
    }
}
