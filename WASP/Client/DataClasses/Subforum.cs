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
            id = countId++;
            this.name = name;
            this.description = description;

            moderators = new Dictionary<int, Moderator>();
            moderators.Add(moderator.user.id, moderator);

            threads = new List<Post>();
            this.term = term;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

       
        public Dictionary<int, Moderator> moderators { get; set; }
        public List<Post> threads { get; set; }

        internal static Dictionary<int, Subforum> ListToDictionary(List<Subforum> list)
        {
            Dictionary<int, Subforum> ret = new Dictionary<int, Subforum>();
            foreach(Subforum sf in list)
            {
                ret.Add(sf.id, sf);
            }
            return ret;
        }
    }
}