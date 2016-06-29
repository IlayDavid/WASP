using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.Exceptions;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientLogInTests
    {

        private static WASPClientBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Admin _admin;
        private string adminpass = "david123";
        private User _member1;
        private User _member2;
        private string mempass = "123456";
        //private Subforum _subforum;

        
        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            _supervisor = ClientFunctions.InitialSystem(_proj);
            var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _member1 = _proj.subscribeToForum(20,"amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456", _forum.id);
            _member2 = _proj.subscribeToForum(21,"edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum.id);

            _proj.login(_admin.user.userName, adminpass, _forum.id,"");
            _proj.login(_member1.userName, mempass, _forum.id, "");
            _proj.login(_member2.userName, mempass, _forum.id, "");
        }

        /// <summary>
        /// Positive Test: log-in of member in forum
        /// </summary>
        [TestMethod]
        public void logInTest1()
        {
            Assert.IsNotNull(_proj.login("amitayaSh", "123456", _forum.id, ""));
            Assert.IsNotNull(_proj.login("edanHb", "123456", _forum.id, ""));
            Assert.IsNotNull(_proj.login(_admin.user.userName, adminpass, _forum.id, ""));
        }

        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void logInTest2()
        {
            _proj.login("", "123456", _forum.id, "");
            
        }

        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest3()
        {
            
            _proj.login("blah", "", _forum.id, "");
        }
        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void logInTest4()
        {
 
            _proj.login("amitayaSh", "", _forum.id, "");
        }
        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest5()
        {
            
            _proj.login("", "123456", _forum.id, "");
            
        }
        /// <summary>
        /// Negative Test: incorrent information
        /// </summary>
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]

        [TestMethod]
        public void logInTest6()
        {
            _proj.login("blah", "123456", _forum.id, "");
            
        }
    }
}
