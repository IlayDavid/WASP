﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AddModeratorTests
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Admin _admin;
        private Forum _forum;
        private Subforum _subforum;
        private User _moderator;
        private User _member1;

    
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            var forumAndAdmin = Functions.CreateSpecForum(_proj,_supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, _admin.user.password, _forum.Id);


            var subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, _moderator.password, _forum.Id);


            _member1 = _proj.subscribeToForum(7,"mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum.Id);
            _proj.login(_member1.userName, _member1.password, _forum.Id);
        }

        /// <summary>
        /// Positive Test:  checks that modirator can add a moderator to subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest1()
        {
            var isModerator = _proj.addModerator(_moderator.id,_forum.Id,_member1.id,_subforum.Id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);
            Assert.IsTrue(_proj.getModerators(_moderator.id,_subforum.Id).Count == 2);

            int isModified = _proj.updateModeratorTerm(_moderator.id,_forum.Id,_member1.id,_subforum.Id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_moderator.id,_forum.Id,_member1.id, _subforum.Id).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Positive Test:  checks that admin of a forum can add a moderator to the forum's subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest2()
        {
            var isModerator = _proj.addModerator(_admin.user.id, _forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);
            Assert.IsTrue(_proj.getModerators(_admin.user.id, _subforum.Id).Count == 2);

            int isModified = _proj.updateModeratorTerm(_admin.user.id, _forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_admin.user.id, _forum.Id, _member1.id, _subforum.Id).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest3()
        {

            Forum forum = _proj.createForum(_supervisor.id, "forum1", "blah",8, "haaronB",
                                            "haaron", "haaronB@post.bgu.ac.il", "haaron123", new PasswordPolicy());
            Admin admin = _proj.getAdmin(_supervisor.id, forum.Id, 8);
            _proj.login(admin.user.userName, admin.user.password, _forum.Id);

            //another admin tries to add a moderator
            var isModerator = _proj.addModerator(admin.user.id,_forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(200));
            Assert.IsNull(isModerator );

            isModerator = _proj.addModerator(_admin.user.id,_forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);

            int isModified = _proj.updateModeratorTerm(admin.user.id,_forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified < 0);
        }

        /* edit name + RTM*/


        /// <summary>
        /// Nagative Test: invalid date time
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest4()
        {
            var isModerator = _proj.addModerator(_admin.user.id,_forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(-10));
            Assert.IsNull(isModerator);
            Assert.IsTrue(_proj.getModerators(_admin.user.id, _subforum.Id).Count== 1);

            isModerator = _proj.addModerator(_admin.user.id, _forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(200));
            int isModified = _proj.updateModeratorTerm(_admin.user.id, _forum.Id, _member1.id, _subforum.Id, DateTime.Now.AddDays(-1));
            Assert.IsTrue(isModified < 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_admin.user.id,_forum.Id, _member1.id, _subforum.Id).Date, DateTime.Now.AddDays(200).Date);
        }


        /// <summary>
        /// Nagative Test: bad information
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest5()
        {
            var isModerator = _proj.addModerator(_admin.user.id,_forum.Id, _member1.id, -1, DateTime.Now);
            Assert.IsNull(isModerator);

            isModerator = _proj.addModerator(-1, _forum.Id, _member1.id, _subforum.Id, DateTime.Now);
            Assert.IsNull(isModerator);

            isModerator = _proj.addModerator(_admin.user.id, _forum.Id, - 1, _subforum.Id, DateTime.Now);
            Assert.IsNull(isModerator);
        }
    }
}
