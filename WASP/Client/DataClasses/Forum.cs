using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Forum
    {
        public int ID { get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Policy policy { get; set; }
        public List<int> subforumsIDs { get; set; }

        //should be null at first request.
        public List<int> membersIDs { get; set; }
        public List<int> adminsIDs { get; set; }
        public Dictionary<string,User> members { get; set; }
        public List<User> admins { get; set; }

        public List<Subforum> subforums { get; set; }
    }
}