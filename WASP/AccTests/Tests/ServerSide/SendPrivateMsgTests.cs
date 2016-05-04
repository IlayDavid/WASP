using System;
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
        private WASPBridge _proj;
        private Forum _forum;
        private User _member1;
        private User _member2;

        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            var forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);

            _forum = forumAndMember.Item1;
            _member1 = _proj.subscribeToForum(50,"amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456",_forum.Id);
            _member2 = _proj.subscribeToForum(51,"edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum.Id);

            _proj.login(_member1.userName, _member1.password, _forum.Id);
            _proj.login(_member2.userName, _member2.password, _forum.Id);
        }

        /*
         * Positive Test: checks that there is one member
         */
        [TestMethod]
        public void sendPrivateMsgTest1()
        {
            var msg = "first message";
            int feedback1 = _proj.sendMessage(_member2.id,_forum.Id, _member1.id.ToString(), msg);
            int feedback2 = _proj.sendMessage(_member1.id,_forum.Id, _member2.id.ToString(), msg);

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
            Forum forum = _proj.createForum(_supervisor.id, "subject12", "blah",52, userName, "oded",
                            "odedb@post.bgu.ac.il", "odded123", new PasswordPolicy());
            var member = _proj.getAdmin(_supervisor.id, forum.Id, 52);

            var msg = "first message";
            int feedback1 = _proj.sendMessage(member.user.id,forum.Id, _member1.id.ToString(), msg);
            int feedback2 = _proj.sendMessage(_member1.id,_forum.Id, member.user.id.ToString(), msg);

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
            int feedback1 = _proj.sendMessage(_member2.id,_forum.Id, _member1.id.ToString(), null);
            int feedback2 = _proj.sendMessage(_member1.id,_forum.Id, null, msg);
            int feedback3 = _proj.sendMessage(-1,_forum.Id, _member1.id.ToString(), msg);
            Assert.IsTrue(feedback1 >= 0);
            Assert.IsTrue(feedback2 < 0);
            Assert.IsTrue(feedback3 < 0);
        }

    }
}
