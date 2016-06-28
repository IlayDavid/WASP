using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientPostMsgTests
    {

        private WASPClientBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private string supass = "moshe123";
        private SuperUser _supervisor;
        private Admin _admin;
        private string adminpass = "david123";
        private User _moderator;
        private string modpass = "ilan123";
        private Post _thread;

       
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            
            _supervisor = ClientFunctions.InitialSystem(_proj);

           var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, adminpass, _forum.id,"");

            var subforumAndModerator = ClientFunctions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, modpass, _forum.id,"");

            _thread  = _proj.createThread("webService for calander",
                                    "Someone know a good web service for Calander?", _subforum.id);
        }


        /// <summary>
        /// Positive Test: checks that member can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest1()
        {
            var member = ClientFunctions.SubscribeSpecMember(_proj, _forum);
            _proj.login(member.userName, "ariel123", _forum.id,"");
            Post isPost = _proj.createReplyPost("sereach at google",_thread.id);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Positive Test: checks that moderator can add a post
        /// </summary>
        [TestMethod]
        public void PostMsgTest2()
        {
            Post isPost = _proj.createReplyPost("sereach at google", _thread.id);
            Assert.IsNotNull(isPost);
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void PostMsgTest3()
        {
            
            var isPost = _proj.createReplyPost("",  _thread.id);
            Assert.IsNull(isPost);

            isPost = _proj.createReplyPost("sereach at google", -1);
            Assert.IsNull(isPost);
        }
    }
}
