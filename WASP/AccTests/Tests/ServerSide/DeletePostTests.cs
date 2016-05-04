using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DeletePostTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Admin _admin;
        private User _moderator;
        private Post _thread;
        private User _member;
        private Post _threadModerator;
        private Post _threadMember;

       
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            var forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, _admin.user.password, _forum.Id);

            var subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, _moderator.password, _forum.Id);

            _member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(_member.userName, _member.password, _forum.Id);

            _threadModerator = _proj.createThread(_moderator.id,_forum.Id, "webService for calander",
                                    "Someone know a good web service for Calander?",  _subforum.Id);

            _threadMember = _proj.createThread(_member.id,_forum.Id, "webService for calander",
                        "Someone know a good web service for Calander?",  _subforum.Id);



        }

        /// <summary>
        /// Positive Test: checks that moderator and member can delete their own thread
        /// </summary>
        [TestMethod]
        public void deletePostTest1()
        {
            int isDelete = _proj.deletePost(_member.id,_forum.Id, _threadMember.id);
            Assert.IsTrue(isDelete >= 0);
            isDelete = _proj.deletePost(_moderator.id, _forum.Id, _threadModerator.id);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void deletePostTest2()
        {
            Post p1 = _proj.createReplyPost(_moderator.id, _forum.Id, "Hi", _threadMember.id);
            Post p2 = _proj.createReplyPost(_member.id, _forum.Id, "Hi", _threadModerator.id);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);
            

            int isDelete = _proj.deletePost(_member.id, _forum.Id, p2.id);
            Assert.IsTrue(isDelete >= 0);
            isDelete = _proj.deletePost(_moderator.id, _forum.Id, p1.id);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete thread which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest3()
        {
            int isDelete = _proj.deletePost(_moderator.id, _forum.Id, _threadMember.id);
            Assert.IsTrue(isDelete > 0);
            isDelete = _proj.deletePost(_member.id, _forum.Id, _threadModerator.id);
            Assert.IsTrue(isDelete < 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete post which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest4()
        {
            Post p1 = _proj.createReplyPost(_moderator.id, _forum.Id, "Hi",  _threadMember.id);
            Post p2 = _proj.createReplyPost(_member.id, _forum.Id, "Hi", _threadModerator.id);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);

            int isDelete = _proj.deletePost(_moderator.id, _forum.Id, p2.id);
            Assert.IsTrue(isDelete > 0);
            isDelete = _proj.deletePost(_member.id, _forum.Id, p1.id);
            Assert.IsTrue(isDelete < 0);
        }
    }
}
