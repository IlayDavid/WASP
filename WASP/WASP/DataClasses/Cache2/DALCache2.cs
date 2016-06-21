using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using WASP.DataClasses;

namespace WASP.DataClasses.Cache2
{
    class DALCache2 : IDALCache2
    {
        private const string _Forum = "Forum";
        private const string _Subforum = "Subforum";
        private const string _Post = "Post";
        private const string _SuperUser = "SuperUser";
        private const string _User = "User";
        private const string _Moderator = "Moderator";
        private const string _Admin = "Admin";


        //delete, get for posts


        private TimeSpan _postExpiration = new TimeSpan(0, 1, 0);
        public void setPostExpiration(int hours, int minutes, int secs)
        {
            _postExpiration = new TimeSpan(hours, minutes, secs);
        }

        private ObjectCache _cache;
        public DALCache2(DAL2 dal)
        {
            _cache = MemoryCache.Default;
        }


        public void AddSuperUser(SuperUser su)
        {
            RemoveSuperUser(su.Id);
            CacheItem forum = new CacheItem(_SuperUser + su.Id.ToString(), su);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddForum(Forum f)
        {
            RemoveForum(f.Id);
            CacheItem forum = new CacheItem(_Forum + f.Id.ToString(), f);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddSubforum(Subforum sf)
        {
            RemoveSubforum(sf.Id);
            CacheItem sforum = new CacheItem(_Subforum + sf.Id.ToString(), sf);
            _cache.Add(sforum, new CacheItemPolicy());
        }
        public void AddUser(User user)
        {
            RemoveUser(user.Id, user.Forum.Id);
            CacheItem us = new CacheItem(_User + user.Id.ToString() + _Forum + user.Forum.Id, user);
            _cache.Add(us, new CacheItemPolicy());
        }
        public void AddModerator(Moderator mod)
        {
            RemoveModerator(mod.Id, mod.SubForum.Id);
            CacheItem moderator = new CacheItem(_Moderator + mod.Id + _Subforum + mod.SubForum.Id, mod);
            _cache.Add(moderator, new CacheItemPolicy());
        }
        public void AddAdmin(Admin admin)
        {
            RemoveAdmin(admin.Id, admin.Forum.Id);
            CacheItem adm = new CacheItem(_Admin + admin.Id.ToString() + _Forum + admin.Forum.Id, admin);
            _cache.Add(adm, new CacheItemPolicy());
        }
        public void AddPost(Post post)
        {
            RemovePost(post.Id);
            CacheItem pst = new CacheItem(_Post + post.Id.ToString(), post);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = _postExpiration;
            _cache.Add(pst, policy);
        }

        public void RemoveSuperUser(int id)
        {
            _cache.Remove(_SuperUser + id.ToString());
        }
        public void RemoveForum(int id)
        {
            _cache.Remove(_Forum + id.ToString());
        }
        public void RemoveSubforum(int id)
        {
            _cache.Remove(_Subforum + id.ToString());
        }
        public void RemoveUser(int id, int forum)
        {
            _cache.Remove(_User + id + _Forum + forum);
        }
        public void RemoveModerator(int id, int subforum)
        {
            _cache.Remove(_Moderator + id + _Subforum + subforum);
        }
        public void RemoveAdmin(int id, int forum)
        {
            _cache.Remove(_Admin + id + _Forum + forum);
        }
        public void RemovePost(int p)
        {
            _cache.Remove(_Post + p.ToString());
        }

        public Forum GetForum(int f)
        {
            object forum = _cache.Get(_Forum + f.ToString());
            return (Forum)forum;
        }
        public Subforum GetSubforum(int sf)
        {
            object subforum = _cache.Get(_Subforum + sf.ToString());
            return (Subforum)subforum;
        }
        public SuperUser GetSuperUser(int su)
        {
            object superuser = _cache.Get(_SuperUser + su.ToString());
            return (SuperUser)superuser;

        }
        public User GetUser(int user, int forum)
        {
            object userobj = _cache.Get(_User + user + _Forum + forum);
            return (User)userobj;

        }
        public Moderator GetModerator(int mod, int subforum)
        {
            object moderator = _cache.Get(_Moderator + mod + _Subforum + subforum);
            return (Moderator)moderator;


        }
        public Admin GetAdmin(int admin, int forum)
        {
            object adm = _cache.Get(_Admin + admin + _Forum + forum);

            return (Admin)adm;

        }
        public Post GetPost(int p)
        {
            object post = _cache.Get(_Post + p.ToString());
            return (Post)post;
        }
    }
}
