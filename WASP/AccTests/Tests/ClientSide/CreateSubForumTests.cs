using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;
using System.Collections.Generic;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientCreateSubForumTests
    {

        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Admin _admin;
        private string adminpass = "david123";

        //private Subforum _subforum;


        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();

            _supervisor = ClientFunctions.InitialSystem(_proj);
            Tuple<Forum, Admin> forumAndAdmin = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.user.userName, adminpass, _forum.id, "");
        }

        /// <summary>
        /// checks that a admin can open a sub forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest1()
        {
            var moderator = _proj.subscribeToForum(11, "maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum.id, new List<string>(), false);
            Subforum subforum = _proj.createSubForum("subject2", "blah blah blah", moderator.id, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum);
            Assert.AreEqual(_proj.getModerators(subforum.id).Count, 1);
            Assert.AreEqual(_proj.getModerators(subforum.id)[0].user.id, moderator.id);
            Assert.AreEqual(_proj.getModerators(subforum.id)[0].appointBy.id, _admin.user.id);
            // Assert.AreEqual(_proj.getModerators(subforum.id)[0].user.password, moderator.password);
            //Assert.AreEqual(_proj.getModerators(subforum.id)[0].user.email, moderator.email);
            //Assert.AreEqual(_proj.getModerators(_admin.user.id, subforum.id)[0].MemberForum, moderator.MemberForum);
        }

        /// <summary>
        /// checks that can create more then one subforum in the same forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest2()
        {
            var moderator1 = _proj.subscribeToForum(12, "maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum.id, new List<string>(), false);
            var moderator2 = _proj.subscribeToForum(13, "yaelp", "yael", "yaelp@post.bgu.ac.il", "yaelp123", _forum.id, new List<string>(), false);
            Subforum subforum1 = _proj.createSubForum("subject1", "blah blah blah", moderator1.id, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum("subject2", "blah blah blah", moderator2.id, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum1);
            Assert.IsNotNull(subforum2);
            Assert.AreEqual(_proj.getSubforums(_forum.id).Count, 2);
        }

        /// <summary>
        /// Positive test - NF load test
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest3()
        {
            int N = 50;
            for (int i = 1; i <= N; i++)
            {

                var moderator = _proj.subscribeToForum(500 + i, "moderator" + i.ToString(),
                        "moderator", "moderator" + i.ToString() + "@post.bgu.ac.il",
                        "moderator".ToString(), _forum.id, new List<string>(), false);
                Subforum subforum = _proj.createSubForum("subject" + i.ToString(),
                        "blah blah blah", moderator.id, DateTime.Now.AddDays(100));

                Assert.IsNotNull(subforum);
                Assert.AreEqual(_proj.getModerators(subforum.id)[0].user.id, moderator.id);
                Assert.AreEqual(_proj.getModerators(subforum.id).Count, 1);
            }

            Assert.AreEqual(_proj.getSubforums(_forum.id).Count, N);
        }

        /// <summary>
        /// Nagative test - NF secure test: admin which is not responsible 
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest4()
        {
            var forumAndModerator = ClientFunctions.CreateSpecForum2(_proj, _supervisor);
            Forum forum = forumAndModerator.Item1;
            var admin = forumAndModerator.Item2;

            var moderator1 = _proj.subscribeToForum(1782461, "bogo", "bigi", "baga@post.bgu.ac.il", "maor123", _forum.id, new List<string>(), false);
            var moderator2 = _proj.subscribeToForum(988, "maorh", "maor", "maorh@post.bgu.ac.il", "maor123", forum.id, new List<string>(), false);
            _proj.login(moderator1.userName, moderator1.password, _forum.id, "");
            _proj.login(moderator2.userName, moderator2.password, forum.id, "");
            // lacking of informations 
            Subforum subforum1 = _proj.createSubForum("", "blah", moderator1.id, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum("blah", "", moderator1.id, DateTime.Now.AddDays(100));

            // fails because moderator2 is not member at _admin's forum
            Subforum subforum3 = _proj.createSubForum("blah", "", moderator2.id, DateTime.Now.AddDays(100));

            Subforum subforum4 = _proj.createSubForum("blah", "blah", moderator1.id, DateTime.Now.AddDays(100));

            Assert.IsNull(subforum1);
            Assert.IsNull(subforum2);
            Assert.IsNull(subforum3);
            Assert.IsNotNull(subforum4);
        }
    }
}
