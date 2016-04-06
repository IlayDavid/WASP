using System;

namespace WASP.DataClasses
{
    public class SuperUser : User
    {
        private static bool _initialized = false;

        public SuperUser(string userName, String name, String email, String pass)
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

        public static bool isValid(string userName, String name, String email, String pass)
        {
            return !(Helper.isEmptyString(userName) || Helper.isEmptyString(name) 
                || Helper.isEmptyString(email) || Helper.isEmptyString(pass));
        }
    }
}