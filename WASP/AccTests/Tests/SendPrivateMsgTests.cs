using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class SendPrivateMsgTests
    {

        private WASPBridge _proj;
        private int _forumId;
        private User _member1;
        private User _member2;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
            User supervisor = _proj.initialize();
            
            User admin = new User("matansar", "123456", "matan", "matansar@post.bgu.ac.il");
            Forum forum = new Forum("Start-Up", admin);
            _forumId = _proj.createForum(supervisor._userName, forum);

            _member1 = new User("amitayaSh", "123456", "amitay", "amitayaSh@post.bgu.ac.il");
            _proj.subscribeToForum(_member1, _forumId);

            _member2 = new User("edanHb", "123456", "edan", "edanHb@post.bgu.ac.il");
            _proj.subscribeToForum(_member2, _forumId);
        }

        /*
         * checks that there is one member
         */ 
        [Test]
        public void subscribeToForumTest1()
        {
            Message msg = new Message("first message", "Hi");
            _proj.sendMessage(_member1._userName, _member2._userName, msg);
            _proj.sendMessage(_member2._userName, _member1._userName, msg);
        }

    }
}
