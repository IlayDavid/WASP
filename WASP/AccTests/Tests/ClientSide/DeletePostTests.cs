using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientDeletePostTests
    {

        private WASPClientBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private String supass = "moshe123";
        private Admin _admin;
        private String adminpass = "david123";
        private User _moderator;
        private String modpass = "ilan123";
        private Post _thread;
        private User _member;
        private String mempass = "ariel123";
        private Post _threadModerator;
        private Post _threadMember;

       
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            
            _supervisor = ClientFunctions.InitialSystem(_proj);

            var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName,adminpass, _forum.id, "");

            var subforumAndModerator = ClientFunctions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, modpass, _forum.id, "");

            _member = ClientFunctions.SubscribeSpecMember(_proj, _forum);
            _proj.login(_member.userName, mempass, _forum.id, "");

            _threadModerator = _proj.createThread("webService for calander",
                                    "Someone know a good web service for Calander?",  _subforum.id);

            _threadMember = _proj.createThread("webService for calander",
                        "Someone know a good web service for Calander?",  _subforum.id);



        }

        /// <summary>
        /// Positive Test: checks that moderator and member can delete their own thread
        /// </summary>
        [TestMethod]
        public void deletePostTest1()
        {
            int isDelete = _proj.deletePost(_threadMember.id);
            Assert.IsTrue(isDelete >= 0);
            _proj.login(_moderator.userName, modpass, _forum.id, "");
            isDelete = _proj.deletePost(_threadModerator.id);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void deletePostTest2()
        {
            Post p1 = _proj.createReplyPost("Hi", _threadMember.id);
            Post p2 = _proj.createReplyPost("Hi", _threadModerator.id);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);
            

            int isDelete = _proj.deletePost(p2.id);
            Assert.IsTrue(isDelete >= 0);
            isDelete = _proj.deletePost(p1.id);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete thread which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest3()
        {
            int isDelete = _proj.deletePost(_threadMember.id);
            Assert.IsTrue(isDelete > 0);
            isDelete = _proj.deletePost(_threadModerator.id);
            Assert.IsTrue(isDelete < 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete post which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest4()
        {
            _proj.login(_moderator.userName, modpass, _forum.id, "");
            Post p1 = _proj.createReplyPost("Hi",  _threadMember.id);
            Post p2 = _proj.createReplyPost("Hi", _threadModerator.id);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);

            _proj.login(_member.userName, mempass, _forum.id, "");
            int isDelete = _proj.deletePost(p2.id);
            Assert.IsTrue(isDelete > 0);
            isDelete = _proj.deletePost(p1.id);
            Assert.IsTrue(isDelete < 0);
        }
    }
}
