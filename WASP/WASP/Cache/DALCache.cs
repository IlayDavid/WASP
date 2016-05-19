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
        private const string _User = "User";
        private const string _Moderator = "Moderator";
        private const string _Admin = "Admin";
        private const string _Post = "Post";

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



        public void AddForum(Forum f)
        {
            new CacheItemPolicy().SlidingExpiration = new TimeSpan(0, 1, 0);
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
            CacheItem us = new CacheItem(user.Id.ToString(), user, _User);
            _cache.Add(us, new CacheItemPolicy());
        }
        public void AddModerator(Moderator mod)
        {
            CacheItem moderator = new CacheItem(mod.Id.ToString(), mod, _Moderator);
            _cache.Add(moderator, new CacheItemPolicy());
        }
        public void AddAdmin(Admin admin)
        {
            CacheItem adm = new CacheItem(admin.Id.ToString(), admin, _Admin);
            _cache.Add(adm, new CacheItemPolicy());
        }
        public void AddPost(Post post)
        {
            CacheItem pst = new CacheItem(post.Id.ToString(), post, _Post);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = _postExpiration;
            _cache.Add(pst, policy);
        }

        public void RemoveForum(int id)
        {
            _cache.Remove(id.ToString(),_Forum);
        }
        public void RemoveSubforum(int id)
        {
            _cache.Remove(id.ToString(), _Subforum);
        }
        public void RemoveUser(int id)
        {
            _cache.Remove(id.ToString(), _User);
        }
        public void RemoveModerator(int id)
        {
            _cache.Remove(id.ToString(), _Moderator);
        }
        public void RemoveAdmin(int id)
        {
            _cache.Remove(id.ToString(), _Admin);
        }



    }
}
