
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP.Domain;

namespace WASP.TestSuits
{
    [TestClass]

    public class DomainSuite
    {
        private BLFacade BL = new BLFacade();
        private DALSQL dal = new DALSQL();

        [TestInitialize]
        public void SetUp()
        {
            BL.Backup();
            BL.Clean();
            BL.initialize("moshe", "moshe", 1234, "habler@post.bgu.ac.il", "1234");
        }

        [TestCleanup]
        public void TearDown()
        {
            BL.Clean();
            BL.Restore();
        }

        [TestMethod]
        public void createForum()
        {
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            // act
            Forum recivedForum = BL.getForum(forum.Id);
            bool isAdmin = recivedForum.IsAdmin(100);
            bool isMemeber = recivedForum.IsMember(user.Id);
            
            // assert
            Assert.AreEqual(recivedForum.Id, forum.Id, "check if forum created");
            Assert.AreEqual(true, isAdmin, "check if forum created and there's an admin");
            Assert.AreEqual(true, isMemeber, "checks if forum has a member");
            Assert.AreEqual(100, BL.getAdmin(-1, forum.Id, 100).Id, " checks if -getAdmin- works");

        }
        [TestMethod]
        public void postTests()
        {
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            bool isSubForum = forum.IsSubForum(sf.Id);
            Post postFromDB = user.GetPost(post.Id);
            Subforum sfFromDB = BL.getSubforum(forum.Id, sf.Id);
            Forum forumFromDB = BL.getForum(forum.Id);
            // assert
            Assert.AreEqual(isSubForum, false, "subforum added, but not updated");
            Assert.AreEqual(postFromDB.Id, post.Id, "post added to DB");
            Assert.AreEqual(forumFromDB.Id, forum.Id, "forum added to DB");

        }

        [TestMethod]
        public void updateTests()
        {
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User user2 = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            Post reply = BL.createReplyPost(user2.Id, forum.Id, "content", post.Id);
            // act
            Post updatedPost = BL.getThread(forum.Id, post.Id);
            Post[] replyArr = BL.getReplys(forum.Id, sf.Id, updatedPost.Id);

            // assert
            Assert.AreEqual(reply.Id, replyArr[0].Id, "getReply and CreateReply works");
            Assert.AreEqual(1, updatedPost.GetAllReplies().Length, "getThread works and reply was added to post");
        }

        [TestMethod]
        public void checkReplyTests()
        {
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
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(-1, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            BL.addModerator(100, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            Subforum updatedSf = BL.getSubforum(forum.Id, sf.Id);
            DateTime date = new DateTime(2016, 12, 12);
            BL.updateModeratorTerm(100, forum.Id, user.Id, updatedSf.Id, date);
            DateTime term = BL.getModeratorTermTime(100, forum.Id, user.Id, updatedSf.Id);

            // assert
            Assert.AreEqual(date, term, "term updated");

        }
        public void checkTotals()
        {
            // arrange
            Forum forum = BL.createForum(-1, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234");
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(-1, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act

            // assert
            Assert.AreEqual(1, BL.totalForums(100), "checking total forums");
            Assert.AreEqual(1, BL.subForumTotalMessages(100, forum.Id, sf.Id), "checking subForumTotal messages");
            Assert.AreEqual(0, BL.memberTotalMessages(100, forum.Id), "checking memberTotal messages - return 0 if no messages");
            Assert.AreEqual(1, BL.memberTotalMessages(user.Id, forum.Id), "checking is return right number of messages");
            Assert.AreEqual(1, BL.postsByMember(100, forum.Id, user.Id).Length, "cheking if postByMember works");
        }
    }
}


