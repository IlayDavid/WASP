using System;
using System.Collections.Generic;
using System.Linq;

namespace WASP.DataClasses
{
    public class Subforum
    {
        private static int _idCounter = 0;
        private readonly List<Tuple<Member, DateTime>> _moderators;
        private readonly List<Post> _threads;

        public Subforum(String name, String description, Member moderator, DateTime term)
        {
            Id = _idCounter++;
            Name = name;
            Description = description;
            _moderators = new List<Tuple<Member, DateTime>>();
            _threads = new List<Post>();
            AddModerator(moderator, term);
        }

        public Subforum(int id, String name, String description, Member moderator, DateTime term)
        {
            Id = id;
            Name = name;
            Description = description;
            _moderators = new List<Tuple<Member, DateTime>>();
            _threads = new List<Post>();
            AddModerator(moderator, term);
        }

        public static bool isValid(String name, String description, Member moderator, DateTime term)
        {
            return !(Helper.isEmptyString(name) || Helper.isEmptyString(description)
                || (moderator == null) || (term==null));
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public bool IsModerator(Member moderator)
        {
            return _moderators.First((x) => x.Item1 == moderator) != null;
        }

        public void AddModerator(Member mod, DateTime expr)
        {
            Tuple<Member, DateTime> tup = new Tuple<Member, DateTime>(mod, expr);

            _moderators.Add(tup);
        }

        public void AddThread(Post tr)
        {
            _threads.Add(tr);
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

            return _threads.First((x) => x.Id == id);
        }
        public Tuple<Member, DateTime> GetModerator(Member moderator)
        {
            return _moderators.First((x) => x.Item1 == moderator);
        }
    }
}