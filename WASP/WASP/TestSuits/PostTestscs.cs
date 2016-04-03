using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.TestSuits
{
    class PostTestscs
    {
        [TestMethod]
        public void repliesTests()
        {
            // arrange
            User member = new User(0, false, "edan", "userName", "email@email.com", "123");
            Post original = new Post("original", "aa", 0, member, DateTime.Now, null, null, DateTime.Today);
            Post reply = new Post("reply", "aa", 0, member, DateTime.Now, original, null, DateTime.Today);
            Post reply2 = new Post("reply2", "aa", 1, member, DateTime.Now, original, null, DateTime.Today);

            
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
            // arrange
            User member = new User(0, false, "edan", "userName", "email@email.com", "123");
            Post original = new Post("original", "aa", 0, member, DateTime.Now, null, null, DateTime.Today);
            Post reply = new Post("reply", "aa", 0, member, DateTime.Now, original, null, DateTime.Today);
            Post reply2reply = new Post("reply2", "aa", 1, member, DateTime.Now, reply, null, DateTime.Today);


            // act
            original.AddReply(reply);
            reply.AddReply(reply2reply);
            

            // assert
            Assert.AreEqual(reply, original.InReplyTo, "checking if reply added to original");
            Assert.AreEqual(reply2reply, reply.InReplyTo, "checking if reply2reply added to reply2");

            Assert.AreEqual(1, original.GetAllReplies().Length, "checking if reply added and removed successfully");

        }

    }
}
