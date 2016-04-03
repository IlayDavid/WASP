using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    /// <summary>
    /// This class will be in charge of the data base.
    /// Functions in the Server send data to the DBM, and the DBM will store it according to its logic.
    /// </summary>
    public class DataBaseManager
    {
        private List<Member> _users=new List<Member>();
        private static DataBaseManager _dataBaseManager=new DataBaseManager();
        private DataBaseManager()
        {
        }

        public static DataBaseManager Instance
        {
            get { return _dataBaseManager; }
        }
        public void AddUser(Member member)
        {
            _users.Add(member);
        }

        public Member GetUser(string password, string username)
        {
            return _users.First((x) => x.Password.Equals(password) && x.Username.Equals(username));
        }

        /// <summary>
        /// DO NOT USE! USED FOR TEST SUITS ONLY!
        /// </summary>
        public void clear()
        {
            _users.Clear();
        }
    }
}
