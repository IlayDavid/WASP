
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class UserSuitTests
    {
        private DAL dal = new DALSQL();
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
            forumId = forum.Id;
        }

        [TestMethod]
        public void AddUserTest1()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue((user.Email).Equals("matansar@post.bgu.ac.il"));
                Assert.IsTrue((user.Username).Equals("matansar"));
                Assert.IsTrue((user.Password).Equals("123"));
                Assert.IsTrue((user.Name).Equals("matan"));

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddUserTest2()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                User user1 = dal.GetUser(315470047, forumId);
                User user2 = dal.GetUser(315470048, forumId);
                Assert.IsTrue(user1.Id == 315470047);
                Assert.IsTrue(user2.Id == 315470048);
                Assert.IsTrue(user1.Username.Equals("matansar"));
                Assert.IsTrue(user2.Username.Equals("matansar2"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddUserTest3()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                dal.CreateUser(new User(315470047, "matan1", "matans3ar", "matansar@post4.bgu.ac.il", "1235", dal.GetForum(forumId)));
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
        public void UpdateUserTest4()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                dal.updateUser(new User(315470047, "moshe", "moshesar", "moshesar@post.bgu.ac.il", "456", dal.GetForum(forumId)));
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue(user.Email.Equals("moshesar@post.bgu.ac.il"));
                Assert.IsTrue(user.Username.Equals("moshesar"));
                Assert.IsTrue(user.Password.Equals("456"));
                Assert.IsTrue(user.Name.Equals("moshe"));

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetUsersTest5()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                User[] users = dal.GetUseres(null, null);
                Assert.IsTrue(users.Length == 2);
                Assert.IsTrue(users[0].Username.Equals("matansar" ) || users[1].Username.Equals("matansar"));
                Assert.IsTrue(users[0].Username.Equals("matansar2") || users[1].Username.Equals("matansar2"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetUsersTest6()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId)));
                User[] users = dal.GetUseres(new int [] { 315470047 }, null);
                Assert.IsTrue(users.Length == 1);
                Assert.IsTrue(users[0].Username.Equals("matansar"));
                Assert.IsTrue(users[0].Email.Equals("matansar@post.bgu.ac.il"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteUserTest7()
        {
            dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId)));
            dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId)));
            int userId1 = dal.GetUser(315470047, forumId).Id;
            int userId2 = dal.GetUser(315470048, forumId).Id;

            dal.DeleteUser(userId1, forumId);
            Assert.IsTrue(dal.GetUseres(null,null).Length == 1);
            dal.DeleteUser(userId2, forumId);
            Assert.IsTrue(dal.GetUseres(null, null).Length == 0);
        }

    }


}