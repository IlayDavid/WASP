using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WASP
{
    public class Subforum
    {
        private static int _idCounter = 0;
        private readonly List<Tuple<Member,DateTime> > _moderators=new List<Tuple<Member, DateTime>>();
        private readonly List<Post> _threads=new List<Post>();

        public Subforum (String name,String description)
        {
            Id = _idCounter;
            _idCounter++;
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public bool IsModerator (Member moderator)
        {
            return _moderators.First((x) => x.Item1 == moderator)!=null;
        }

        public void AddModerator(Member mod,DateTime expr)
        {
            Tuple<Member, DateTime> tup = new Tuple<Member, DateTime>(mod, expr);
            
            _moderators.Add(tup);
        }
      
        public void AddThread (Post tr)
        {
            _threads.Add( tr);
        }
 
       public void RemoveModerator(Member moderator)
        {
            _moderators.Remove(_moderators.First((x) => x.Item1 == moderator));
        }
       public void RemoveThread(Post post)
        {
            _threads.Remove(post);
        }


        public List<Post> GetThreads()
        {
            return _threads;
        }
        public List<Tuple<Member, DateTime>> GetModerators()
        {
            return _moderators;
        }
        public Post GetThread(int id)
        {
            
            return _threads.First((x)=>x.Id==id);
        }
        public Tuple<Member, DateTime> GetModerator(Member moderator)
        {
            return _moderators.First((x) => x.Item1 == moderator);
        }
    }
}