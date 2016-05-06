using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class SubForumTests
    {
        private DAL2 dal = new DALSQL();
        [TestMethod]
        public void subForumTest()
        {
            // arrange

            Forum forum = new Forum(0, "stackforum", "someDesc", null, null);
            User author = new User(0, "edan", "userName", "email@email.com", "123", forum,dal);
            forum.AddMember(author);
            Subforum sf = new Subforum(0, "sf", "sf", forum, null);

            Post original = new Post(1, "title", "content", author, DateTime.Now, null, sf, DateTime.Today, null);
            Post tempPost2 = new Post(2, "title", "content", author, DateTime.Today, original, sf, DateTime.Today, null);
            // act
            sf.AddThread(original);
            sf.AddThread(tempPost2);
            sf.RemoveThread(tempPost2.Id); // check if remove works
            // assert
            Assert.AreEqual(original, sf.GetThread(original.Id), "check if thread was added");
            Assert.AreEqual(1, sf.GetThreads().Length, "check if thread was removed.");
        }
        [TestMethod]
        public void moderatorTest()
        {
            // arrange

            Forum forum = new Forum(0, "stackforum", "someDesc", null, null);
            User author = new User(0, "edan", "userName", "email@email.com", "123", forum,dal);
            forum.AddMember(author);
            Subforum sf = new Subforum(0, "sf", "sf", forum, null);
            forum.AddSubForum(sf);
            DateTime dateNow = DateTime.Now;
            DateTime future = DateTime.Today;
            Moderator tempMod = new Moderator(author, DateTime.Today, sf, null, null);
            Moderator realMod = new Moderator(author, DateTime.Today, sf, null, null);

            // act
            sf.AddModerator(tempMod);
            sf.AddModerator(realMod);
            sf.RemoveModerator(tempMod.Id);
            // assert
            Assert.AreEqual(1, sf.GetAllModerators().Length, "checking if moderator added successfully");
            // *to check why getModerator recives author**//
            Assert.AreEqual(realMod, sf.GetModerator(realMod.Id), "checks if right moderator deleted");
        }
    }
}


