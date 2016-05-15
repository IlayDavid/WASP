using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.Exceptions;

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
        private Admin _admin;
        private User _member1;
        private User _member2;

        //private Subforum _subforum;

        
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _proj.Clean();
            _supervisor = Functions.InitialSystem(_proj);
            var forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _member1 = _proj.subscribeToForum(20,"amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456", _forum.Id);
            _member2 = _proj.subscribeToForum(21,"edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum.Id);

            _proj.login(_admin.User.Username, _admin.User.Password, _forum.Id);
            _proj.login(_member1.Username, _member1.Password, _forum.Id);
            _proj.login(_member2.Username, _member2.Password, _forum.Id);
        }

        /// <summary>
        /// Positive Test: log-in of member in forum
        /// </summary>
        [TestMethod]
        public void logInTest1()
        {
            Assert.IsNotNull(_proj.login("amitayaSh", "123456", _forum.Id));
            Assert.IsNotNull(_proj.login("edanHb", "123456", _forum.Id));
            Assert.IsNotNull(_proj.login(_admin.User.Username, _admin.User.Password, _forum.Id));
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void logInTest2()
        {
            _proj.login("", "123456", _forum.Id);
            
        }

        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest3()
        {
            
            _proj.login("blah", "", _forum.Id);
        }
        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void logInTest4()
        {
 
            _proj.login("amitayaSh", "", _forum.Id);
        }
        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest5()
        {
            
            _proj.login("", "123456", _forum.Id);
            
        }
        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest6()
        {
            _proj.login("blah", "123456", _forum.Id);
            
        }
    }
}
