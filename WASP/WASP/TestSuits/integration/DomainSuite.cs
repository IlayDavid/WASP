
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP.Domain;

namespace WASP.TestSuits.integration
{
    [TestClass]

    class DomainSuite
    {
        private BLFacade BL;
        private DALSQL dal;

        [TestMethod]
        public void createForum()
        {
            //pre-arrange
            dal.Clean();
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            // act
            bool isAdmin = forum.IsAdmin(100);
            bool isMemeber = forum.IsMember(user.Id);
            Forum recivedForum = BL.getForum(forum.Id);
            // assert
            Assert.AreEqual(recivedForum.Id, forum.Id, "check if forum created");
            Assert.AreEqual(true, isAdmin, "check if forum created and there's an admin");
            Assert.AreEqual(true, isMemeber, "checks if forum has a member");
            Assert.AreEqual(100, BL.getAdmin(-1,forum.Id, 100).Id, " checks if -getAdmin- works");
  
        }
        [TestMethod]
        public void postTests()
        {
            //pre-arragne
            dal.Clean();
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            bool isSubForum = forum.IsSubForum(sf.Id);
            Post postFromDB = user.GetPost(post.Id);
            Subforum sfFromDB = BL.getSubforum(forum.Id,sf.Id);
            Forum forumFromDB = BL.getForum(forum.Id);
            // assert
            Assert.AreEqual(isSubForum, false, "subforum added, but not updated");
            Assert.AreEqual(postFromDB.Id, post.Id, "post added to DB");
            Assert.AreEqual(forumFromDB.Id, forum.Id, "forum added to DB");

        }

        [TestMethod]
        public void updateTests()
        {
            //pre-arragne
            dal.Clean();
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User user2 = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            Post reply = BL.createReplyPost(user2.Id, forum.Id, "content", post.Id);
            // act
            Post updatedPost = BL.getThread(forum.Id, post.Id);
            Post [] replyArr = BL.getReplys(forum.Id, sf.Id, updatedPost.Id);

            // assert
            Assert.AreEqual(reply.Id, replyArr[0].Id, "getReply and CreateReply works");
            Assert.AreEqual(1, updatedPost.GetAllReplies().Length, "getThread works and reply was added to post");
            
        }

        [TestMethod]
        public void checkReplyTests()
        {
            //pre-arragne
            dal.Clean();
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            bool isSubForum = forum.IsSubForum(sf.Id);
            Forum forum2 = BL.getForum(forum.Id); // forum after subscribe subforum - contains sub forum.
            bool isSubforum = forum2.IsSubForum(sf.Id);
            // assert
            Assert.AreEqual(true, isSubForum, "subforum added, but not updated");

        }

        [TestMethod]
        public void checkModerator()
        {
            //pre-arragne
            dal.Clean();
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(-1, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            BL.addModerator(100, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            Subforum updatedSf = BL.getSubforum(forum.Id, sf.Id);

       
            // assert
            Assert.AreEqual(2, sf.GetAllModerators().Length, "added another mod execpt the one at constructor");
            Assert.AreEqual(true, updatedSf.IsModerator(willBeMod.Id), " addModerator works");
            Assert.AreEqual(false, sf.IsModerator(willBeMod.Id), "willBeMod is not a mod in subForum before the add function");
            


        }
    }
}


