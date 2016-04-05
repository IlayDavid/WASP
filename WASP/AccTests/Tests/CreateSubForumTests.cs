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
        private Member _admin;

        //private Subforum _subforum;

        [AssemblyInitialize]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [ClassInitialize]
        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.UserName, _admin.Password, _forum);
        }

        /// <summary>
        /// checks that a admin can open a sub forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest1()
        {
            Member moderator = _proj.subscribeToForum("maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum);
            Subforum subforum = _proj.createSubForum(_admin, "subject2", "blah blah blah", moderator, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum);
            Assert.Equals(_proj.getModerators(_admin, subforum).Count, 1);
            Assert.Equals(_proj.getModerators(_admin, subforum)[0].UserName, moderator.UserName);
            Assert.Equals(_proj.getModerators(_admin, subforum)[0].Name, moderator.Name);
            Assert.Equals(_proj.getModerators(_admin, subforum)[0].Password, moderator.Password);
            Assert.Equals(_proj.getModerators(_admin, subforum)[0].Email, moderator.Email);
            Assert.Equals(_proj.getModerators(_admin, subforum)[0].MemberForum, moderator.MemberForum);
        }

        /// <summary>
        /// checks that can create more then one subforum in the same forum
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest2()
        {
            Member moderator1 = _proj.subscribeToForum("maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum);
            Member moderator2 = _proj.subscribeToForum("yaelp", "yael", "yaelp@post.bgu.ac.il", "yaelp123", _forum);
            Subforum subforum1 = _proj.createSubForum(_admin, "subject1", "blah blah blah", moderator1, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum(_admin, "subject2", "blah blah blah", moderator2, DateTime.Now.AddDays(100));

            Assert.IsNotNull(subforum1);
            Assert.IsNotNull(subforum2);
            Assert.Equals(_proj.getSubforums(_admin, _forum).Count, 2);
        }

        /// <summary>
        /// Positive test - NF load test
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest3()
        {
            int N = 500;
            for (int i = 1; i <= N; i++)
            {

                Member moderator = _proj.subscribeToForum("moderator"+i.ToString(),
                        "moderator", "moderator"+i.ToString()+"@post.bgu.ac.il", 
                        "moderator".ToString(), _forum);
                Subforum subforum = _proj.createSubForum(_admin, "subject" + i.ToString(), 
                        "blah blah blah", moderator, DateTime.Now.AddDays(100));

                Assert.IsNotNull(subforum);
                Assert.Equals(_proj.getModerators(_admin, subforum)[0].UserName, moderator.UserName);
                Assert.Equals(_proj.getModerators(_admin,subforum).Count, 1);
            }

            Assert.Equals(_proj.getSubforums(_admin, _forum).Count, N);
        }

        /// <summary>
        /// Nagative test - NF secure test: admin which is not responsible 
        /// </summary>
        [TestMethod]
        public void CreatesubforumTest4()
        {
            Tuple<Forum,Member> forumAndModerator = Functions.CreateSpecForum2(_proj, _supervisor);
            Forum forum = forumAndModerator.Item1;
            Member admin = forumAndModerator.Item2;

            Member moderator1 = _proj.subscribeToForum("maorh", "maor", "maorh@post.bgu.ac.il", "maor123", _forum);
            Member moderator2 = _proj.subscribeToForum("maorh", "maor", "maorh@post.bgu.ac.il", "maor123", forum);
            _proj.login(moderator1.UserName, moderator1.Password, _forum);
            _proj.login(moderator2.UserName, moderator2.Password, _forum);
            // lacking of informations
            Subforum subforum1 = _proj.createSubForum(_admin, "", "blah", moderator1, DateTime.Now.AddDays(100));
            Subforum subforum2 = _proj.createSubForum(_admin, "blah", "", moderator1, DateTime.Now.AddDays(100));

            // fails because moderator2 is not member at _admin's forum
            Subforum subforum3 = _proj.createSubForum(_admin, "blah", "", moderator2, DateTime.Now.AddDays(100));

            Subforum subforum4 = _proj.createSubForum(admin, "blah", "blah", moderator1, DateTime.Now.AddDays(100));

            Assert.IsNull(subforum1);
            Assert.IsNull(subforum2);
            Assert.IsNull(subforum3);
            Assert.IsNull(subforum4);
        }
    }
}
