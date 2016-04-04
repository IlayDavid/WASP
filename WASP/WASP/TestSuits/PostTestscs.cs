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
            Forum forum = new Forum("stackforum", "someDesc");

            Member member = new Member("edan", "userName", "email@email.com", "123",forum);
            Post original = new Post("original", "aa", member, DateTime.Now, null, null, DateTime.Today);
            Post reply = new Post("reply", "aa", member, DateTime.Now, original, null, DateTime.Today);
            Post reply2 = new Post("reply2", "aa", member, DateTime.Now, original, null, DateTime.Today);

            
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
            Forum forum = new Forum("stackforum", "someDesc");

            Member member = new Member("username", "edan", "email", "pass", forum);
            Post original = new Post("title", "content", member, DateTime.Now, null, null, DateTime.Today);
            Post reply = new Post("reply", "aa", member, DateTime.Now, original, null, DateTime.Today);
            Post reply2reply = new Post("reply2", "aa", member, DateTime.Now, reply, null, DateTime.Today);


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
