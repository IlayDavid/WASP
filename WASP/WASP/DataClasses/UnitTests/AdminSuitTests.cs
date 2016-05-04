using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;

namespace WASP.TestSuits
{
    [TestClass]
    public class AdminSuitTests

    {
        private DAL dal = new DALSQL();
        private User user1;
        private User user2;
        int forumId;


        [TestCleanup]
        public void CleanUp()
        {
            ((DALSQL)dal).Clean();
        }
        [TestInitialize]
        public void SetUp()
        {
            ((DALSQL)dal).Clean();
            Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
            user1 = new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum);
            user2 = new User(205857121, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum);
            forumId = forum.Id;
        }



        [TestMethod]
        public void AddAdminTest1()
        {
            try
            {   
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));

                
                Admin admin = dal.GetAdmin(user1.Id, forumId);
                Assert.IsTrue(admin.Id == user1.Id);
                Assert.IsTrue(admin.Forum.Id == forumId);
                Assert.IsTrue(admin.InnerUser.Username == user1.Username);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddAdminTest2()
        {
            try
            {
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
                dal.CreateAdmin(new Admin(user2, dal.GetForum(forumId), dal));
                Admin admin1 = dal.GetAdmin(user1.Id,forumId);
                Admin admin2 = dal.GetAdmin(user2.Id, forumId);

                Assert.IsTrue(admin1.Id == user1.Id);
                Assert.IsTrue(admin1.Forum.Id == forumId);
                Assert.IsTrue(admin1.InnerUser.Username == user1.Username);

                Assert.IsTrue(admin2.Id == user2.Id);
                Assert.IsTrue(admin2.Forum.Id == forumId);
                Assert.IsTrue(admin2.InnerUser.Username == user2.Username);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddAdminTest3()
        {
            try
            {
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
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
        public void UpdateAdminTest4()
        {
            try
            {
                User user11 = new User(315470047, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", dal.GetForum(forumId));
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
                dal.UpdateAdmin(new Admin(user11, dal.GetForum(forumId), dal));
                Admin admin = dal.GetAdmin(user1.Id, forumId);
                Assert.IsTrue(admin.InnerUser.Name == user11.Name);
                Assert.IsTrue(admin.InnerUser.Username == user11.Username);
                Assert.IsTrue(admin.InnerUser.Email == user11.Email);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetAdminsTest5()
        {
            try
            {
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
                dal.CreateAdmin(new Admin(user2, dal.GetForum(forumId), dal));
                Admin[] admins = dal.GetAdmins(null, dal.GetForum(forumId));
                Assert.IsTrue(admins.Length == 2);
                Assert.IsTrue(admins[0].InnerUser.Username == user1.Username || admins[1].InnerUser.Username == user1.Username);
                Assert.IsTrue(admins[0].InnerUser.Username == user2.Username || admins[1].InnerUser.Username == user2.Username);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetAdminsTest6()
        {
            try
            {
                dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
                dal.CreateAdmin(new Admin(user2, dal.GetForum(forumId), dal));
                Admin[] admins = dal.GetAdmins(new Collection<int> { user1.Id }, dal.GetForum(forumId));
                Assert.IsTrue(admins.Length == 1);
                Assert.IsTrue(admins[0].InnerUser.Username == user1.Username);
                Assert.IsTrue(admins[0].InnerUser.Email == user1.Email);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteAdminTest7()
        {
            dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal));
            dal.CreateAdmin(new Admin(user2, dal.GetForum(forumId), dal));
            int adminId1 = dal.GetAdmin(user1.Id, forumId).Id;
            int adminId2 = dal.GetAdmin(user2.Id, forumId).Id;

            dal.DeleteAdmin(adminId1, forumId);
            Assert.IsTrue(dal.GetAdmins(null,null).Length == 1);
            dal.DeleteAdmin(adminId2, forumId);
            Assert.IsTrue(dal.GetAdmins(null, null).Length == 0);
        }
    }
}
