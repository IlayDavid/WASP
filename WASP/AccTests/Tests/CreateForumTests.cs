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
    public class CreateForumTests
    {

        private WASPBridge _proj;
        private User _supervisor;
        private User _admin;
        private Forum _forum;

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
            _forum = new Forum("Start-Up", _admin);
        }

        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that there is only one admin
        /// </summary>
        [Test]
        public void CreateForumTest1()
        {
            int forumId = _proj.createForum(_supervisor._userName, _forum);
            Assert.Greater( forumId, 0); //checks that a forum is created
            List<User> admins = _proj.getAdmins(forumId);
            Assert.Equals(admins.Count, 1); // checks that there is only one admin
            
        }


        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that the user which should be a admin, is it
        /// </summary>
        [Test]
        public void CreateForumTest2()
        {
            int forumId = _proj.createForum(_supervisor._userName, _forum);
            Assert.Greater(forumId, 0); //checks that a forum is created
            List<User> admins = _proj.getAdmins(forumId);
            Assert.Equals(admins.Contains(_admin), true); // checks that the user added as admin         
        }

        /// <summary>
        /// Positive Test, NF - load Test
        /// </summary>
        public void CreateForumTest3()
        {
            int N = 500;
            for (int i = 1; i <= N; i++)
            {
                User admin = new User("user" + i.ToString(), "123456",
                                      "user" + i.ToString(),
                                      "user" + i.ToString() +"@post.bgu.ac.il");
                Forum forum = new Forum("Start-Up", admin);
                _proj.createForum(_supervisor._userName, forum);
            }
            Assert.Equals(_proj.getAllForums().Count, N);
        }

        /// <summary>
        /// Nagative Test, NF - secure Test
        /// </summary>
        public void CreateForumTest4()
        {
            int forumId = _proj.createForum("UnauthorizedUser", _forum);
            Assert.LessOrEqual(forumId, 0);
        }

    }
}
