using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;
namespace WASP.TestSuits
{
    [TestClass]
    public class ModeratorSuitTest

    {
        private DAL dal = new DALSQL();
        private User user1;
        private User user2;
        private User userAdmin;
        int adminId;
        int subforumId;
        int forumId;


        [TestCleanup]
        public void CleanUp()
        {
            ((DALSQL)dal).Clean();
            DALSQL.GetBackUp();
        }
        [TestInitialize]
        public void SetUp()
        {
            DALSQL.BackUpAll();
            ((DALSQL)dal).Clean();
            Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));

            user1 = new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum);
            user2 = new User(205857121, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum);
            userAdmin = new User(111111111, "admin", "admina", "admina@post.bgu.ac.il", "123", forum);

            Subforum subf = dal.CreateSubForum(new Subforum(-1, "calander", "Blah", forum, dal));
            Admin admin = dal.CreateAdmin(new Admin(userAdmin, forum, dal));

            forumId = forum.Id;
            subforumId = subf.Id;
            adminId = admin.Id;
        }



        [TestMethod]
        public void AddModeratorTest1()
        {
            try
            {
                
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId), 
                    dal.GetAdmin(adminId,forumId) , dal));
                Moderator mod = dal.GetModerator(user1.Id, subforumId);
                Assert.IsTrue(mod.Id == user1.Id);
                Assert.IsTrue(mod.Appointer.Id == adminId);
                Assert.IsTrue(mod.TermExp.Date == DateTime.Now.AddDays(10).Date);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddModeratorTest2()
        {
            try
            {

                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator mod1 = dal.GetModerator(user1.Id, subforumId);
                Moderator mod2 = dal.GetModerator(user2.Id, subforumId);
                Assert.IsTrue(mod1.Id == user1.Id);
                Assert.IsTrue(mod1.Appointer.Id == adminId);
                Assert.IsTrue(mod1.TermExp.Date == DateTime.Now.AddDays(10).Date);
                Assert.IsTrue(mod2.Id == user2.Id);
                Assert.IsTrue(mod2.Appointer.Id == adminId);
                Assert.IsTrue(mod2.TermExp.Date == DateTime.Now.AddDays(20).Date);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddModeratorTest3()
        {
            try
            {
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Assert.Fail();
            }
            catch (ExistException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateModeratorTest4()
        {
            try
            {
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.updateModerator(new Moderator(user1, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator mod = dal.GetModerator(user1.Id, subforumId);
                Assert.IsTrue(mod.TermExp.Date == DateTime.Now.AddDays(20).Date);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetModeratorsTest5()
        {
            try
            {
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator[] mods = dal.GetModerators(null,dal.GetSubForum(subforumId));
                
                Assert.IsTrue(mods.Length == 2);
                Assert.IsTrue(mods[0].Id == user1.Id || mods[1].Id == user1.Id);
                Assert.IsTrue(mods[0].Id == user2.Id || mods[1].Id == user2.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetModeratorsTest6()
        {
            try
            {
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator[] mods = dal.GetModerators(new int [] { user1.Id }, dal.GetSubForum(subforumId));
                Assert.IsTrue(mods.Length == 1);
                Assert.IsTrue(mods[0].Id == user1.Id);
                
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteModeratorTest7()
        {
            Moderator mod1 = dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                dal.GetAdmin(adminId, forumId), dal));
            Moderator mod2 = dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                dal.GetAdmin(adminId, forumId), dal));

            dal.DeleteModerator(mod1.Id, subforumId);
            Assert.IsTrue(dal.GetModerators(null, null).Length == 1);
            dal.DeleteModerator(mod2.Id, subforumId);
            Assert.IsTrue(dal.GetModerators(null, null).Length == 0);
        }
        [TestMethod]
        public void CreateModeratorTest8()
        {
            try
            {
                Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
                Subforum subf = dal.CreateSubForum(new Subforum(-1, "calander", "Blah", forum, dal));
                User user3 = new User(205857221, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum);
                Admin admin = dal.CreateAdmin(new Admin(user3, forum, dal));


                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    admin, dal));
                Assert.Fail();
            }
            catch (InvalidException)
            {
                    Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
