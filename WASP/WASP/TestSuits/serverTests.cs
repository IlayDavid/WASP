using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
namespace WASP.TestSuits
{
    [TestClass]
    public class serverTests
    {
        private ServerAPI server=new Server.Server();
        private SuperUser _supervisor=null;
        private Forum forum = null;
        private int _subforumId = 0;
        private int _threadId = 0;


        [ClassInitialize]
        public void initializeTests()
        {
            _supervisor=server.initialize("super", "super", "super@user.man", "super");
            forum = server.createForum(_supervisor, "forum", "the forum", "admin", "admin", "e@mail.com", "admin");
        }

        [TestMethod]
        public void getforum()
        {
            var serverForum = server.getForum(null, forum.Id);
            Assert.IsTrue(forum == serverForum);
        }
        [TestMethod]
        public void login1()
        {
            //assert that admin can login
            var admin = server.login("admin", "admin", forum);
            Assert.IsNotNull(admin);
        }

        [TestMethod]
        public void subscribeUser1()
        {
            //assert that subscribed user requires an input, and requires mail to contain '.' and '@'
            var nothin = server.subscribeToForum("", "", "", "", forum);
            var badmail = server.subscribeToForum("a", "a", "a", "a", forum);
            Assert.IsTrue(nothin == null && badmail == null);
        }

        [TestMethod]
        public void subscribeUser2()
        {
            //assert that we can create a user
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            Assert.IsNotNull(member);
        }
        [TestMethod]
        public void createSubForum1()
        {
            //assert we cannot create a subforum with no description or name
            var admin = server.login("admin", "admin", forum);
            var subforum= server.createSubForum(admin, "", "", admin, DateTime.Now);
            Assert.IsNotNull(subforum);
        }

        [TestMethod]
        public void createSubForum2()
        {
            //assert an admin can create a subforum with himself as moderator
            var admin = server.login("admin", "admin", forum);
            var subforum=server.createSubForum(admin, "sub", "forum", admin, DateTime.Now);
            Assert.IsNotNull(subforum);
        }
        [TestMethod]
        public void createSubForum3()
        {
            //assert an admin can not create a second identicle subforum
            var admin = server.login("admin", "admin", forum);
            var subforum= server.createSubForum(admin, "sub", "forum", admin, DateTime.Now);
            Assert.IsNull(subforum);
        }
        [TestMethod]
        public void createSubForum4()
        {
            //assert an admin can create a subforum with user as the moderator
            var admin = server.login("admin", "admin", forum);
            var subforum=server.createSubForum(admin, "sub2", "forum2", server.login("user","user",forum), DateTime.Now);
            _subforumId = subforum.Id;
            Assert.IsNotNull(subforum);
        }
        [TestMethod]
        public void checkGetSubforum()
        {
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub", "forum", admin, DateTime.Now);
            var inserverSubforum = server.getSubforum(admin, subforum.Id);
            Assert.IsTrue(subforum==inserverSubforum);
        }
        [TestMethod]
        public void createThread1()
        {
            //asserts thread cannot accept an empty body and content
            var user = server.login("user", "user", forum);
            var thread = server.createThread(user, "", "", DateTime.Now, server.getSubforum(user, _subforumId));

            Assert.IsNull(thread);
        }

        [TestMethod]
        public void createThread2()
        {
            //asserts thread can be created
            var user = server.login("user", "user", forum);
            var thread = server.createThread(user, "title", "body", DateTime.Now, server.getSubforum(user,_subforumId));
            _threadId = thread.Id;
            Assert.IsNotNull(thread);
        }

        [TestMethod]
        public void createReply1()
        {
            //asserts that replies requires a body
            var user = server.login("user", "user", forum);
            var reply = server.createReplyPost(user, "", DateTime.Now, server.getThread(user, _threadId));
            Assert.IsNull(reply);
        }

        [TestMethod]
        public void createReply2()
        {
            //asserts that we can reply to a thread
            var user = server.login("user", "user", forum);
            var reply = server.createReplyPost(user, "reply!", DateTime.Now,
                server.getThread(user, _threadId));
            Assert.IsNotNull(reply);
        }

        [TestMethod]
        public void createReply3()
        {
            //asserts we can reply to a post
            var user = server.login("user", "user", forum);
            var reply = server.createReplyPost(user, "reply to reply", DateTime.Now,
                server.getThread(user, _threadId).GetAllReplies().First((x)=>true));
            Assert.IsNotNull(reply);
        }

        [TestMethod]
        public void addModerator()
        {
            //checks that we can add a moderator
            var admin = server.login("admin", "admin", forum);
            var check=server.addModerator(admin, admin,server.getSubforum(admin, _subforumId), DateTime.MaxValue);
            Assert.IsTrue(check>=0);
        }
        [TestMethod]
        public void getModeratorTermTime()
        {
            //assert that we can get the correct moderator term time
            var admin = server.login("admin", "admin", forum);
            var check = server.getModeratorTermTime(admin, admin, server.getSubforum(admin, _subforumId));
            Assert.IsTrue(DateTime.MaxValue==check);
            
        }

        [TestMethod]
        public void changeModeratorTermTime()
        {
            var admin = server.login("admin", "admin", forum);
            var subforum = server.getSubforum(admin, _subforumId);
            var check = server.updateModeratorTerm(admin, admin, subforum, DateTime.Now);
            Assert.IsTrue(check>=0);
        }

    }
}
