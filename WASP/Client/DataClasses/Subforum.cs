using System;
using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Subforum
    {
        private DateTime term;
        private static int countId = 0;
        public Subforum(string name, string description, int moderatorID, DateTime term)
        {
            Id = countId++;
            Name = name;
            Description = description;
            _moderatorIDs = new List<int>();
            _moderatorIDs.Add(moderatorID);
            _threads = new List<Post>();
            this.term = term;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //should be null at first request
        public List<int> _threadsIDs { get; set; }
        public List<int> _moderatorIDs { get; set; }
        public List<Post> _threads { get; set; }
        
    }
}