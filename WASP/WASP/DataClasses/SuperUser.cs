using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP
{
    public class SuperUser : User
    {
        private static bool _initialized=false;

        public SuperUser(string userName, string name, String email, String pass)
        {
            UserName = userName;
            Name = name;
            Email = email;
            Password = pass;
            if (_initialized)
            {
                throw new Exception("cannot create a second superuser");
            }
            else
            {
                _initialized = true;
            }
        }
    }
}