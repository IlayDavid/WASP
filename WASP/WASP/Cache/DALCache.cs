using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using WASP.DataClasses;

namespace WASP.Cache
{
    class DALCache : IDALCache
    {
        private const string _Forum = "Forum";
        private const string _Subforum = "Subforum";
        private const string _Post = "Post";

        private const string _SuperUser = "SuperUser";
        private const string _User = "User";
        private const string _Moderator = "Moderator";
        private const string _Admin = "Admin";
        

        private TimeSpan _postExpiration = new TimeSpan(0, 1, 0);
        public void setPostExpiration(int hours, int minutes, int secs)
        {
            _postExpiration = new TimeSpan(hours, minutes, secs);
        }

        private ObjectCache _cache;
        
        public DALCache()
        {
            _cache = MemoryCache.Default;
           
            
        }


        public void AddSuperUser(SuperUser su)
        {
            CacheItem forum = new CacheItem(su.Id.ToString(), su, _SuperUser);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddForum(Forum f)
        {
            CacheItem forum = new CacheItem(f.Id.ToString(), f, _Forum);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddSubforum(Subforum sf)
        {
            CacheItem sforum = new CacheItem(sf.Id.ToString(), sf, _Subforum);
            _cache.Add(sforum, new CacheItemPolicy());
        }
        public void AddUser(User user)
        {
            CacheItem us = new CacheItem(user.Id.ToString(), user, _User + user.Forum.Id);
            _cache.Add(us, new CacheItemPolicy());
        }
        public void AddModerator(Moderator mod)
        {
            CacheItem moderator = new CacheItem(mod.Id.ToString(), mod, _Moderator + mod.SubForum.Id);
            _cache.Add(moderator, new CacheItemPolicy());
        }
        public void AddAdmin(Admin admin)
        {
            CacheItem adm = new CacheItem(admin.Id.ToString(), admin, _Admin + admin.Forum.Id);
            _cache.Add(adm, new CacheItemPolicy());
        }
        public void AddPost(Post post)
        {
            CacheItem pst = new CacheItem(post.Id.ToString(), post, _Post);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = _postExpiration;
            _cache.Add(pst, policy);
        }

        public void RemoveSuperUser(int id)
        {
            _cache.Remove(id.ToString(), _SuperUser);
        }
        public void RemoveForum(int id)
        {
            _cache.Remove(id.ToString(),_Forum);
        }
        public void RemoveSubforum(int id)
        {
            _cache.Remove(id.ToString(), _Subforum);
        }
        public void RemoveUser(int id, int forum)
        {
            _cache.Remove(id.ToString(), _User + forum);
        }
        public void RemoveModerator(int id, int subforum)
        {
            _cache.Remove(id.ToString(), _Moderator + subforum);
        }
        public void RemoveAdmin(int id, int forum)
        {
            _cache.Remove(id.ToString(), _Admin + forum);
        }

        public Forum GetForum(int f)
        {
            return (Forum)_cache.Get(f.ToString(), _Forum);
        }
        public Subforum GetSubforum(int sf)
        {
            return (Subforum)_cache.Get(sf.ToString(), _Subforum);
        }
        public SuperUser GetSuperUser(int su)
        {
            return (SuperUser)_cache.Get(su.ToString(), _SuperUser);
        }
        public User GetUser(int user, int forum)
        {
            return (User)_cache.Get(user.ToString(), _User + forum);
        }
        public Moderator GetModerator(int mod, int subforum)
        {
            return (Moderator)_cache.Get(mod.ToString(), _Moderator + subforum);
        }
        public Admin GetAdmin(int admin, int forum)
        {
            return (Admin)_cache.Get(admin.ToString(), _Admin + forum);
        }
    }
}
