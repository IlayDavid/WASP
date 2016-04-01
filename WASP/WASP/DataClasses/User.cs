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
        private bool isSuperMan;
        private String name;
        private String userName;
        private String email;
        private String pass;

        public User(bool isSuperMan, String name, String userName, String email, String pass)
        {
            this.isSuperMan = isSuperMan;
            this.name = name;
            this.userName = userName;
            this.email = email;
            this.pass = pass;
        }

        public String Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public bool IsSuperMan
        {
            get
            {
                return isSuperMan;
            }
        }

        public String Password
        {
            get
            {
                return pass;
            }
            set
            {
                pass = value;
            }
        }
    }
}