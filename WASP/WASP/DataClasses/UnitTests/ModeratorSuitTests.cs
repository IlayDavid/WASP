﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class ModeratorSuitTest

    {
        private DAL2 dal = new DALSQL();
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

            user1 = new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum, dal);
            user2 = new User(205857121, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
            userAdmin = new User(111111111, "admin", "admina", "admina@post.bgu.ac.il", "123", forum, dal);

            Subforum subf = dal.CreateSubForum(new Subforum(-1, "calander", "Blah", forum, dal));
            dal.CreateUser(userAdmin);
            Admin admin = dal.CreateAdmin(new Admin(userAdmin, forum, dal));

            forumId = forum.Id;
            subforumId = subf.Id;
            adminId = admin.Id;
        }
        
        [TestMethod]
        public void AddModeratorTest1_1()
        {
            try
            {
                Subforum subforum = dal.GetSubForum(subforumId);
                Admin admin = dal.GetAdmin(adminId, forumId);
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), subforum,
                    admin, dal));
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
        public void AddModeratorTest1_2()
        {
            try
            {
                Subforum subforum = dal.GetSubForum(subforumId);
                Admin admin = dal.GetAdmin(adminId, forumId);
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), subforum,
                    admin, DateTime.Now.AddDays(11)));
                Moderator mod = dal.GetModerator(user1.Id, subforumId);
                Assert.IsTrue(mod.Id == user1.Id);
                Assert.IsTrue(mod.Appointer.Id == adminId);
                Assert.IsTrue(mod.TermExp.Date == DateTime.Now.AddDays(10).Date);
                Assert.IsTrue(mod.StartDate.Date == DateTime.Now.AddDays(11).Date);

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

                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateUser(user2);
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
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateUser(user2);
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
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.UpdateModerator(new Moderator(user1, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
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
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateUser(user2);
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator[] mods = dal.GetModerators(null, dal.GetSubForum(subforumId));

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
                dal.CreateUser(user1);
                dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                dal.CreateUser(user2);
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(20), dal.GetSubForum(subforumId),
                    dal.GetAdmin(adminId, forumId), dal));
                Moderator[] mods = dal.GetModerators(new int[] { user1.Id }, dal.GetSubForum(subforumId));
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
            dal.CreateUser(user1);
            Moderator mod1 = dal.CreateModerator(new Moderator(user1, DateTime.Now.AddDays(10), dal.GetSubForum(subforumId),
                dal.GetAdmin(adminId, forumId), dal));
            dal.CreateUser(user2);
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
                User user3 = new User(205857221, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
                dal.CreateUser(user3);
                Admin admin = dal.CreateAdmin(new Admin(user3, forum, dal));

                dal.CreateUser(user1);
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

        [TestMethod]
        public void ModeratorSubForumTest9()
        {
            try
            {
                Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
                Subforum subf = dal.CreateSubForum(new Subforum(-1, "calander", "Blah", forum, dal));
                User user3 = new User(205857221, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
                User user2 = new User(205857211, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
                dal.CreateUser(user3);
                Admin admin = dal.CreateAdmin(new Admin(user3, forum, dal));
                dal.CreateUser(user2);
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(10), subf, admin, dal));
                Assert.IsTrue(dal.GetModeratorSubForum(user2.Id, forum.Id).Id == subf.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        [TestMethod]
        public void ModeratorAdminTest10()
        {
            try
            {
                Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
                Subforum subf = dal.CreateSubForum(new Subforum(-1, "calander", "Blah", forum, dal));
                User user3 = new User(205857221, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
                User user2 = new User(205857211, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum, dal);
                dal.CreateUser(user3);
                Admin admin = dal.CreateAdmin(new Admin(user3, forum, dal));
                dal.CreateUser(user2);
                dal.CreateModerator(new Moderator(user2, DateTime.Now.AddDays(10), subf, admin, dal));
                Assert.IsTrue(dal.GetModeratorAppointerAdmin(user2.Id, subf.Id).Id == admin.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
