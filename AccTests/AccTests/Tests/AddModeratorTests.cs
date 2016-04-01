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
    public class AddModeratorTests
    {



        private WASPBridge _proj;
        private User _supervisor;
        private User _admin;
        private int _forumId;
        private int _subforumId;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }
        [SetUp]
        public void setUp()
        {
            _supervisor = _proj.initialize();
            _admin = new User("matansar", "123456", "matan", "matansar@post.bgu.ac.il");
            Forum forum = new Forum("Start-Up", _admin);
            _forumId = _proj.createForum(_supervisor._userName, forum);
            Subforum subforum = new Subforum("Calander-Start-Up");
            _subforumId = _proj.createSubForum(_supervisor._userName, _forumId, subforum);
        }

        /// <summary>
        /// checks that supervisor can add a moderator to subforum
        /// </summary>
        [Test]
        public void addModeratorTest1()
        {
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il");
            Assert.Greater(_proj.addModerator(_supervisor._userName,user._userName, _subforumId, new DateTime(2017,1,1)), 0);
            Assert.Greater(_proj.updateModeratorTerm(_supervisor._userName, user._userName, _subforumId, DateTime.Now.AddDays(10)), 0);
        }
        /// <summary>
        /// checks that admin can add a moderator to subforum
        /// </summary>
        [Test]
        public void addModeratorTest2()
        {
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il"); 
            Assert.Greater(_proj.addModerator(_admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(20)), 0);
            Assert.Greater(_proj.updateModeratorTerm(_admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(10)), 0);
        }
    }
}
