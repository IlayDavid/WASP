using System;
using NUnit.Framework;
using WASP.DataClasses;
namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class AddModeratorTests
    {



        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Member _admin;
        private Forum _forum;
        private Subforum _subforum;
        private Member _moderator;
        private Member _member1;

        [OneTimeSetUp]

        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }
        [SetUp]

        public void setUp()
        {
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
        [Test]
        public void addModeratorAndUpdateTermTest1()
        {
            int isModerator = _proj.addModerator(_moderator, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.GreaterOrEqual(isModerator, 0);
            Assert.Equals(_proj.getModerators(_admin,_subforum).Count, 2);

            int isModified = _proj.updateModeratorTerm(_moderator,_member1,_subforum, DateTime.Now.AddDays(100));
            Assert.GreaterOrEqual(isModerator, 0);
            Assert.Equals(_proj.getModeratorTermTime(_moderator, _member1, _subforum), DateTime.Now.AddDays(100));
        }


        /// <summary>
        /// Positive Test:  checks that admin of a forum can add a moderator to the forum's subforum
        ///                 checks that the term's update is succeed
        /// </summary>
        [Test]
        public void addModeratorAndUpdateTermTest2()
        {
            int isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.GreaterOrEqual(isModerator, 0);
            Assert.Equals(_proj.getModerators(_admin, _subforum).Count, 2);

            int isModified = _proj.updateModeratorTerm(_admin, _member1, _subforum, DateTime.Now.AddDays(100));
            Assert.GreaterOrEqual(isModerator, 0);
            Assert.Equals(_proj.getModeratorTermTime(_admin, _member1, _subforum), DateTime.Now.AddDays(100));
        }
       

        /// <summary>
        /// Nagative Test - NF secure test:
        ///     checks that another forum's admin cannot term a moderator for another subforum
        /// </summary>
        public void addModeratorAndUpdateTermTest3()
        {

            Forum forum = _proj.createForum(_supervisor, "forum1", "blah", "haaronB",
                                            "haaron", "haaronB@post.bgu.ac.il", "haaron123");
            Member admin = _proj.getAdmin(_supervisor, forum, "haaronB");
            _proj.login(admin.UserName, admin.Password, _forum);

            //another admin tries to add a moderator
            int isModerator = _proj.addModerator(admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.Less(isModerator, 0);

            isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            Assert.GreaterOrEqual(isModerator, 0);

            int isModified = _proj.updateModeratorTerm(admin, _member1, _subforum, DateTime.Now.AddDays(100));
            Assert.Less(isModified, 0);

        }
          
/* edit name + RTM*/
        
            
            /// <summary>
        /// Nagative Test: invalid date time
        /// </summary>
        public void addModeratorAndUpdateTermTest4()
        {
            int isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(-10));
            Assert.Less(isModerator, 0);
            Assert.Equals(_proj.getModerators(_admin, _subforum).Count, 1);

            isModerator = _proj.addModerator(_admin, _member1, _subforum, DateTime.Now.AddDays(200));
            int isModified = _proj.updateModeratorTerm(_admin, _member1, _subforum, DateTime.Now.AddDays(-1));
            Assert.Less(isModerator, 0);
            Assert.Equals(_proj.getModeratorTermTime(_admin, _member1, _subforum), DateTime.Now.AddDays(200));
        }
      
        
        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        public void addModeratorAndUpdateTermTest5()
        {
            int isModerator = _proj.addModerator(_admin, _member1, null, DateTime.Now);
            Assert.Less(isModerator, 0);

            isModerator = _proj.addModerator(null, _member1, _subforum, DateTime.Now);
            Assert.Less(isModerator, 0);

            isModerator = _proj.addModerator(_admin, null, _subforum, DateTime.Now);
            Assert.Less(isModerator, 0);
        }
    }
}
