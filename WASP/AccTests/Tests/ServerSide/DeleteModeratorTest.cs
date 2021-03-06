﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

namespace AccTests.Tests
{
    [TestClass]
    class DeleteModeratorTest
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Admin _admin;
        private Forum _forum;
        private Subforum _subforum;
        private User _moderator;
        private User _moderator2;

        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            var forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.User.Username, _admin.User.Password, _forum.Id);


            var subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.Username, _moderator.Password, _forum.Id);


            _moderator2 = _proj.subscribeToForum(16,"mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum.Id);
            _proj.addModerator(_admin.User.Id, _forum.Id, _moderator2.Id, _subforum.Id, DateTime.MaxValue);
            _proj.login(_moderator2.Username, _moderator2.Password, _forum.Id);
        }
        /// <summary>
        /// Positive Test:  checks that admin can delete a moderator to subforum
        /// </summary>
        [TestMethod]
        public void deleteModerator1()
        {
            var check = _proj.deleteModerator(_admin.User.Id, _forum.Id, _moderator.Id, _subforum.Id);
            Assert.IsTrue(check>=0);
            var numMods = _proj.getModerators(_admin.User.Id, _subforum.Id);
            Assert.IsTrue(numMods.Length==1);
            var firedMod = _proj.login(_moderator.Username, _moderator.Password, _forum.Id);
            Assert.IsNotNull(firedMod);
        }


        /// <summary>
        /// negative Test:  checks that admin of a forum can't delete a moderator another admin added
        /// </summary>
        [TestMethod]
        public void deleteModerator2()
        {
            var newAdminUser = _proj.subscribeToForum(17,"admin2", "admin2", "dmin@ds.ds", "zzzz222", _forum.Id);
            _proj.addAdmin(_supervisor.Id,_forum.Id, newAdminUser.Id);
            var check = _proj.deleteModerator(newAdminUser.Id, _forum.Id, _moderator.Id, _subforum.Id);
                Assert.IsTrue(check<0);
            
        }


        /// <summary>
        /// Nagative Test: checks that you cannot delete the last moderator
        /// </summary>
        [TestMethod]
        public void deleteModerator3()
        {
            var check = _proj.deleteModerator(_admin.User.Id, _forum.Id, _moderator.Id, _subforum.Id);
            Assert.IsTrue(check >= 0);
            check = _proj.deleteModerator(_admin.User.Id, _forum.Id, _moderator2.Id, _subforum.Id);
            Assert.IsTrue(check < 0);
            var numMods = _proj.getModerators(_admin.User.Id, _subforum.Id);
            Assert.IsTrue(numMods.Length == 1);
            var firedMod = _proj.login(_moderator.Username, _moderator.Password, _forum.Id);
            Assert.IsNotNull(firedMod);
            var mod = _proj.login(_moderator2.Username, _moderator2.Password, _forum.Id);
            Assert.IsNotNull(mod);
            Assert.IsTrue(_proj.getModerators(_admin.User.Id, _subforum.Id).Any((x) => x.User.Id == _moderator2.Id));
        }


    }
}
