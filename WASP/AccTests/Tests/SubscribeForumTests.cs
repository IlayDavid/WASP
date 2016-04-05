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
    public class SubscribeForumTests
    {

        private WASPBridge _proj;
        private int _forumId;

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
        }

        /*
         * checks that there is one member
         */ 
        [Test]
        public void subscribeToForumTest1()
        {
            User member = new User("amitayaSh", "123456", "amitay", "amitayaSh@post.bgu.ac.il");
            _proj.subscribeToForum(member,_forumId);
            List<User> members = _proj.getMembers(_forumId);
            Assert.Equals(members.Count, 1);
        }

        /*
         * checks that the user who should be added, is it
         */
        [Test]
        public void subscribeToForumTest2()
        {
            User member = new User("amitayaSh", "123456", "amitay", "amitayaSh@post.bgu.ac.il");
            _proj.subscribeToForum(member, _forumId);
            List<User> members = _proj.getMembers(_forumId);
            Assert.IsTrue(members.Contains(member));
        }
    }
}
