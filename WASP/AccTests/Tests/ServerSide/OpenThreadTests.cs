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
        private Admin _admin;
        private User _moderator;

      
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            _proj.Clean();
            _supervisor = Functions.InitialSystem(_proj);

            var forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.User.Username, _admin.User.Password, _forum.Id);

            var subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.Username, _moderator.Password, _forum.Id);
           

        }


        /// <summary>
        /// Positive Test: checks that member can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest1()
        {
            User member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.Username, member.Password, _forum.Id);
            Post isOpenPost = _proj.createThread(member.Id,_forum.Id, "webService for calander", "Someone know a good web service for Calander?",
                                   _subforum.Id);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Positive Test: checks that admin can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest2()
        {
            Post isOpenPost = _proj.createThread(_moderator.Id,_forum.Id, "webService for calander", "Someone know a good web service for Calander?",
                                   _subforum.Id);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void OpenThreadTest3()
        {
            Post isOpenPost = _proj.createThread(_moderator.Id, _forum.Id, "", "Someone know a good web service for Calander?",
                                   _subforum.Id);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(_moderator.Id, _forum.Id, "webService for calander", "", _subforum.Id);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(_moderator.Id, _forum.Id, "webService for calander", "Someone know a good web service for Calander?",
                       -1);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread(-1, _forum.Id, "webService for calander", "Someone know a good web service for Calander?",
                       _subforum.Id);
            Assert.IsNull(isOpenPost);
        }
    }
}
