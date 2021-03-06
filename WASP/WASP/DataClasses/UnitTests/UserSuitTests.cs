﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WASP.DataClasses.DAL_EXCEPTIONS;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class UserSuitTests
    {
        private DAL2 dal = new DALSQL();
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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
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
        public void AddUserTest1_1()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
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
        public void AddUserTest1_2()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), DateTime.Now.AddDays(11), DateTime.Now.AddDays(9)));
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue((user.Email).Equals("matansar@post.bgu.ac.il"));
                Assert.IsTrue((user.Username).Equals("matansar"));
                Assert.IsTrue((user.Password).Equals("123"));
                Assert.IsTrue((user.Name).Equals("matan"));
                Assert.IsTrue(user.StartDate.Date == DateTime.Now.AddDays(11).Date);
                Assert.IsTrue(user.PasswordChangeDate.Date == DateTime.Now.AddDays(9).Date);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void AddUserTest1_3()
        {
            try
            {
                string [] answers = { "Hi", "Bye" };
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), DateTime.Now.AddDays(11), DateTime.Now.AddDays(9),answers, false));
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue((user.Email).Equals("matansar@post.bgu.ac.il"));
                Assert.IsTrue((user.Username).Equals("matansar"));
                Assert.IsTrue((user.Password).Equals("123"));
                Assert.IsTrue((user.Name).Equals("matan"));
                Assert.IsTrue(user.StartDate.Date == DateTime.Now.AddDays(11).Date);
                Assert.IsTrue(user.PasswordChangeDate.Date == DateTime.Now.AddDays(9).Date);
                Assert.IsTrue(user.Answers[0].Equals(answers[0]));
                Assert.IsTrue(user.Answers[1].Equals(answers[1]));
                Assert.IsTrue(user.WantNotifications == false);
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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.CreateUser(new User(315470047, "matan1", "matans3ar", "matansar@post4.bgu.ac.il", "1235", dal.GetForum(forumId), dal));
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
        public void UpdateUserTest4_1()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.UpdateUser(new User(315470047, "moshe", "moshesar", "moshesar@post.bgu.ac.il", "456", dal.GetForum(forumId), dal));
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
        public void UpdateUserTest4_2()
        {
            try
            {
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), DateTime.Now.AddDays(11), DateTime.Now.AddDays(9)));
                User user = dal.UpdateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), DateTime.Now.AddDays(131), DateTime.Now.AddDays(39)));

                Assert.IsTrue(user.StartDate.Date == DateTime.Now.AddDays(131).Date);
                Assert.IsTrue(user.PasswordChangeDate.Date == DateTime.Now.AddDays(39).Date);

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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.UpdateUser(new User(315470047, "moshe", "moshesar", "moshesar@post.bgu.ac.il", "456", dal.GetForum(forumId), dal));
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
        public void UpdateUserTest4_3()
        {
            try
            {
                string[] sheet = { "", "" };
                string[] answers = { "Hi", "Bye" };
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId),DateTime.Now, DateTime.Now, sheet, false));
                dal.UpdateUser(new User(315470047, "moshe", "moshesar", "moshesar@post.bgu.ac.il", "456", dal.GetForum(forumId), DateTime.Now, DateTime.Now, answers, true));
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue(user.Email.Equals("moshesar@post.bgu.ac.il"));
                Assert.IsTrue(user.Username.Equals("moshesar"));
                Assert.IsTrue(user.Password.Equals("456"));
                Assert.IsTrue(user.Name.Equals("moshe"));
                Assert.IsTrue(user.Answers[0].Equals(answers[0]));
                Assert.IsTrue(user.Answers[1].Equals(answers[1]));
                Assert.IsTrue(user.WantNotifications == true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateUserTest4_4()
        {
            try
            {
                string[] sheet = { "", "" };
                string[] answers = { "Hi", "Bye" };
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), DateTime.Now, DateTime.Now, sheet, false));
                User us = new User(315470047, "moshe", "moshesar", "moshesar@post.bgu.ac.il", "456", dal.GetForum(forumId), DateTime.Now, DateTime.Now, answers, true);
                us.Secret = "moshe";
                us.OnlineCount = 4;
                dal.UpdateUser(us);
                User user = dal.GetUser(315470047, forumId);
                Assert.IsTrue(user.Email.Equals("moshesar@post.bgu.ac.il"));
                Assert.IsTrue(user.Username.Equals("moshesar"));
                Assert.IsTrue(user.Password.Equals("456"));
                Assert.IsTrue(user.Name.Equals("moshe"));
                Assert.IsTrue(user.Answers[0].Equals(answers[0]));
                Assert.IsTrue(user.Answers[1].Equals(answers[1]));
                Assert.IsTrue(user.WantNotifications == true);

                Assert.IsTrue(user.Secret.Equals("moshe"));
                Assert.IsTrue(user.OnlineCount == 4);
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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                User[] users = dal.GetUsers(null, forumId);
                Assert.IsTrue(users.Length == 2);
                Assert.IsTrue(users[0].Username.Equals("matansar") || users[1].Username.Equals("matansar"));
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
                dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
                User[] users = dal.GetUsers(new int[] { 315470047 }, forumId);
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
            dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            int userId1 = dal.GetUser(315470047, forumId).Id;
            int userId2 = dal.GetUser(315470048, forumId).Id;

            dal.DeleteUser(userId1, forumId);
            Assert.IsTrue(dal.GetUsers(null, forumId).Length == 1);
            dal.DeleteUser(userId2, forumId);
            Assert.IsTrue(dal.GetUsers(null, forumId).Length == 0);
        }
        [TestMethod]
        public void UserPostTest8()
        {
            User user1 = dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            User user2 = dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            Subforum subforum1 = dal.CreateSubForum(new Subforum(-1, "calandar", "blah", dal.GetForum(forumId), dal));

            dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, subforum1, DateTime.Now, dal));
            dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, subforum1, DateTime.Now, dal));
            dal.CreatePost(new Post(-1, "question", "blah", user2, DateTime.Now, null, subforum1, DateTime.Now, dal));
            dal.CreatePost(new Post(-1, "question", "blah", user2, DateTime.Now, null, subforum1, DateTime.Now, dal));


            Assert.IsTrue(dal.GetUserPosts(315470047, forumId).Length == 2);
            Assert.IsTrue(dal.GetUserPosts(315470048, forumId).Length == 2);
        }

        [TestMethod]
        public void UseNewrNotificationsTest9()
        {
            User user1 = dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            User user2 = dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateNotification(new Notification(-1, "hi", true, user1, user2,Notification.Types.Message, DateTime.Now));
            dal.CreateNotification(new Notification(-1, "hi", false, user1, user2, Notification.Types.Message, DateTime.Now));
            dal.CreateNotification(new Notification(-1, "hi", true, user2, user1, Notification.Types.Message, DateTime.Now));
            dal.CreateNotification(new Notification(-1, "hi", true, user2, user1, Notification.Types.Message, DateTime.Now));

            Assert.IsTrue(dal.GetUserNewNotifications(user1.Id).Length == 2);
            Assert.IsTrue(dal.GetUserNewNotifications(user2.Id).Length == 1);
        }

        [TestMethod]
        public void UserNotificationsTest10()
        {
            User user1 = dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            User user2 = dal.CreateUser(new User(315470048, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateNotification(new Notification(-1, "hi", true, user1, user2, Notification.Types.Message, DateTime.Now));
            dal.CreateNotification(new Notification(-1, "hi", false, user1, user2, Notification.Types.Message, DateTime.Now));
            dal.CreateNotification(new Notification(-1, "hi", true, user2, user1, Notification.Types.Message, DateTime.Now));

            Assert.IsTrue(dal.GetUserNotifications(user1.Id).Length == 1);
            Assert.IsTrue(dal.GetUserNotifications(user2.Id).Length == 2);
        }

        [TestMethod]
        public void UserDiffTest11()
        {
            Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
            dal.CreateUser(new User(315470040, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470041, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470041, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum, dal));
            dal.CreateUser(new User(315470047, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470046, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum, dal));
            dal.CreateUser(new User(315470048, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470042, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            dal.CreateUser(new User(315470042, "matan", "matansar", "matansar@post.bgu.ac.il", "123", forum, dal));
            dal.CreateUser(new User(315470047, "matan", "matansar2", "matansar2@post.bgu.ac.il", "123", forum, dal));

            User[] users = dal.GetUsersInDiffForums();
            Assert.IsTrue(users.Length == 6, "0- " + users.Length);

            Assert.IsTrue(users.ToList().Where(x => x.Id == 315470041).Count() == 2, "1- " + users.ToList().Where(x => x.Id == 315470041).Count());
            Assert.IsTrue(users.ToList().Where(x => x.Id == 315470042).Count() == 2);
            Assert.IsTrue(users.ToList().Where(x => x.Id == 315470047).Count() == 2);
            Assert.IsTrue(users.ToList().Where(x => x.Id == 315470046).Count() == 0);
            Assert.IsTrue(users.ToList().Where(x => x.Id == 315470048).Count() == 0);

        }
        [TestMethod]
        public void UserAddFriendTest12()
        {
            Forum forum = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
            User user1 = dal.CreateUser(new User(111111, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));
            User user2 = dal.CreateUser(new User(222222, "matan", "matansar", "matansar@post.bgu.ac.il", "123", dal.GetForum(forumId), dal));

            dal.AddFriend(user1, user2);
            User[] users = dal.GetUserFriends(user1.Id, user1.Forum.Id);
            Assert.IsTrue(users.Length == 1);
            Assert.IsTrue(users[0].Id == user2.Id);
        }

    }


}