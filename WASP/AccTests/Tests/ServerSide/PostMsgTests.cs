using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PostMsgTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Admin _admin;
        private User _moderator;
        private Post _thread;

       
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

            _thread  = _proj.createThread(_moderator.id,_forum.Id, "webService for calander",
                                    "Someone know a good web service for Calander?", _subforum.Id);
        }


        /// <summary>
        /// Positive Test: checks that member can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest1()
        {
            var member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.userName, member.password, _forum.Id);
            Post isPost = _proj.createReplyPost(member.id,_forum.Id, "sereach at google",_thread.Id);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Positive Test: checks that moderator can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest2()
        {
            Post isPost = _proj.createReplyPost(_moderator.id,_forum.Id, "sereach at google", _thread.Id);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void PostMsgTest3()
        {
            Post isPost = _proj.createReplyPost(-1,_forum.Id , "sereach at google", _thread.Id);
            Assert.IsNull(isPost);

            isPost = _proj.createReplyPost(_moderator.id,_forum.Id, "",  _thread.Id);
            Assert.IsNull(isPost);

            isPost = _proj.createReplyPost(_moderator.id,_forum.Id, "sereach at google", -1);
            Assert.IsNull(isPost);
        }
    }
}
