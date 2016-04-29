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
            Assert.IsTrue(false);

        }


        /// <summary>
        /// negative Test:  checks that admin of a forum can't delete a moderator another admin added
        /// </summary>
        [TestMethod]
        public void deleteModerator2()
        {
            Assert.IsTrue(false);
        }


        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        [TestMethod]
        public void deleteModerator3()
        {

            Assert.IsTrue(false);
        }


        /// <summary>
        /// Nagative Test: attempts to delete last moderator in a subforum
        /// </summary>
        [TestMethod]
        public void deleteModerator4()
        {
            Assert.IsTrue(false);
        }

    }
}
