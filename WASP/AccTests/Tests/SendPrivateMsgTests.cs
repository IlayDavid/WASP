using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using WASP;
using WASP.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class SendPrivateMsgTests
    {

        private SuperUser _supervisor;
        private WASPBridge _proj;
        private Forum _forum;
        private Member _member1;
        private Member _member2;

        [OneTimeSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum,Member> forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);

            _forum = forumAndMember.Item1;
            _member1 = _proj.subscribeToForum("amitayaSh", "amitay", "amitayaSh@post.bgu.ac.il", "123456",_forum);
            _member2 = _proj.subscribeToForum("edanHb", "edan", "edanHb@post.bgu.ac.il", "123456", _forum);
        }

        /*
         * Positive Test: checks that there is one member
         */ 
        [Test]
        public void sendPrivateMsgTest1()
        {
            Message msg = new Message("first message", "Hi");
            int feedback1 = _proj.sendMessage(_member2, _member1, msg);
            int feedback2 = _proj.sendMessage(_member1, _member2, msg);

            Assert.GreaterOrEqual(feedback1, 0);
            Assert.GreaterOrEqual(feedback1, 0);
        }

        /*
         * Nagative Test: members in diffrent forums cannot be in touch
         */
        [Test]
        public void sendPrivateMsgTest2()
        {
            string userName = "odedb";
            Forum forum = _proj.createForum(_supervisor, "subject12", "blah", userName, "oded",
                            "odedb@post.bgu.ac.il", "odded123");
            Member member = _proj.getAdmin(_supervisor, forum, userName);

            Message msg = new Message("first message", "Hi");
            int feedback1 = _proj.sendMessage(member, _member1, msg);
            int feedback2 = _proj.sendMessage(_member1, member, msg);

            Assert.Less(feedback1, 0);
            Assert.Less(feedback1, 0);
        }

    }
}
