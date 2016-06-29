using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CreateSubForumTests
    {

        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Admin _admin;
        private string[] arr = { "shlomo", "moshe" };

        //private Subforum _subforum;

        
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _proj.Clean();
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Admin> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.User.Username, _admin.User.Password, _forum.Id);
        }

        /// <summary>
        /// checks that a admin can open a sub forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest1()
        {
            var moderator = _proj.subscribeToForum(11,"maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum.Id, arr, false);
            Subforum subforum = _proj.createSubForum(_admin.User.Id,_forum.Id, "subject2", "blah blah blah", moderator.Id, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum);
            Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id).Length, 1);
            Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].User.Username, moderator.Username);
            Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].User.Name, moderator.Name);
            Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].User.Password, moderator.Password);
            Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].User.Email, moderator.Email);
            //Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].MemberForum, moderator.MemberForum);
        }

        /// <summary>
        /// checks that can create more then one subforum in the same forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest2()
        {
            var moderator1 = _proj.subscribeToForum(12,"maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum.Id, arr, false);
            var moderator2 = _proj.subscribeToForum(13,"yaelp", "yael", "yaelp@post.bgu.ac.il", "yaelp123", _forum.Id, arr, false);
            Subforum subforum1 = _proj.createSubForum(_admin.User.Id, _forum.Id, "subject1", "blah blah blah", moderator1.Id, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum(_admin.User.Id, _forum.Id, "subject2", "blah blah blah", moderator2.Id, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum1);
            Assert.IsNotNull(subforum2);
            Assert.AreEqual(_proj.getSubforums(_forum.Id).Length, 2);
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

                var moderator = _proj.subscribeToForum(500+i,"moderator"+i.ToString(),
                        "moderator", "moderator"+i.ToString()+"@post.bgu.ac.il", 
                        "moderator".ToString(), _forum.Id, arr, false);
                Subforum subforum = _proj.createSubForum(_admin.User.Id, _forum.Id, "subject" + i.ToString(), 
                        "blah blah blah", moderator.Id, DateTime.Now.AddDays(100));

                Assert.IsNotNull(subforum);
                Assert.AreEqual(_proj.getModerators(_admin.User.Id, subforum.Id)[0].User.Username, moderator.Username);
                Assert.AreEqual(_proj.getModerators(_admin.User.Id,subforum.Id).Length, 1);
            }

            Assert.AreEqual(_proj.getSubforums( _forum.Id).Length, N);
        }

        /// <summary>
        /// Nagative test - NF secure test: admin which is not responsible 
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest4()
        {
            var forumAndModerator = Functions.CreateSpecForum2(_proj, _supervisor);
            Forum forum = forumAndModerator.Item1;
            var admin = forumAndModerator.Item2;

            var moderator1 = _proj.subscribeToForum(1782461,"bogo", "bigi", "baga@post.bgu.ac.il", "maor123", _forum.Id, arr, false);
            var moderator2 = _proj.subscribeToForum(988,"maorh", "maor", "maorh@post.bgu.ac.il", "maor123", forum.Id, arr, false);
            _proj.login(moderator1.Username, moderator1.Password, _forum.Id);
            _proj.login(moderator2.Username, moderator2.Password, forum.Id);
            // lacking of informations 
            Subforum subforum1 = _proj.createSubForum(_admin.User.Id, _forum.Id, "", "blah", moderator1.Id, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum(_admin.User.Id, _forum.Id,"blah", "", moderator1.Id, DateTime.Now.AddDays(100));

            // fails because moderator2 is not member at _admin's forum
            Subforum subforum3 = _proj.createSubForum(_admin.User.Id, _forum.Id, "blah", "", moderator2.Id, DateTime.Now.AddDays(100));

            Subforum subforum4 = _proj.createSubForum(admin.User.Id, forum.Id, "blah", "blah", moderator1.Id, DateTime.Now.AddDays(100));

            Assert.IsNull(subforum1);
            Assert.IsNull(subforum2);
            Assert.IsNull(subforum3);
            Assert.IsNotNull(subforum4);
        }
    }
}
