using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Forum
    {
        private static int countId = 0;
        public Forum(string name, string description, User admin)
        {
            members = new Dictionary<int, User>();
            subforums = new List<Subforum>();

            Name = name;
            Description = description;
            ID = countId++;
            admins = new List<User>();
            admins.Add(admin);
        } 
        public int ID { get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Policy policy { get; set; }
        public List<int> subforumsIDs { get; set; }

        //should be null at first request.
        public List<int> membersIDs { get; set; }
        public List<int> adminsIDs { get; set; }
        public Dictionary<int,User> members { get; set; }
        public List<User> admins { get; set; }

        public List<Subforum> subforums { get; set; }
    }
}