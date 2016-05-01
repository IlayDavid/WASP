﻿
namespace Client.DataClasses
{
    public class User
    {
        public User() { }
        public User(int id, string name, string userName, string email, string pass)
        {
            this.id = id;
            this.name = name;
            this.userName = userName;
            this.email = email;
            this.password = pass;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}