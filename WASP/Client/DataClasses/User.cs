
using System;
using System.Collections.Generic;

namespace Client.DataClasses
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime passCreateDate { get; set; }
        public DateTime joinDate { get; set; }
        public List<User> friends;
        public string client_session;

        public User() { }
        public User(int id, string name, string userName, string email, string pass)
        {
            this.id = id;
            this.name = name;
            this.userName = userName;
            this.email = email;
            this.password = pass;
            this.joinDate = DateTime.Now;
            this.passCreateDate = joinDate;
            this.friends = new List<User>();
            client_session = "need to get from CL";
        }


    }
}