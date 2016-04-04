using NUnit.Framework;
using WASP;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class CreateSubForumTests
    {



        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Subforum _subforum;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]
        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            _forum = Functions.CreateSpecForum(_proj, _supervisor);
            _subforum = new Subforum("Calander-Start-Up",
                new User ("mod1","123456","mod1","mod1@post.bgu.ac.il"));
        }

        /// <summary>
        /// checks that a supervisor can open a sub forum
        /// </summary>
        [Test]
        public void CreatesubforumTest1()
        {
            int subforumId = _proj.createSubForum(_supervisor._userName, _forumId, _subforum);
            Assert.Greater(subforumId, 0);
            Assert.Equals(_proj.getModerators(subforumId).Count, 1);
        }

        /// <summary>
        /// checks that a admin can open a sub forum
        /// </summary>
        [Test]
        public void CreatesubforumTest2()
        {
            int subforumId = _proj.createSubForum(_admin._userName, _forumId, _subforum);
            Assert.Greater(subforumId, 0);
            Assert.Equals(_proj.getModerators(subforumId).Count, 1);

        }

        /// <summary>
        /// checks that can create more then one subforum in the same forum
        /// </summary>
        public void CreatesubforumTest3()
        {

            _admin = new User("matansar", "123456", "matan", "matansar@post.bgu.ac.il");
            Subforum subforum2 = new Subforum("Calander-Start-Up", new User("mod2", "123456", "mod2", "mod2@post.bgu.ac.il"));
            Subforum subforum3 = new Subforum("Soliter-Start-Up", new User("mod3", "123456", "mod3", "mod3@post.bgu.ac.il"));
            bool is_opened = _proj.createSubForum(_supervisor._userName, _forumId, _subforum) > 0 &&
                _proj.createSubForum(_supervisor._userName, _forumId, subforum2) > 0 &&
                _proj.createSubForum(_admin._userName, _forumId, subforum3) > 0;
            Assert.IsTrue(is_opened);
            Assert.Equals(_proj.getSubforums(_forumId).Count, 3);
        }

        /// <summary>
        /// Positive test - NF load test
        /// </summary>
        [Test]
        public void CreatesubforumTest4()
        {
            int N = 500;
            for (int i = 1; i <= N; i++)
            {
                Subforum subforum = new Subforum("Calander-Start-Up",
                                new User("modd" + i.ToString() , 
                                        "123456", "modd" + i.ToString()
                                        , "modd" + i.ToString() + "@post.bgu.ac.il"));
                int subforumId = _proj.createSubForum(_admin._userName, _forumId, subforum);
                Assert.Greater(subforumId, 0);
                Assert.Equals(_proj.getModerators(subforumId).Count, 1);
            }

            Assert.Equals(_proj.getSubforums(_forumId).Count, N);
        }

        /// <summary>
        /// Nagative test - NF secure test: admin which is not responsible 
        /// </summary>
        [Test]
        public void CreatesubforumTest5()
        {

            User admin = new User("moshe", "123456", "moshe", "moshe@post.bgu.ac.il");
            Forum forum = new Forum("Health", _admin);
            int forumId = _proj.createForum(_supervisor._userName, forum);

            Subforum subforum = new Subforum("v",
                            new User("modd", "123456", "modd", "modd@post.bgu.ac.il"));
            int subforumId = _proj.createSubForum(admin._userName, _forumId, subforum);
            Assert.LessOrEqual(subforumId, 0);
        }


    }
}
