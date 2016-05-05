using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;

namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class NotificSuitTests

    {
        private DAL dal = new DALSQL();
        private User user1;
        private User user2;
        int userId1;
        int userId2;
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
            user1 = new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum);
            user2 = new User(205857121, "amitay", "shaera", "shaera@post.bgu.ac.il", "123", forum);
            userId1 = dal.CreateAdmin(new Admin(user1, dal.GetForum(forumId), dal)).Id;
            userId2 = dal.CreateAdmin(new Admin(user2, dal.GetForum(forumId), dal)).Id;
            
        }



        [TestMethod]
        public void AddNotificationTest1()
        {
            try
            {
                Notification not1 = dal.CreateNotification(new Notification(-1, "hi",true, dal.GetUser(userId1, forumId), dal.GetUser(userId2,forumId)));
                Notification not2 = dal.CreateNotification(new Notification(-1, "hi", false, dal.GetUser(userId2, forumId), dal.GetUser(userId1, forumId)));
                Assert.IsTrue(not1.Message  != null);
               
                Assert.IsTrue(not1.Message.Equals("hi"));
                Assert.IsTrue(not2.Message.Equals("hi"));
                Assert.IsTrue(not1.IsNew == true);
                Assert.IsTrue(not2.IsNew == false);
                Assert.IsTrue(not1.Source.Id == user1.Id);
                Assert.IsTrue(not2.Source.Id == user2.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateNotificationTest2()
        {
            try
            {
                Notification not1 = dal.CreateNotification(new Notification(-1, "hi", true, dal.GetUser(userId1, forumId), dal.GetUser(userId2, forumId)));
                not1 = dal.UpdateNotification(new Notification(not1.Id, "balh", false, dal.GetUser(userId1, forumId), dal.GetUser(userId2, forumId)));
                Assert.IsTrue(not1.Message.Equals("balh"));
                Assert.IsTrue(not1.IsNew == false);
                Assert.IsTrue(not1.Source.Id == user1.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetNotificationsTest3()
        {
            try
            {
                Notification not1 = dal.CreateNotification(new Notification(-1, "hi", true, dal.GetUser(userId1, forumId), dal.GetUser(userId2, forumId)));
                Notification not2 = dal.CreateNotification(new Notification(-1, "bo", false, dal.GetUser(userId2, forumId), dal.GetUser(userId1, forumId)));

                Notification[] notifications = dal.GetNotifications(null);
                Assert.IsTrue(notifications.Length == 2);

                Assert.IsTrue(notifications[0].Message.Equals("bo") || notifications[1].Message.Equals("bo"));
                Assert.IsTrue(notifications[0].Message.Equals("hi") || notifications[1].Message.Equals("hi"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetNotificationsTest4()
        {
            try
            {
                Notification not1 = dal.CreateNotification(new Notification(-1, "hi", true, dal.GetUser(userId1, forumId), dal.GetUser(userId2, forumId)));
                Notification not2 = dal.CreateNotification(new Notification(-1, "bo", false, dal.GetUser(userId2, forumId), dal.GetUser(userId1, forumId)));

                Notification[] notifications = dal.GetNotifications(new int[] { not1.Id});
                Assert.IsTrue(notifications.Length == 1);
                Assert.IsTrue(notifications[0].Id == not1.Id);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteNotificationTest5()
        {
            Notification not1 = dal.CreateNotification(new Notification(-1, "hi", true, dal.GetUser(userId1, forumId), dal.GetUser(userId2, forumId)));
            Notification not2 = dal.CreateNotification(new Notification(-1, "bo", false, dal.GetUser(userId2, forumId), dal.GetUser(userId1, forumId)));


            dal.DeleteNotification(not1.Id);
            Assert.IsTrue(dal.GetNotifications(null).Length == 1);
            dal.DeleteNotification(not2.Id);
            Assert.IsTrue(dal.GetNotifications(null).Length == 0);
        }
    }
}
