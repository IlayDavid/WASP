using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientOpenThreadTests
    {

        private WASPClientBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Admin _admin;
        private User _moderator;

      
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            
            _supervisor = ClientFunctions.InitialSystem(_proj);

            var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, _admin.user.password, _forum.id, "");

            var subforumAndModerator = ClientFunctions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, _moderator.password, _forum.id, "");
           

        }


        /// <summary>
        /// Positive Test: checks that member can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest1()
        {
            User member = ClientFunctions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.userName, member.password, _forum.id, "");
            Post isOpenPost = _proj.createThread("webService for calander", "Someone know a good web service for Calander?",
                                   _subforum.id);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Positive Test: checks that admin can add a thread
        /// </summary>
        [TestMethod]
        public void OpenThreadTest2()
        {
            Post isOpenPost = _proj.createThread("webService for calander", "Someone know a good web service for Calander?",
                                   _subforum.id);
            Assert.IsNotNull(isOpenPost);
        }

        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void OpenThreadTest3()
        {
            Post isOpenPost = _proj.createThread("", "Someone know a good web service for Calander?",
                                   _subforum.id);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread("webService for calander", "", _subforum.id);
            Assert.IsNull(isOpenPost);

            isOpenPost = _proj.createThread("webService for calander", "Someone know a good web service for Calander?",
                       -1);
            Assert.IsNull(isOpenPost);
            
        }
    }
}
