using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Forum
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _description{ get; set; }
        public Policy policy { get; set; }
        public List<int> subforumsIDs { get; set; }

        //should be null at first request.
        public List<int> membersIDs { get; set; }
        public List<int> adminsIDs { get; set; }
        public List<Member> members { get; set; }
        public List<Member> admins { get; set; }

        public List<Subforum> subforums { get; set; }
    }
}