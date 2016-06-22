
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;
using WASP.Domain;
using WASP.Exceptions;


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
            BL.Clean();
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
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(2055, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            // act
            Forum recivedForum = BL.getForum(forum.Id);
            bool isAdmin = recivedForum.IsAdmin(100);
            bool isMemeber = recivedForum.IsMember(2055);

            // assert
            Assert.IsNotNull(user);
            Assert.AreEqual(recivedForum.Id, forum.Id, "check if forum created");
            Assert.AreEqual(true, isAdmin, "check if forum created and there's an admin");
            Assert.AreEqual(true, isMemeber, "checks if forum has a member");
            Assert.AreEqual(100, BL.getAdmin(-1, forum.Id, 100).Id, " checks if -getAdmin- works");

        }
        [TestMethod]
        public void postTests()
        {
            // arrange
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            bool isSubForum = Forum.Get(forum.Id).IsSubForum(sf.Id);
            Post postFromDB = BL.getThread(forum.Id, post.Id);
            Subforum sfFromDB = BL.getSubforum(forum.Id, sf.Id);
            Forum forumFromDB = BL.getForum(forum.Id);
            // assert
            Assert.AreEqual(isSubForum, true, "subforum added, but not updated");
            Assert.AreEqual(postFromDB.Id, post.Id, "post added to DB");
            Assert.AreEqual(forumFromDB.Id, forum.Id, "forum added to DB");

        }

        [TestMethod]
        public void updateTests()
        {
            // arrange
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(88, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User user2 = BL.subscribeToForum(99, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
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
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
           // Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
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
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(88, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
           // Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act
            BL.addModerator(100, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            Subforum updatedSf = BL.getSubforum(forum.Id, sf.Id);
            DateTime date = new DateTime(2016, 12, 12);
            BL.updateModeratorTerm(100, forum.Id, user.Id, updatedSf.Id, date);
            DateTime term = BL.getModeratorTermTime(100, forum.Id, user.Id, updatedSf.Id);

            // assert
            Assert.AreEqual(date, term, "term updated");

        }
        [TestMethod]
        public void checkTotals()
        {
            // arrange
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(88, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, DateTime.Today);
            //Post post = BL.createThread(user.Id, forum.Id, "title", "content", sf.Id);
            // act

            // assert
            Assert.AreEqual(1, BL.totalForums(1234), "checking total forums");
            Assert.AreEqual(0, BL.subForumTotalMessages(100, forum.Id, sf.Id), "checking subForumTotal messages");
            // Assert.AreEqual(0, BL.memberTotalMessages(100, forum.Id), "checking memberTotal messages - return 0 if no messages");
            //Assert.AreEqual(1, BL.memberTotalMessages(user.Id, forum.Id), "checking is return right number of messages");
           // Assert.AreEqual(1, BL.postsByMember(100, forum.Id, user.Id).Length, "cheking if postByMember works");
        }


        [TestMethod]
        public void createForumTest()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            Assert.AreEqual(true, BL.getAllForums().Length > 0, "forum created");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginException))]
        public void checkLogin()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            BL.login("ori", "thisisfakeuser", forum.Id);
        }
        [TestMethod]
        public void checkValidLogin()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User userlogged = BL.login("edan", "123", forum.Id);
            Assert.IsNotNull(userlogged, "user logged succ");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginException))]
        public void checkSuperUserLogin()
        {
            BL.loginSU("ori", "thisisfakeuser");
        }

        [TestMethod]
        public void checkFalidSuperUserLogin()
        {
           SuperUser superUser =  BL.loginSU("moshe", "1234");
            Assert.IsNotNull(superUser,"super user logged in succ");
        }

        [TestMethod]
        public void checkModTerm()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(88, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            DateTime now = DateTime.Today;
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, now);
            BL.addModerator(100, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            sf = BL.getSubforum(forum.Id, sf.Id);

            DateTime checkDate = sf.GetModerator(willBeMod.Id).TermExp;
            Assert.AreEqual(now, checkDate, "Term date of mod works well");
        }


        [TestMethod]
        [ExpectedException(typeof(WaspException))]

        public void checkSuspendMod()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(88, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            DateTime now = DateTime.Today;
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, now);
            BL.addModerator(100, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            BL.deleteModerator(user.Id, forum.Id, willBeMod.Id, sf.Id);
       
        }
        [TestMethod]
        [ExpectedException(typeof(WaspException))]

        public void checkSuspendMod2()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User willBeMod = BL.subscribeToForum(88, "edanAdmin", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            DateTime now = DateTime.Today;
            Subforum sf = BL.createSubForum(100, forum.Id, "sf", "desc", user.Id, now);
            BL.addModerator(user.Id, forum.Id, willBeMod.Id, sf.Id, DateTime.Today);
            BL.deleteModerator(user.Id, forum.Id, willBeMod.Id, sf.Id);
            sf = BL.getSubforum(forum.Id, sf.Id);
            Assert.AreEqual(sf.GetModerator(willBeMod.Id), null, "delete succeded");

            
        }

        public void checkFriends()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user1 = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User user2 = BL.subscribeToForum(-1, "ilay", "david", "eli@post.bgu.ac.il", "123", forum.Id);
            User user3 = BL.subscribeToForum(-1, "noam", "barkay", "noam@post.bgu.ac.il", "123", forum.Id);
            user1.AddFriend(user2);
            user1.AddFriend(user3); 
            Assert.AreEqual(2, user1.GetAllFriends().Length, "2 friends added correctly");
            Assert.IsNull(user2.GetAllFriends());
            
        }

        public void checkNotification()
        {
            Policy policy = new Policy();
            Forum forum = BL.createForum(1234, "AviTheKing", "avi is a king", 100, "avi", "avi", "avi@gmail.com", "1234", policy);
            User user1 = BL.subscribeToForum(-1, "edan", "habler", "habler@post.bgu.ac.il", "123", forum.Id);
            User user2 = BL.subscribeToForum(-1, "ilay", "david", "eli@post.bgu.ac.il", "123", forum.Id);
            BL.sendMessage(user1.Id, forum.Id, user2.Id, "user1 sends msg to user 2");
            Assert.AreEqual(1, user2.GetNewNotifications().Length,"message recived");
            Assert.AreEqual(0, user1.GetNewNotifications().Length, "no new messages");
            
        }


    }
}


