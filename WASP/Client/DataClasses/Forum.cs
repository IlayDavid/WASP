﻿using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Forum
    {
        private static int countId = 0;
        public Forum(){ }
        public Forum(string name, string description, User admin, Policy policy)
        {
            subforums = new Dictionary<int, Subforum>();
            Name = name;
            Description = description;
            id = countId++;
            admins = new Dictionary<int, Admin>();
            Admin newAdmin = new Admin(admin);
            admins.Add(admin.id, newAdmin);
            members = new Dictionary<int, User>();
            members.Add(admin.id, admin);
            this.policy = policy;
        }
        public Forum(int id, string name, string description, User admin, Policy policy)
        {
            subforums = new Dictionary<int, Subforum>();
            Name = name;
            Description = description;
            this.id = id;
            admins = new Dictionary<int, Admin>();
            Admin newAdmin = new Admin(admin);
            admins.Add(admin.id, newAdmin);
            members = new Dictionary<int, User>();
            members.Add(admin.id, admin);
            this.policy = policy;
        }
        public int id { get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Policy policy { get; set; }
        
        public Dictionary<int,User> members { get; set; }
        public Dictionary<int, Admin> admins { get; set; }

        public Dictionary<int, Subforum> subforums { get; set; }
        public Dictionary<int, Post> posts { get; set; }
    }
}