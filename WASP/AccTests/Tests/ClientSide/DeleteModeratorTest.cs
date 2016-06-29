using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests
{
    [TestClass]
    class ClientDeleteModeratorTest
    {
        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        private Admin _admin;
        private Forum _forum;
        private Subforum _subforum;
        private User _moderator;
        private User _moderator2;

        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            _supervisor = ClientFunctions.InitialSystem(_proj);

            var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, _admin.user.password, _forum.id, "");


            var subforumAndModerator = ClientFunctions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.userName, _moderator.password, _forum.id, "");


            _moderator2 = _proj.subscribeToForum(16,"mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum.id);
            _proj.addModerator(_moderator2.id, _subforum.id, DateTime.MaxValue);
            _proj.login(_moderator2.userName, _moderator2.password, _forum.id, "");
        }
        /// <summary>
        /// Positive Test:  checks that admin can delete a moderator to subforum
        /// </summary>
        [TestMethod]
        public void deleteModerator1()
        {
            var check = _proj.deleteModerator(_moderator.id, _subforum.id);
            Assert.IsTrue(check>=0);
            var numMods = _proj.getModerators( _subforum.id);
            Assert.IsTrue(numMods.Count==1);
            var firedMod = _proj.login(_moderator.userName, _moderator.password, _forum.id, "");
            Assert.IsNotNull(firedMod);
        }


        /// <summary>
        /// negative Test:  checks that admin of a forum can't delete a moderator another admin added
        /// </summary>
        [TestMethod]
        public void deleteModerator2()
        {
            var newAdminUser = _proj.subscribeToForum(17,"admin2", "admin2", "dmin@ds.ds", "zzzz222", _forum.id);
            _proj.addAdmin(newAdminUser.id);
            var check = _proj.deleteModerator(_moderator.id, _subforum.id);
                Assert.IsTrue(check<0);
            
        }


        /// <summary>
        /// Nagative Test: checks that you cannot delete the last moderator
        /// </summary>
        [TestMethod]
        public void deleteModerator3()
        {
            var check = _proj.deleteModerator( _moderator.id, _subforum.id);
            Assert.IsTrue(check >= 0);
            check = _proj.deleteModerator( _moderator2.id, _subforum.id);
            Assert.IsTrue(check < 0);
            var numMods = _proj.getModerators( _subforum.id);
            Assert.IsTrue(numMods.Count == 1);
            var firedMod = _proj.login(_moderator.userName, _moderator.password, _forum.id, "");
            Assert.IsNotNull(firedMod);
            var mod = _proj.login(_moderator2.userName, _moderator2.password, _forum.id, "");
            Assert.IsNotNull(mod);
            Assert.IsTrue(_proj.getModerators(_subforum.id).Any((x) => x.user.id == _moderator2.id));
        }


    }
}
