using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;
using System.Collections.Generic;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientSendPrivateMsgTests
    {

        private SuperUser _supervisor;
        private WASPClientBridge _proj;
        private Forum _forum;
        private User _member1;
        private User _member2;

        [TestInitialize]     //before each Test
        public void SetUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            
            _supervisor = ClientFunctions.InitialSystem(_proj);
            var forumAndMember = ClientFunctions.CreateSpecForum(_proj, _supervisor);

            _forum = forumAndMember.Item1;
            _member1 = _proj.subscribeToForum(50,"amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456",_forum.id, new List<string>(), false);
            _member2 = _proj.subscribeToForum(51,"edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum.id, new List<string>(), false);

            _proj.login(_member1.userName, _member1.password, _forum.id,"");
            _proj.logout();
            _proj.login(_member2.userName, _member2.password, _forum.id,"");
        }

        /*
         * Positive Test: checks that there is one member
         */
        [TestMethod]
        public void sendPrivateMsgTest1()
        {
            var msg = "first message";
            int feedback1 = _proj.sendMessage(_member1.id, msg);
            int feedback2 = _proj.sendMessage(_member2.id, msg);
            _proj.logout();
            Assert.IsTrue(feedback1 >= 0);
            Assert.IsTrue(feedback2 >= 0);
        }

        /*
         * Nagative Test: members in diffrent forums cannot be in touch
         */
        [TestMethod]
        public void sendPrivateMsgTest2()
        {
            string userName = "odedb";
            Forum forum = _proj.createForum( "subject12", "blah",52, userName, "oded",
                            "odedb@post.bgu.ac.il", "odded123", new Policy(5, 5, false, 5, 500));
            var member = _proj.getAdmin(52, forum.id);

            var msg = "first message";
            int feedback1 = _proj.sendMessage( _member1.id, msg);
            int feedback2 = _proj.sendMessage(member.user.id, msg);

            Assert.IsTrue(feedback1 < 0);
            Assert.IsTrue(feedback2 < 0);
        }


        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void sendPrivateMsgTest3()
        {
            var msg = "first message";
            int feedback1 = _proj.sendMessage( _member1.id, null);
            int feedback2 = _proj.sendMessage(-1, msg);
            int feedback3 = _proj.sendMessage(_member1.id, msg);
            Assert.IsTrue(feedback1 >= 0);
            Assert.IsTrue(feedback2 < 0);
            Assert.IsTrue(feedback3 < 0);
        }

    }
}
