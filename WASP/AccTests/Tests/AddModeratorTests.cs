using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
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
        private Member _admin;
        private Forum _forum;
        private Subforum _subforum;
        private Member _moderator;
        private Member _member1;

    
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj,_supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.UserName, _admin.Password, _forum);


            Tuple<Subforum, Member> subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.UserName, _moderator.Password, _forum);


            _member1 = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum);
            _proj.login(_member1.UserName, _member1.Password, _forum);
        }

        /// <summary>
        /// Positive Test:  checks that modirator can add a moderator to subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest1()
        {
            int isModerator = _proj.addModerator(_moderator, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.IsTrue(isModerator >= 0);
            Assert.IsTrue(_proj.getModerators(_admin,_subforum).Count == 2);

            int isModified = _proj.updateModeratorTerm(_moderator,_member1,_subforum, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModerator >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_moderator, _member1, _subforum).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Positive Test:  checks that admin of a forum can add a moderator to the forum's subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest2()
        {
            int isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.IsTrue(isModerator >= 0);
            Assert.IsTrue(_proj.getModerators(_admin, _subforum).Count == 2);

            int isModified = _proj.updateModeratorTerm(_admin, _member1, _subforum, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModerator >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_admin, _member1, _subforum).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest3()
        {

            Forum forum = _proj.createForum(_supervisor, "forum1", "blah", "haaronB",
                                            "haaron", "haaronB@post.bgu.ac.il", "haaron123", new PasswordPolicy());
            Member admin = _proj.getAdmin(_supervisor, forum, "haaronB");
            _proj.login(admin.UserName, admin.Password, _forum);

            //another admin tries to add a moderator
            int isModerator = _proj.addModerator(admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.IsTrue(isModerator > 0);

            isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.IsTrue(isModerator >= 0);

            int isModified = _proj.updateModeratorTerm(admin, _member1, _subforum, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified > 0);
        }

        /* edit name + RTM*/


        /// <summary>
        /// Nagative Test: invalid date time
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest4()
        {
            int isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(-10));
            Assert.IsTrue(isModerator < 0);
            Assert.IsTrue(_proj.getModerators(_admin, _subforum).Count== 1);

            isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            int isModified = _proj.updateModeratorTerm(_admin, _member1, _subforum, DateTime.Now.AddDays(-1));
            Assert.IsTrue(isModified < 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_admin, _member1, _subforum).Date, DateTime.Now.AddDays(200).Date);
        }


        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest5()
        {
            int isModerator = _proj.addModerator(_admin, _member1, null, DateTime.Now);
            Assert.IsTrue(isModerator < 0);

            isModerator = _proj.addModerator(null, _member1, _subforum, DateTime.Now);
            Assert.IsTrue(isModerator < 0);

            isModerator = _proj.addModerator(_admin, null, _subforum, DateTime.Now);
            Assert.IsTrue(isModerator < 0);
        }
    }
}
