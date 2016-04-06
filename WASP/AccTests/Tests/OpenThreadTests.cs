using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class OpenThreadTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Member _admin;
        private Member _moderator;

      
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
           

        }


        /// <summary>
        /// Positive Test: checks that member can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest1()
        {
            Member member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.UserName, member.Password, _forum);
            Post isOpenPost = _proj.createThread(member, "webService for calander", "Someone know a good web service for Calander?",
                                   DateTime.Now, _subforum);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Positive Test: checks that admin can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest2()
        {
            Post isOpenPost = _proj.createThread(_moderator, "webService for calander", "Someone know a good web service for Calander?",
                                   DateTime.Now, _subforum);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void OpenThreadTest3()
        {
            Post isOpenPost = _proj.createThread(_moderator, "", "Someone know a good web service for Calander?",
                                   DateTime.Now, _subforum);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(_moderator, "webService for calander", "", DateTime.Now, _subforum);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(_moderator, "webService for calander", "Someone know a good web service for Calander?",
                       DateTime.Now, _subforum);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(_moderator, "webService for calander", "Someone know a good web service for Calander?",
                       DateTime.Now.AddDays(-10), _subforum);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(null, "webService for calander", "Someone know a good web service for Calander?",
                       DateTime.Now, _subforum);
            Assert.IsNull(isOpenPost);
        }

        /// <summary>
        /// Negative Test: secure NF: member that doent not sign in this forum, try to post
        /// </summary>
        [TestMethod]
        public void OpenThreadTest4()
        {
            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            Forum forum = forumAndAdmin.Item1;
            Tuple<Subforum, Member> subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            Subforum subforum = subforumAndModerator.Item1;
            Member moderator = subforumAndModerator.Item2;
            _proj.login(moderator.UserName, moderator.Password, _forum);

            Member member = Functions.SubscribeSpecMember2(_proj, forum);
            _proj.login(member.UserName, member.Password, forum);

            Post isOpenPost = _proj.createThread(member, "webService for calander", "Someone know a good web service for Calander?",
                                   DateTime.Now, subforum);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(moderator, "webService for calander", "Someone know a good web service for Calander?",
                       DateTime.Now, subforum);
            Assert.IsNull(isOpenPost);
        }
    }
}
