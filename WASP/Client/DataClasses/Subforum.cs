using System;
using System.Collections.Generic;
using Client.BusinessLogic;

namespace Client.DataClasses
{
    public class Subforum
    {
        private DateTime term;
        private static int countId = 0;

        public Subforum() { }
        public Subforum(string name, string description, Moderator moderator, DateTime term)
        {
            Id = countId++;
            Name = name;
            Description = description;

            _moderators = new Dictionary<int, Moderator>();
            _moderators.Add(moderator.user.id, moderator);

            _threads = new List<Post>();
            this.term = term;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //should be null at first request
        public Dictionary<int, Moderator> _moderators { get; set; }
        public List<Post> _threads { get; set; }

        internal static Dictionary<int, Subforum> ListToDictionary(List<Subforum> list)
        {
            Dictionary<int, Subforum> ret = new Dictionary<int, Subforum>();
            foreach(Subforum sf in list)
            {
                ret.Add(sf.Id, sf);
            }
            return ret;
        }
    }
}