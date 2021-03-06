﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class PostSuitTests

    {
        private DAL2 dal = new DALSQL();
        private User user1;
        private User user2;
        private User userAdmin;
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

            dal.CreateUser(user1);
            dal.CreateUser(user2);

            forumId = forum.Id;
            subforumId = subf.Id;
        }



        [TestMethod]
        public void AddPostTest1()
        {
            try
            {
                Subforum subforum = dal.GetSubForum(subforumId);
                int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, subforum, DateTime.Now, dal)).Id;
                Assert.IsTrue(threadId > 0);
                Post thread = dal.GetPost(threadId);
                Assert.IsTrue(thread.Title.Equals("question"));
                Assert.IsTrue(thread.Content.Equals("blah"));
                Assert.IsTrue(thread.PublishedAt.Date == DateTime.Now.Date);
                Assert.IsTrue(thread.EditAt.Date == DateTime.Now.Date);
                Assert.IsTrue(thread.GetAuthor.Id == user1.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddPostTest2()
        {
            try
            {
                int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                Assert.IsTrue(threadId > 0);
                Post thread = dal.GetPost(threadId);
                int replyId = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                replyId = dal.UpdatePost(new Post(replyId, "answer", "blah", user1, DateTime.Now.AddDays(1), dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                Assert.IsTrue(replyId > 0);
                Assert.IsTrue(dal.GetPost(replyId).InReplyTo.Id == threadId);
                Assert.IsTrue(dal.GetPost(replyId).PublishedAt.Date == DateTime.Now.AddDays(1).Date);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdatePostTest3()
        {
            try
            {
                int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                dal.UpdatePost(new Post(threadId, "question1", "blah1", user2, DateTime.Now.AddDays(10), null, dal.GetSubForum(subforumId), DateTime.Now.AddDays(11), dal));
                Assert.IsTrue(threadId > 0);
                Post thread = dal.GetPost(threadId);
                Assert.IsTrue(thread.Title.Equals("question1"));
                Assert.IsTrue(thread.Content.Equals("blah1"));
                Assert.IsTrue(thread.PublishedAt.Date == DateTime.Now.AddDays(10).Date);
                Assert.IsTrue(thread.EditAt.Date == DateTime.Now.AddDays(11).Date);
                Assert.IsTrue(thread.GetAuthor.Id == user2.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetPostsTest4()
        {
            try
            {
                int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                int replyId = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                Post[] posts = dal.GetPosts(null);
                Assert.IsTrue(posts.Length == 2);
                Assert.IsTrue(posts[0].Id == threadId || posts[1].Id == threadId);
                Assert.IsTrue(posts[0].Id == replyId || posts[1].Id == replyId);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetPostsTest5()
        {
            try
            {
                int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                int replyId = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
                Post[] posts = dal.GetPosts(new int[] { replyId });

                Assert.IsTrue(posts.Length == 1);
                Assert.IsTrue(posts[0].Id == replyId);
            }

            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeletePostTest6()
        {
            int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;


            dal.DeletePost(replyId);
            Assert.IsTrue(dal.GetPosts(null).Length == 1);
            dal.DeletePost(threadId);
            Assert.IsTrue(dal.GetPosts(null).Length == 0);
        }

        [TestMethod]
        public void DeletePostTest7()
        {
            int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId1 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId2 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(replyId1), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;

            dal.DeletePost(threadId);
            Assert.IsTrue(dal.GetPosts(null).Length == 0);
        }

        [TestMethod]
        public void PostRepliesTest8()
        {
            int threadId = dal.CreatePost(new Post(-1, "question", "blah", user1, DateTime.Now, null, dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId1 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId3 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(threadId), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId2 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(replyId1), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;
            int replyId4 = dal.CreatePost(new Post(-1, "answer", "blah", user1, DateTime.Now, dal.GetPost(replyId3), dal.GetSubForum(subforumId), DateTime.Now, dal)).Id;

            Assert.IsTrue(replyId4 == dal.GetPost(replyId4).Id);
            Assert.IsTrue(replyId1 == dal.GetPost(replyId1).Id);
            Assert.IsTrue(dal.GetReplies(threadId).Length == 2);
            Assert.IsTrue(dal.GetReplies(replyId1).Length == 1);
        }
        
    }
}
