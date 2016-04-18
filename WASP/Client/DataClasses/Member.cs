using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Member : User
    {
        public bool _isActive { get; set; }
        public List<int> postsIDs { get; set; }
        public List<int> messagesIDs { get; set; }

        public List<Post> posts { get; set; }
        public List<Message> messages { get; set; }
    }
}