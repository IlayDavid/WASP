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
        
        
        //delete, get for posts


        private TimeSpan _postExpiration = new TimeSpan(0, 1, 0);
        public void setPostExpiration(int hours, int minutes, int secs)
        {
            _postExpiration = new TimeSpan(hours, minutes, secs);
        }

        private ObjectCache _cache;
        private DAL2 _dal;
        public DALCache(DAL2 dal)
        {
            _cache = MemoryCache.Default;
            _dal = dal;         
        }


        public void AddSuperUser(SuperUser su)
        {
            CacheItem forum = new CacheItem(_SuperUser + su.Id.ToString(), su);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddForum(Forum f)
        {
            CacheItem forum = new CacheItem(_Forum + f.Id.ToString(), f);
            _cache.Add(forum, new CacheItemPolicy());
        }
        public void AddSubforum(Subforum sf)
        {
            CacheItem sforum = new CacheItem(_Subforum + sf.Id.ToString(), sf);
            _cache.Add(sforum, new CacheItemPolicy());
        }
        public void AddUser(User user)
        {
            CacheItem us = new CacheItem(_User + user.Id.ToString() + _Forum + user.Forum.Id, user);
            _cache.Add(us, new CacheItemPolicy());
        }
        public void AddModerator(Moderator mod)
        {
            CacheItem moderator = new CacheItem(_Moderator + mod.Id + _Subforum + mod.SubForum.Id, mod);
            _cache.Add(moderator, new CacheItemPolicy());
        }
        public void AddAdmin(Admin admin)
        {
            CacheItem adm = new CacheItem(_Admin + admin.Id.ToString()+ _Forum + admin.Forum.Id, admin);
            _cache.Add(adm, new CacheItemPolicy());
        }
        public void AddPost(Post post)
        {
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
            if (forum != null)
                return (Forum)forum;
            AddForum(_dal.GetForum(f));
            return (Forum)_cache.Get(_Forum + f.ToString());
        }
        public Subforum GetSubforum(int sf)
        {
            object subforum = _cache.Get(_Subforum + sf.ToString());
            if (subforum != null)
                return (Subforum)subforum;
            AddSubforum(_dal.GetSubForum(sf));
            return (Subforum)_cache.Get(_Subforum + sf.ToString()); 
        }
        public SuperUser GetSuperUser(int su)
        {
            object superuser = _cache.Get(_SuperUser + su.ToString());
            if (superuser != null)
                return (SuperUser)superuser;
            AddSuperUser(_dal.GetSuperUser(su));
            return (SuperUser)_cache.Get(_SuperUser + su.ToString());
            
        }
        public User GetUser(int user, int forum)
        {
            object userobj = _cache.Get(_User + user + _Forum + forum);
            if (userobj != null)
                return (User)userobj;
            AddUser(_dal.GetUser(user, forum));
            return (User)_cache.Get(_User + user + _Forum + forum);
        }
        public Moderator GetModerator(int mod, int subforum)
        {
            object moderator = _cache.Get(_Moderator + mod + _Subforum + subforum);
            if (moderator != null)
                return (Moderator)moderator;
            AddModerator(_dal.GetModerator(mod, subforum));
            return (Moderator)_cache.Get(_Moderator + mod + _Subforum + subforum);

        }
        public Admin GetAdmin(int admin, int forum)
        {
            object adm = _cache.Get(_Admin + admin + _Forum + forum);
            if (adm != null)
                return (Admin)adm;
            AddAdmin(_dal.GetAdmin(admin, forum));
            return (Admin)_cache.Get(_Admin + admin + _Forum + forum);
        }
        public Post GetPost(int p)
        {
            object post = _cache.Get(_Post + p.ToString());
            if (post != null)
                return (Post)post;
            AddPost(_dal.GetPost(p));
            return (Post)_cache.Get(_Post + p.ToString());
        }
    }
}
