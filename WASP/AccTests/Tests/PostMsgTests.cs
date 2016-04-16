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
        private Member _admin;
        private Member _moderator;
        private Post _thread;

       
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.UserName, _admin.Password, _forum);

            Tuple<Subforum, Member> subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.UserName, _moderator.Password, _forum);

            _thread  = _proj.createThread(_moderator, "webService for calander",
                                    "Someone know a good web service for Calander?", DateTime.Now, _subforum);
        }


        /// <summary>
        /// Positive Test: checks that member can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest1()
        {
            Member member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.UserName, member.Password, _forum);
            Post isPost = _proj.createReplyPost(member, "sereach at google", DateTime.Now, _thread);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Positive Test: checks that moderator can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest2()
        {
            Post isPost = _proj.createReplyPost(_moderator, "sereach at google", DateTime.Now, _thread);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void PostMsgTest3()
        {
            Post isPost = _proj.createReplyPost(null , "sereach at google", DateTime.Now, _thread);
            Assert.IsNull(isPost);

            isPost = _proj.createReplyPost(_moderator, "", DateTime.Now.AddDays(10), _thread);
            Assert.IsNull(isPost);

            isPost = _proj.createReplyPost(_moderator, "sereach at google", DateTime.Now, null);
            Assert.IsNull(isPost);
        }
    }
}
