﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class ForumSuitTests

    {
        private DAL dal = new DALSQL();
        

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
        }


        [TestMethod]
        public void AddForumTest1()
        {
            try
            {
                Forum newForum = new Forum(-1, "Start-Up", "blah", null, dal);
                int forumId = dal.CreateForum(newForum).Id;
                Assert.IsTrue(forumId > 0);
                Forum forum = dal.GetForum(forumId);
                Assert.IsTrue(forum.Name == newForum.Name);
                Assert.IsTrue(forum.Description == newForum.Description);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddForumTest2()
        {
            try
            {
                Forum forum1 = new Forum(-1, "Start-Up1", "blah", null, dal);
                Forum forum2 = new Forum(-1, "Start-Up2", "blah", null, dal);
                int forumId1 = dal.CreateForum(forum1).Id;
                int forumId2 = dal.CreateForum(forum2).Id;

                Assert.IsTrue(forumId1 > 0);
                Assert.IsTrue(forumId2 > 0);

                Forum forum = dal.GetForum(forumId1);
                Assert.IsTrue(forum.Name == forum1.Name);
                Assert.IsTrue(forum.Description == forum1.Description);

                forum = dal.GetForum(forumId2);
                Assert.IsTrue(forum.Name == forum2.Name);
                Assert.IsTrue(forum.Description == forum2.Description);


            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateForumTest3()
        {
            try
            {
                Forum forum = new Forum(-1, "Start-Up", "blah", null, dal);
                int forumId = dal.CreateForum(forum).Id;
                dal.UpdateForum(new Forum(forumId, "Up-start", "blah blah", null, dal));
                Forum updatedForum = dal.GetForum(forumId);
                Assert.IsTrue((updatedForum.Name).Equals("Up-start"));
                Assert.IsTrue((updatedForum.Description).Equals("blah blah"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetForumsTest4()
        {
            try
            {
                Forum forum1 = new Forum(-1, "Start-Up1", "blah", null, dal);
                Forum forum2 = new Forum(-1, "Start-Up2", "blah", null, dal);
                int forumId1 = dal.CreateForum(forum1).Id;
                int forumId2 = dal.CreateForum(forum2).Id;

                Forum[] forums = dal.GetForums(null);
                Assert.IsTrue(forums.Length == 2);
                Assert.IsTrue(forums[0].Name == forum1.Name|| forums[1].Name == forum1.Name);
                Assert.IsTrue(forums[0].Name == forum2.Name || forums[1].Name == forum2.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetForumsTest5()
        {
            try
            {
                Forum forum1 = new Forum(-1, "Start-Up1", "blah", null, dal);
                Forum forum2 = new Forum(-1, "Start-Up2", "blah", null, dal);
                int forumId1 = dal.CreateForum(forum1).Id;
                int forumId2 = dal.CreateForum(forum2).Id;

                Forum[] forums = dal.GetForums(new int [] { forumId1 });
                Assert.IsTrue(forums.Length == 1);
                Assert.IsTrue(forums[0].Name == forum1.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteForumTest6()
        {
            Forum forum1 = dal.CreateForum(new Forum(-1, "Start-Up1", "blah", null, dal));
            Forum forum2 = dal.CreateForum(new Forum(-1, "Start-Up2", "blah", null, dal));
            dal.DeleteForum(forum1.Id);
            Assert.IsTrue(dal.GetForums(null).Length == 1);
            dal.DeleteForum(forum2.Id);
            Assert.IsTrue(dal.GetForums(null).Length == 0);
        }
    }
}
