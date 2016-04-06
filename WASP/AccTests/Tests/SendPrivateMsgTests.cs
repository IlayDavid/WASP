﻿using System;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SendPrivateMsgTests
    {

        private SuperUser _supervisor;
        private static WASPBridge _proj = Driver.getBridge();
        private Forum _forum;
        private Member _member1;
        private Member _member2;

        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum,Member> forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);

            _forum = forumAndMember.Item1;
            _member1 = _proj.subscribeToForum("amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456",_forum);
            _member2 = _proj.subscribeToForum("edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum);

            _proj.login(_member1.UserName, _member1.Password, _forum);
            _proj.login(_member2.UserName, _member2.Password, _forum);
        }

        /*
         * Positive Test: checks that there is one member
         */
        [TestMethod]
        public void sendPrivateMsgTest1()
        {
            Message msg = new Message("first message", "Hi");
            int feedback1 = _proj.sendMessage(_member2, _member1, msg);
            int feedback2 = _proj.sendMessage(_member1, _member2, msg);

            Assert.IsTrue(feedback1 >= 0);
            Assert.IsTrue(feedback1 >= 0);
        }

        /*
         * Nagative Test: members in diffrent forums cannot be in touch
         */
        [TestMethod]
        public void sendPrivateMsgTest2()
        {
            string userName = "odedb";
            Forum forum = _proj.createForum(_supervisor, "subject12", "blah", userName, "oded",
                            "odedb@post.bgu.ac.il", "odded123", new PasswordPolicy());
            Member member = _proj.getAdmin(_supervisor, forum, userName);

            Message msg = new Message("first message", "Hi");
            int feedback1 = _proj.sendMessage(member, _member1, msg);
            int feedback2 = _proj.sendMessage(_member1, member, msg);

            Assert.IsTrue(feedback1 < 0);
            Assert.IsTrue(feedback1 < 0);
        }


        /// <summary>
        /// Negative Test: lack of information
        /// </summary>
        [TestMethod]
        public void sendPrivateMsgTest3()
        {
            Message msg = new Message("first message", "Hi");
            int feedback1 = _proj.sendMessage(_member2, _member1, null);
            int feedback2 = _proj.sendMessage(_member1, null, msg);
            int feedback3 = _proj.sendMessage(null, _member1, msg);
            Assert.IsTrue(feedback1 < 0);
            Assert.IsTrue(feedback2 < 0);
            Assert.IsTrue(feedback3 < 0);
        }

    }
}
