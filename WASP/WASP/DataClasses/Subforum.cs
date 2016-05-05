using System;
using System.Collections.Generic;
namespace WASP.DataClasses
{
    public class Subforum
    {
        private int id;
        private String name, description;
        private Dictionary<int, Moderator> moderators=new Dictionary<int, Moderator>();
        private Dictionary<int, Post> threads=new Dictionary<int, Post>();
        private DAL dal;
        private Forum forum;

        public Subforum(int id, String name, String description, Forum forum, DAL dal)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.dal = dal;
            this.forum = forum;

        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Forum Forum
        {
            get
            {
                return forum;
            }
            set
            {
                forum = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public Boolean IsModerator(int id)
        {
            return moderators.ContainsKey(id);
        }

        public void AddModerator(Moderator mod)
        {
            moderators.Add(mod.Id, mod);
        }

        public void AddThread(Post tr)
        {
            threads.Add(tr.Id, tr);
        }

        public void RemoveModerator(int id)
        {
            moderators.Remove(id);
        }
        public void RemoveThread(int id)
        {
            threads.Remove(id);
        }


        public Post[] GetThreads()
        {
            Post[] tr = new Post[threads.Values.Count];
            threads.Values.CopyTo(tr, 0);
            return tr;
        }
        public Moderator[] GetAllModerators()
        {
             Moderator[] mods= new Moderator[moderators.Values.Count];
            moderators.Values.CopyTo(mods, 0);
            return mods;
        }

        
        public Post GetThread(int id)
        {
            Post theThread;
            threads.TryGetValue(id, out theThread);
            return theThread;
        }
        public Moderator GetModerator(int id)
        {
            Moderator mod;
            moderators.TryGetValue(id, out mod);
            return mod;
        }

        public void Delete()
        {
            throw new NotImplementedException("Not a requirement as of yet.");
        }
    }
}