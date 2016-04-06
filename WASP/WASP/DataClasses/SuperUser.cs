using System;

namespace WASP.DataClasses
{
    public class SuperUser : User
    {
        public SuperUser(string userName, String name, String email, String pass)
        {
            UserName = userName;
            Name = name;
            Email = email;
            Password = pass;
            
        }

        public static bool isValid(string userName, String name, String email, String pass)
        {
            return !(Helper.isEmptyString(userName) || Helper.isEmptyString(name) 
                || Helper.isEmptyString(email) || Helper.isEmptyString(pass));
        }
    }
}