using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class LogInTests
    {

        private static WASPBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Member _admin;
        private Member _member1;
        private Member _member2;

        //private Subforum _subforum;

        
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _member1 = _proj.subscribeToForum("amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456", _forum);
            _member2 = _proj.subscribeToForum("edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum);

            _proj.login(_admin.UserName, _admin.Password, _forum);
            _proj.login(_member1.UserName, _member1.Password, _forum);
            _proj.login(_member2.UserName, _member2.Password, _forum);
        }

        /// <summary>
        /// Positive Test: log-in of member in forum
        /// </summary>
        [TestMethod]
        public void logInTest1()
        {
            Assert.IsNotNull(_proj.login("amitayaSh", "123456", _forum));
            Assert.IsNotNull(_proj.login("edanHb", "123456", _forum));
            Assert.IsNotNull(_admin.UserName, _admin.Password, _admin.MemberForum);
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void logInTest2()
        {
            Assert.IsNull(_proj.login("", "123456", _forum));
            Assert.IsNull(_proj.login("amitayaSh", "", _forum));
        }

        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [TestMethod]
        public void logInTest3()
        {
            Assert.IsNull(_proj.login("blah", "123456", _forum));
            Assert.IsNull(_proj.login("", "123456", _forum));
            Assert.IsNull(_proj.login("blah", "", _forum));
        }
    }
}
