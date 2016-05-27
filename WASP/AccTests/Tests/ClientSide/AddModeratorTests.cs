using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.Exceptions;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AddClientModeratorTests
    {
        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        private String supass="moshe123";
        private Admin _admin;
        private String adminpass="david123";
        private Forum _forum;
        private Subforum _subforum;
        private User _moderator;
        private String modpass="ilan123";
        private User _member1;
        private String mempass="mem123";


        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            _supervisor = ClientFunctions.InitialSystem(_proj); //password is moshe123

            var forumAndAdmin = ClientFunctions.CreateSpecForum(_proj,_supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2; //password is david123
            _proj.login(_admin.user.userName, adminpass, _forum.id);


            var subforumAndModerator = ClientFunctions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2; //password is ilan123
            _proj.login(_moderator.userName, modpass, _forum.id);


            _member1 = _proj.subscribeToForum(7,"mem1", "mem", "mem1@post.bgu.ac.il", "mem123", _forum.id); //password is ilan123
            _proj.login(_member1.userName, mempass, _forum.id);
        }

        /// <summary>
        /// Positive Test:  checks that modirator can add a moderator to subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest1()
        {
            _proj.login(_admin.user.userName, adminpass, _forum.id);
            var isModerator = _proj.addModerator(_member1.id,_subforum.id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);
            Assert.IsTrue(_proj.getModerators(_subforum.id).Count == 2);

            int isModified = _proj.updateModeratorTerm(_subforum.id, _member1.id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_member1.id, _subforum.id).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Positive Test:  checks that admin of a forum can add a moderator to the forum's subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [TestMethod]
        public void addModeratorAndUpdateTermTest2()
        {
            var isModerator = _proj.addModerator( _member1.id, _subforum.id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);
            Assert.IsTrue(_proj.getModerators(_subforum.id).Count == 2);

            int isModified = _proj.updateModeratorTerm( _member1.id, _subforum.id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified >= 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_member1.id, _subforum.id).Date, DateTime.Now.AddDays(100).Date);
        }


        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void addModeratorAndUpdateTermTest3()
        {

            Forum forum = _proj.createForum( "forum1", "blah",8, "haaronB",
                                            "haaron", "haaronB@post.bgu.ac.il", "haaron123", new Policy(5, 5, false, 5, 500));
            Admin admin = _proj.getAdmin(8, forum.id);
            _proj.login(admin.user.userName, admin.user.password, forum.id);

            //another admin tries to add a moderator
            var isModerator = _proj.addModerator(_member1.id, _subforum.id, DateTime.Now.AddDays(200));
            Assert.IsNull(isModerator );

            isModerator = _proj.addModerator(_member1.id, _subforum.id, DateTime.Now.AddDays(200));
            Assert.IsNotNull(isModerator);

            int isModified = _proj.updateModeratorTerm(_member1.id, _subforum.id, DateTime.Now.AddDays(100));
            Assert.IsTrue(isModified < 0);
        }

        /* edit name + RTM*/


        /// <summary>
        /// Nagative Test: invalid date time
        /// </summary>
       [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        [TestMethod]
        public void addModeratorAndUpdateTermTest4()
        {
            var isModerator = _proj.addModerator(_member1.id, _subforum.id, DateTime.Now.AddDays(-10));
            var check = _proj.getModerators(_subforum.id).Count;
            Assert.IsTrue(check== 1);

            isModerator = _proj.addModerator(_member1.id, _subforum.id, DateTime.Now.AddDays(200));
            int isModified = _proj.updateModeratorTerm(_member1.id, _subforum.id, DateTime.Now.AddDays(-1));
            Assert.IsTrue(isModified == 0);
            Assert.AreEqual(_proj.getModeratorTermTime(_member1.id, _subforum.id).Date, DateTime.Now.AddDays(200).Date);
        }


        /// <summary>
        /// Nagative Test: bad information
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WaspException), AllowDerivedTypes = true)]
        public void addModeratorAndUpdateTermTest5()
        {
            var isModerator = _proj.addModerator(_member1.id, -1, DateTime.Now);
            Assert.IsNull(isModerator);

            isModerator = _proj.addModerator(_member1.id, _subforum.id, DateTime.Now);
            Assert.IsNull(isModerator);

            isModerator = _proj.addModerator(-1, _subforum.id, DateTime.Now);
            Assert.IsNull(isModerator);
        }
    }
}
