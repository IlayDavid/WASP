using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class SubForumTests
    {
        [TestMethod]
        public void subForumTest()
        {
            // arrange
            Forum forum = new Forum("stack", "desc", new PasswordPolicy());
            Member author = new Member("edan", "habler", "mail@mail.com", "123",forum);
            Subforum sf = new Subforum("subForum", "someDescription",author, DateTime.Now);
            Post post = new Post("title", "content", author, DateTime.Now, sf);
            Post tempPost2 = new Post("title", "content",author, DateTime.Today, sf);
            // act
            sf.AddThread(tempPost2);
            sf.RemoveThread(tempPost2); // check if remove works
            sf.AddThread(post);
            // assert
            Assert.AreEqual(post, sf.GetThread(post.Id), "check if thread was added");
            Assert.AreEqual(1, sf.GetThreads().Count, "check if thread was removed.");
        }
        [TestMethod]
        public void moderatorTest()
        {
            // arrange
            Forum forum = new Forum("stack", "desc", new PasswordPolicy());
            Member author = new Member("edan", "habler", "mail@mail.com", "123", forum);
            Subforum sf = new Subforum("subForum", "someDescription",author,DateTime.Now);
            forum.AddSubForum(sf);
            DateTime dateNow = DateTime.Now;
            DateTime future = DateTime.Today;
            Member tempUser = new Member( "userForDelete", "ToRemove", "mail@mail.com", "123",forum);
            // act
            sf.AddModerator(tempUser, dateNow);
            sf.RemoveModerator(author);
            // assert
            Assert.AreEqual(1, sf.GetModerators().Count, "checking if moderator added successfully");
            // *to check why getModerator recives author**//
            Assert.AreEqual(tempUser, sf.GetModerator(tempUser).Item1, "check if right moderator deleted");
        }
    }
}
