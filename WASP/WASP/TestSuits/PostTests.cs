using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void repliesTests()
        {
            // arrange
            Forum forum = new Forum("stackforum", "someDesc", new PasswordPolicy());
            Member member = new Member("edan", "userName", "email@email.com", "123",forum);
            Subforum sf = new Subforum("sf", "sf", member, DateTime.Now);
            Post original = new Post("original", "original post", member, DateTime.Now, sf);
            Post reply = new Post("reply", member, DateTime.Now, original);
            Post reply2 = new Post("reply2", member, DateTime.Now, original);
            // act
            original.AddReply(reply);
            original.AddReply(reply2);
            original.RemoveReply(reply2);
            // assert
            Assert.AreEqual(true, original.IsOriginal(), "checking if reply added as original");
            Assert.AreEqual(false, reply.IsOriginal(), "checking if reply added as non-original");
            Assert.AreEqual(reply, original.GetReply(reply.Id), "checking if reply added successfully");
            Assert.AreEqual(1, original.GetAllReplies().Count, "checking if reply added and removed successfully");
        }
        [TestMethod]
        public void replyTests()
        {
            //check reply to reply
            // arrange
            Forum forum = new Forum("stackforum", "someDesc", new PasswordPolicy());
            Member member = new Member("username", "edan", "email", "pass", forum);
            Post original = new Post("title", "content", member, DateTime.Now, null);
            Post reply = new Post("reply", member, DateTime.Now, original);
            Post reply2reply = new Post("reply2", member, DateTime.Now, reply);
            // act
            original.AddReply(reply);
            reply.AddReply(reply2reply);
            // assert
            Assert.AreEqual(reply, original.InReplyTo, "checking if reply added to original");
            Assert.AreEqual(reply2reply, reply.InReplyTo, "checking if reply2reply added to reply2");
            Assert.AreEqual(1, original.GetAllReplies().Count, "checking if reply added and removed successfully");
        }
    }
}
