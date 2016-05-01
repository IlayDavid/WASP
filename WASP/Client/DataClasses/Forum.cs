using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Forum
    {
        private static int countId = 0;
        public Forum(){ }
        public Forum(string name, string description, User admin)
        {
            subforums = new Dictionary<int, Subforum>();
            Name = name;
            Description = description;
            ID = countId++;
            admins = new Dictionary<int, User>();
            admins.Add(admin.id, admin);
            members = new Dictionary<int, User>();
            members.Add(admin.id, admin);
        }
        public int ID { get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Policy policy { get; set; }
        
        public Dictionary<int,User> members { get; set; }
        public Dictionary<int, User> admins { get; set; }

        public Dictionary<int, Subforum> subforums { get; set; }
        public Dictionary<int, Post> posts { get; set; }
    }
}