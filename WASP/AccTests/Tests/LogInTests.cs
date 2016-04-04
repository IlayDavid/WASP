using System;
using NUnit.Framework;
using WASP;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class LogInTests
    {

        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Member _admin;
        private Member _member1;
        private Member _member2;

        //private Subforum _subforum;

        [OneTimeSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
            
        }

        [SetUp]
        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _member1 = _proj.subscribeToForum("amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456", _forum);
            _member2 = _proj.subscribeToForum("edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum);
        }

        /// <summary>
        /// Positive Test: log-in of member in forum
        /// </summary>
        [Test]
        public void logInTest1()
        {
            Assert.NotNull(_proj.login("amitayaSh", "123456", _forum));
            Assert.NotNull(_proj.login("edanHb", "123456", _forum));
            Assert.NotNull(_admin.UserName, _admin.Password, _admin.MemberForum);
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        public void logInTest2()
        {
            Assert.IsNull(_proj.login("", "123456", _forum));
            Assert.IsNull(_proj.login("amitayaSh", "", _forum));
        }

        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        public void logInTest3()
        {
            Assert.IsNull(_proj.login("blah", "123456", _forum));
            Assert.IsNull(_proj.login("", "123456", _forum));
            Assert.IsNull(_proj.login("blah", "", _forum));
        }




    }
}
