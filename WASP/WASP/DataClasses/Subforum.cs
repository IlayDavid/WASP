using System;
using System.Collections.Generic;
namespace WASP.DataClasses
{
    public class Subforum
    {
        private int id;
        private string name, description;
        private Dictionary<int, Moderator> moderators = null;
        private Dictionary<int, Post> threads = null;
        private Forum forum;
        private static DAL2 dal = WASP.Config.Settings.GetDal();
        public static Subforum Get(int id)
        {
            return dal.GetSubForum(id);
        }

        public Subforum Create()
        {
            return dal.CreateSubForum(this);
        }

        public Subforum Update()
        {
            return dal.UpdateSubForum(this);
        }

        public bool Delete()
        {
            return dal.DeleteSubforum(Id);
        }

        public Subforum(int id, String name, String description, Forum forum)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.forum = forum;
        }

        // DEPRECATED
        public Subforum(int id, String name, String description, Forum forum, DAL2 dal)
        {
            this.id = id;
            this.name = name;
            this.description = description;
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
               // if (this.forum == null)
                 //   this.forum = dal.GetSubForumForum(Id);
                return forum;
            }
            set
            {
                forum = value;
            }
        }

        private Dictionary<int, Moderator> Moderators
        {
            get
            {
                if(moderators == null)
                {
                    moderators = new Dictionary<int, Moderator>();
                    foreach(Moderator mod in dal.GetSubForumMods(Id))
                    {
                        moderators.Add(mod.Id, mod);
                    }
                }
                return moderators;
            }
        }
        private Dictionary<int, Post> Threads
        {
            get
            {
                if (threads == null)
                {
                    threads = new Dictionary<int, Post>();
                    foreach (Post thread in dal.GetSubForumThreads(Id))
                    {
                        threads.Add(thread.Id, thread);
                    }
                }
                return threads;
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
            return Moderators.ContainsKey(id);
        }

        public void AddModerator(Moderator mod)
        {
            forum.Policy.Validate(mod);
            Moderators.Add(mod.Id, mod);
        }

        public void AddThread(Post tr)
        {
            Threads.Add(tr.Id, tr);
        }

        public void RemoveModerator(int id)
        {
            Moderators.Remove(id);
        }
        public void RemoveThread(int id)
        {
            Threads.Remove(id);
        }


        public Post[] GetThreads()
        {
            Post[] tr = new Post[Threads.Values.Count];
            Threads.Values.CopyTo(tr, 0);
            return tr;
        }
        public Moderator[] GetAllModerators()
        {
             Moderator[] mods= new Moderator[Moderators.Values.Count];
            Moderators.Values.CopyTo(mods, 0);
            return mods;
        }

        
        public Post GetThread(int id)
        {
            Post theThread;
            Threads.TryGetValue(id, out theThread);
            return theThread;
        }
        public Moderator GetModerator(int id)
        {
            Moderator mod;
            Moderators.TryGetValue(id, out mod);
            return mod;
        }

    }
}