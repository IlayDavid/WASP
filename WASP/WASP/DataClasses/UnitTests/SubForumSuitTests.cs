
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class SubForumSuitTests

    {
        private DAL dal = new DALSQL();
        int forumId1;
        int forumId2;



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
            Forum forum1 = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
            Forum forum2 = dal.CreateForum(new Forum(-1, "Start-Up", "blah", null, dal));
            forumId1 = forum1.Id;
            forumId2 = forum2.Id;
        }




        [TestMethod]
        public void AddSubforumTest1()
        {
            try
            {
                Subforum subforum = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal );

                int subforumId = dal.CreateSubForum(subforum).Id;
                Assert.IsTrue(subforumId > 0);

                Subforum newSubforum = dal.GetSubForum(subforumId);
                Assert.IsTrue(subforum.Name == newSubforum.Name);
                Assert.IsTrue(subforum.Description == newSubforum.Description);
                Assert.IsTrue(subforum.Forum.Id == newSubforum.Forum.Id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddSubforumTest2()
        {
            try
            {
                Subforum subforum1 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal);
                Subforum subforum2 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId2), dal);
                int subforumId1 = dal.CreateSubForum(subforum1).Id;
                int subforumId2 = dal.CreateSubForum(subforum2).Id;

                Assert.IsTrue(subforumId1 > 0);
                Assert.IsTrue(subforumId2 > 0);

                Subforum newSubforum1 = dal.GetSubForum(subforumId1);
                Assert.IsTrue(subforum1.Name == newSubforum1.Name);
                Assert.IsTrue(subforum1.Description == newSubforum1.Description);
                Assert.IsTrue(subforum1.Forum.Id == newSubforum1.Forum.Id);

                Subforum newSubforum2 = dal.GetSubForum(subforumId2);
                Assert.IsTrue(subforum2.Name == newSubforum2.Name);
                Assert.IsTrue(subforum2.Description == newSubforum2.Description);
                Assert.IsTrue(subforum2.Forum.Id == newSubforum2.Forum.Id);


            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateSubforumTest3()
        {
            try
            {
                Subforum subforum = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal);
                int subforumId = dal.CreateSubForum(subforum).Id;
                dal.UpdateSubForum(new Subforum(subforumId, "calandar1", "blah blah blah", dal.GetForum(forumId2), dal));
                Subforum updatedSubforum = dal.GetSubForum(subforumId);
                Assert.IsTrue(updatedSubforum.Name == "calandar1");
                Assert.IsTrue(updatedSubforum.Description == "blah blah blah");
                Assert.IsTrue(updatedSubforum.Forum.Id == forumId2);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetSubforumsTest4()
        {
            try
            {
                Subforum subforum1 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal);
                Subforum subforum2 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId2), dal);
                int subforumId1 = dal.CreateSubForum(subforum1).Id;
                int subforumId2 = dal.CreateSubForum(subforum2).Id;

                Subforum[] forums = dal.GetSubForums(null);
                Assert.IsTrue(forums.Length == 2);
                Assert.IsTrue(forums[0].Name == subforum1.Name || forums[1].Name == subforum1.Name);
                Assert.IsTrue(forums[0].Name == subforum2.Name || forums[1].Name == subforum2.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetSubforumsTest5()
        {
            try
            {
                Subforum subforum1 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal);
                Subforum subforum2 = new Subforum(-1, "calandar", "blah", dal.GetForum(forumId2), dal);
                int subforumId1 = dal.CreateSubForum(subforum1).Id;
                int subforumId2 = dal.CreateSubForum(subforum2).Id;

                Subforum[] forums = dal.GetSubForums(new int [] { subforumId1 });
                Assert.IsTrue(forums.Length == 1);
                Assert.IsTrue(forums[0].Name == subforum1.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteSubforumTest6()
        {
            Subforum subforum1 = dal.CreateSubForum(new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal));
            Subforum subforum2 = dal.CreateSubForum(new Subforum(-1, "calandar", "blah", dal.GetForum(forumId1), dal));
            dal.DeleteSubforum(subforum1.Id);
            Assert.IsTrue(dal.GetSubForums(null).Length == 1);
            dal.DeleteSubforum(subforum2.Id);
            Assert.IsTrue(dal.GetSubForums(null).Length == 0);
        }
    }
}
