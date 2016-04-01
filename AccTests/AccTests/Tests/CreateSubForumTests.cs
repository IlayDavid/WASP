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
    public class CreateSubForumTests
    {



        private WASPBridge _proj;
        private User _supervisor;
        private User _admin;
        private int _forumId;
        private Subforum _subforum;

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
            _subforum = new Subforum("Calander-Start-Up");
        }

        /// <summary>
        /// checks that a supervisor can open a sub forum
        /// </summary>
        [Test]
        public void CreatesubforumTest1()
        {
            Assert.Greater(_proj.createSubForum(_supervisor._userName, _forumId, _subforum) , 0);
        }

        /// <summary>
        /// checks that a admin can open a sub forum
        /// </summary>
        [Test]
        public void CreatesubforumTest2()
        {
            Assert.Greater(_proj.createSubForum(_admin._userName, _forumId, _subforum), 0);
        }

        /// <summary>
        /// checks that can create more then one subforum in the same forum
        /// </summary>
        public void CreatesubforumTest3()
        {

            _admin = new User("matansar", "123456", "matan", "matansar@post.bgu.ac.il");
            Subforum subforum2 = new Subforum("Calander-Start-Up");
            Subforum subforum3 = new Subforum("Soliter-Start-Up");
            bool is_opened = _proj.createSubForum(_supervisor._userName, _forumId, _subforum) > 0 &&
                _proj.createSubForum(_supervisor._userName, _forumId, subforum2) > 0 &&
                _proj.createSubForum(_admin._userName, _forumId, subforum3) > 0;
            Assert.IsTrue(is_opened);
            Assert.Equals(_proj.getSubforums(_forumId).Count, 3);
        }
    }
}
