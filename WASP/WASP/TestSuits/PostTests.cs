using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]

    public class PostTests
    {
        private DAL2 dal = new DALSQL();
        [TestMethod]
        public void repliesTests()
        {
            // arrange
            Forum forum = new Forum(0, "stackforum", "someDesc", null, null);
            User author = new User(0, "edan", "userName", "email@email.com", "123", forum,dal);
            forum.AddMember(author);
            Subforum sf = new Subforum(0, "sf", "sf", forum, null);
            Post original = new Post(0, "original", "original post", author, DateTime.Now, null, sf, DateTime.Now, null);
            Post reply = new Post(1, "reply", "content", author, DateTime.Now, original, sf, DateTime.Now, null);
            Post reply2 = new Post(2, "reply2", "content2", author, DateTime.Now, original, sf, DateTime.Now, null);
            // act
            original.AddReply(reply);
            original.AddReply(reply2);
            original.RemoveReply(reply2.Id);
            // assert
            Assert.AreEqual(true, original.IsOriginal(), "checking if reply added as original");
            Assert.AreEqual(false, reply.IsOriginal(), "checking if reply added as non-original");
            Assert.AreEqual(reply, original.GetReply(reply.Id), "checking if reply added successfully");
            Assert.AreEqual(1, original.GetAllReplies().Length, "checking if reply added and removed successfully");
        }
        [TestMethod]
        public void replyTests()
        {
            //check reply to reply
            // arrange
            Forum forum = new Forum(0, "stackforum", "someDesc", null, null);
            User member = new User(0, "edan", "userName", "email@email.com", "123", forum,dal);
            forum.AddMember(member);
            Subforum sf = new Subforum(0, "sf", "sf", forum, null);
            Post original = new Post(0, "original", "original post", member, DateTime.Now, null, sf, DateTime.Now, null);
            Post reply = new Post(1, "reply", "content", member, DateTime.Now, original, sf, DateTime.Now, null);
            Post reply2reply = new Post(1, "reply", "content", member, DateTime.Now, reply, sf, DateTime.Now, null);

            // act
            original.AddReply(reply);
            reply.AddReply(reply2reply);
            original.AddReply(reply2reply);
            original.RemoveReply(reply2reply.Id); // should not delete anything.
            reply.RemoveReply(reply2reply.Id); // shuld remove reply2reply from reply replys
            // assert
            Assert.AreEqual(reply, original.GetReply(reply.Id), "checking if reply added to original");
            Assert.AreEqual(reply2reply, reply.GetReply(reply2reply.Id), "checking if reply2reply added to reply2");
            Assert.AreEqual(1, original.GetAllReplies().Length, "checking if reply added and removed successfully");
            Assert.AreEqual(0, reply.GetAllReplies().Length, "checking if reply2reply removed successfully");

        }
    }
}
