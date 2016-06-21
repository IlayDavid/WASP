using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.Cache;
using WASP.DataClasses.DAL_EXCEPTIONS;

namespace WASP.DataClasses.Cache2.UnitTests
{
    [TestClass]
    public class CahcheTests
    {
        private IDALCache _cache;

        [TestInitialize]
        public void SetUp()
        {
            _cache = new DALCache(new DALSQL());
        }

        [TestMethod]
        public void CacheTest1()
        {
            Forum forum = new Forum(1,"forum", "forum", null, null);
            User user = new User(123, "blah", "moshe", "asdasd@walla.com", "123", forum);
            _cache.AddUser(user);
            Assert.IsNotNull(_cache.GetUser(user.Id, forum.Id));
            Assert.IsTrue(_cache.GetUser(user.Id, forum.Id).Id == 123);
        }

        [TestMethod]
        public void CacheTest2()
        {
            Forum forum = new Forum(1, "forum", "forum", null, null);
            User user = new User(123, "blah", "moshe", "asdasd@walla.com", "123", forum);
            Admin admin = new Admin(user, forum);
            _cache.AddAdmin(admin);
            Assert.IsNotNull(_cache.GetAdmin(admin.Id, forum.Id));
            Assert.IsTrue(_cache.GetAdmin(admin.Id, forum.Id).Id == 123);
        }

        [TestMethod]
        public void CacheTest3()
        {
            Forum forum = new Forum(1, "forum", "forum", null, null);
            User user = new User(123, "blah", "moshe", "asdasd@walla.com", "123", forum);
            Subforum subforum = new Subforum(11, "asd", "asd", forum);
            Moderator mod = new Moderator(user, DateTime.Now, subforum, null);
            _cache.AddModerator(mod);
            Assert.IsNotNull(_cache.GetModerator(mod.Id, subforum.Id));
            Assert.IsTrue(_cache.GetModerator(mod.Id, subforum.Id).Id == 123);
        }

        [TestMethod]
        public void CacheTest4()
        {
            SuperUser sp = new SuperUser(1231, "asdasd", "asd");
            _cache.AddSuperUser(sp);
            Assert.IsNotNull(_cache.GetSuperUser(sp.Id));
            Assert.IsTrue(_cache.GetSuperUser(sp.Id).Id == 1231);
        }
        [TestMethod]
        public void CacheTest5()
        {
            Forum forum = new Forum(1, "forum", "forum", null, null);
            _cache.AddForum(forum);
            Assert.IsNotNull(_cache.GetForum(forum.Id));
            Assert.IsTrue(_cache.GetForum(forum.Id).Id == 1);
        }

        [TestMethod]
        public void CacheTest6()
        {
            Forum forum = new Forum(1, "forum", "forum", null, null);
            Subforum subforum = new Subforum(11, "asd", "asd", forum);
            _cache.AddSubforum(subforum);
            Assert.IsNotNull(_cache.GetSubforum(subforum.Id));
            Assert.IsTrue(_cache.GetSubforum(subforum.Id).Id == 11);
        }

        [TestMethod]
        public void CacheTest7()
        {
            Forum forum = new Forum(1, "forum", "forum", null, null);
            Subforum subforum = new Subforum(11, "asd", "asd", forum);
            Post post = new Post(1, "asd", "asd", null, DateTime.Now, null, subforum, DateTime.Now);
            _cache.AddPost(post);
            Assert.IsNotNull(_cache.GetPost(post.Id));
            Assert.IsTrue(_cache.GetPost(post.Id).Id == 1);
        }

    }
}