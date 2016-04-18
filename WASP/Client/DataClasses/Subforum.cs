using System;
using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Subforum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //should be null at first request
        public List<int> _threadsIDs { get; set; }
        public List<Tuple<Member, DateTime>> _moderators { get; set; }
        public List<Post> _threads { get; set; }
        
    }
}