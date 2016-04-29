using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace AccTests.Tests
{
    [TestClass]
    class DeleteModeratorTest
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Member _admin;
        private Forum _forum;
        private Subforum _subforum;
        private Member _moderator;
        private Member _moderator2;

        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.UserName, _admin.Password, _forum);


            Tuple<Subforum, Member> subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.UserName, _moderator.Password, _forum);


            _moderator2 = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum);
            _proj.addModerator(_admin, _moderator2, _subforum, DateTime.MaxValue);
            _proj.login(_moderator2.UserName, _moderator2.Password, _forum);
        }
        /// <summary>
        /// Positive Test:  checks that admin can delete a moderator to subforum
        /// </summary>
        [TestMethod]
        public void deleteModerator1()
        {
            var check=_proj.fireModerator(_admin, _moderator);
            Assert.IsTrue(check>=0);
            var numMods = _proj.getModerators(_admin, _subforum);
            Assert.IsTrue(numMods.Count==1);
            var firedMod = _proj.login(_moderator.UserName, _moderator.Password, _forum);
            Assert.IsNotNull(firedMod);
        }


        /// <summary>
        /// negative Test:  checks that admin of a forum can't delete a moderator another admin added
        /// </summary>
        [TestMethod]
        public void deleteModerator2()
        {
            var newAdmin = _proj.subscribeToForum("admin2", "admin2", "dmin@ds.ds", "zzzz222", _forum);
            _proj.addAdmin(_supervisor, newAdmin);
                var check = _proj.fireModerator(newAdmin, _moderator);
                Assert.IsTrue(check<0);
            
        }


        /// <summary>
        /// Nagative Test: checks that you cannot delete the last moderator
        /// </summary>
        [TestMethod]
        public void deleteModerator3()
        {
            var check = _proj.fireModerator(_admin, _moderator);
            Assert.IsTrue(check >= 0);
            check = _proj.fireModerator(_admin, _moderator2);
            Assert.IsTrue(check < 0);
            var numMods = _proj.getModerators(_admin, _subforum);
            Assert.IsTrue(numMods.Count == 1);
            var firedMod = _proj.login(_moderator.UserName, _moderator.Password, _forum);
            Assert.IsNotNull(firedMod);
            var mod = _proj.login(_moderator2.UserName, _moderator2.Password, _forum);
            Assert.IsNotNull(mod);
            Assert.IsTrue(_proj.getModerators(_admin, _subforum).Any((x) => x.Name == _moderator2.Name));
        }


    }
}
