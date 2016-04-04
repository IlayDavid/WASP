using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using WASP;
using WASP.DataClasses;
using AccTests.Tests;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class AddModeratorTests
    {

        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Member _admin;
        private int _subforumId;
        private Forum _forum;

        [TestFixtureSetUp]

        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }
        [SetUp]

        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);

            //String userName, String name, String email, String pass, Forum memberForum
            _admin = new Member("matansar","matan", "matansar@post.bgu.ac.il", "matan123", null);
            _forum = _proj.createForum(_supervisor, "start-up", "ideas", "admin",
                                            "david", "david@post.bgu.ac.il", "david123");
            Subforum subforum = new Subforum("Calander-Start-Up", new User("moder", "123456", "moder", "moderator@post.bgu.ac.il"));
            _subforumId = _proj.createSubForum(_supervisor._userName, _forumId, subforum);
        }

        /// <summary>
        /// Positive Test:  checks that supervisor can add a moderator to subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [Test]
        public void addModeratorTest1()
        {
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il");
            Assert.Greater(_proj.addModerator(_supervisor._userName,user._userName, _subforumId, new DateTime(2017,1,1)), 0);
            Assert.Equals(_proj.getModerators(_subforumId).Count, 2);
            Assert.Greater(_proj.updateModeratorTerm(_supervisor._userName, user._userName, _subforumId, DateTime.Now.AddDays(10)), 0);
            Assert.Equals(_proj.getModeratorTermTime(user._userName, _subforumId), DateTime.Now.AddDays(10));
        }

        /// <summary>
        /// Positive Test:  checks that admin of a forum can add a moderator to the forum's subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [Test]
        public void addModeratorTest2()
        {
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il"); 
            Assert.Greater(_proj.addModerator(_admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(20)), 0);
            Assert.Equals(_proj.getModerators(_subforumId).Count, 2);
            Assert.Greater(_proj.updateModeratorTerm(_admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(10)), 0);
            Assert.Equals(_proj.getModeratorTermTime(user._userName, _subforumId), DateTime.Now.AddDays(10));
        }
        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that the term time is not updated by unauthorized user
        /// </summary>

        public void addModeratorTest3()
        {
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il");
            Assert.Greater(_proj.addModerator(_admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(20)), 0);
            Assert.LessOrEqual(_proj.updateModeratorTerm("unauthorizedUser", user._userName, _subforumId, DateTime.Now.AddDays(10)), 0);
            Assert.Equals(_proj.getModeratorTermTime(user._userName, _subforumId), DateTime.Now.AddDays(20));
        }

        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        public void addModeratorTest4()
        {
            // creates another forum with his admin
            User admin = new User("NoamB", "123456", "Noam", "NoamB@post.bgu.ac.il");
            Forum forum = new Forum("Philosophia", admin);
            int forumId = _proj.createForum(_supervisor._userName, forum);

            //another admin tries to add a moderator
            User user = new User("amitaySH", "123456", "amitay", "amitaySH@post.bgu.ac.il");
            Assert.LessOrEqual(_proj.addModerator(admin._userName, user._userName, _subforumId, DateTime.Now.AddDays(20)), 0);
        }
    }
}
