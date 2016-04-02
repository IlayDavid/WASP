using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace WASP
{
    public class Subforum
    {
        private int id;
        private String name, description;
        private Dictionary<int, Tuple<User,DateTime> > moderators;
        private Dictionary<int, Post> threads;



        public Subforum (int id, String name,String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
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

        public Boolean IsModerator (int id)
        {
            return moderators.ContainsKey(id);
        }

        public void AddModerator(User mod,DateTime expr)
        {
            Tuple<User, DateTime> tup = new Tuple<User, DateTime>(mod, expr);
            
            moderators.Add(mod.Id, tup);
        }
      
        public void AddThread (Post tr)
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
        public Tuple<User, DateTime>[] GetModerators()
        {
            Tuple<User, DateTime>[] mods = new Tuple<User, DateTime>[moderators.Values.Count];
            moderators.Values.CopyTo(mods, 0);
            return mods;
        }
        public Post GetThread(int id)
        {
            Post theThread;
            threads.TryGetValue(id, out theThread);
            return theThread;
        }
        public Tuple<User, DateTime> GetModerator(int id)
        {
            Tuple<User, DateTime> mod;
            moderators.TryGetValue(id,out mod);
            return mod;
        }
    }
}