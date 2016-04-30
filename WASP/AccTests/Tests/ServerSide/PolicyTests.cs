using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace AccTests.Tests.ServerSide
{
    [TestClass]

    class PolicyTests
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Forum _forum;
        private Subforum _subforum;
        private Member _moderator;
        private Member _member1;
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            _supervisor = _proj.login(_supervisor.UserName, _supervisor.Password);

        }
        /// <summary>
        /// password test
        /// Positive Test: tests that policy doesn't fail a good registery 
        /// </summary>
        [TestMethod]
        public void PasswordPolicy1()
        {
            var forum = _proj.createForum(_supervisor, "forum1", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy(null, true, 5));
            var mem=_proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem123", forum);
            Assert.IsNotNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (lack of number in password)
        /// </summary>
        [TestMethod]
        public void PasswordPolicy2()
        {
            var forum = _proj.createForum(_supervisor, "forum2", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy(null, true, 5));
            var mem = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "member", forum);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (password too short)
        /// </summary>
        [TestMethod]
        public void PasswordPolicy3()
        {
            var forum = _proj.createForum(_supervisor, "forum3", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy(null, true, 5));
            var mem = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem1", forum);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (password too short and lack of number)
        /// </summary>
        [TestMethod]
        public void PasswordPolicy4()
        {
            var forum = _proj.createForum(_supervisor, "forum4", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy(null, true, 5));
            var mem = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem", forum);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (admin doesn't follow policy)
        /// </summary>
        [TestMethod]
        public void PasswordPolicy5()
        {
            var forum = _proj.createForum(_supervisor, "forum5", "forum", "admin", "admin", "admin@gmail.com", "admi", new PasswordPolicy(null, true, 5));
            Assert.IsNull(forum);
        }
        /// <summary>
        /// changing policy test
        /// Positive Test: attempts to change the policy to no policy and add a member
        /// </summary>
        [TestMethod]
        public void ChangePolicy()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy(null, true, 5));
            _forum.AddPolicy(new PasswordPolicy());
            _proj.defineForumPolicy(_supervisor, forum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            Assert.IsNotNull(mem);
        }
        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checking that the normal policy causes no issue
        /// </summary>
        [TestMethod]
        public void NewModeratorPolicy1()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy());
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum=_proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check=_proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check>=0);
        }
        /// <summary>
        /// adding moderator policy tests
        /// Negative Test: a: checking that the adding member as moderator fails if he lacks seniority,
        ///                b: that admin can appoint himself as the first moderator regardless of seniority
        /// </summary>
        [TestMethod]
        public void NewModeratorPolicy2()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, 1000));
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check < 0);
        }

        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>

        [TestMethod]
        public void NewModeratorPolicy3()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, -5));
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check >= 0);
        }


        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checks that after duration you can creat the moderator
        /// </summary>
        [TestMethod]
        public async void NewModeratorPolicy4()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, 1));
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            await wait(2); //wait for seniority
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check >= 0);
        }


        /// <summary>
        /// security policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>

        [TestMethod]
        public void SecurityPolicy1()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com",
                "admin1234", new SecurityPolicy());
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check >= 0);
        }


        /// <summary>
        /// security policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>
        [TestMethod]
        public void SecurityPolicy2()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com",
                "admin1234", new SecurityPolicy(null, -5));
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check >= 0);
        }
        /// <summary>
        /// security policy tests
        /// Negative Test: checks that after the duration, the user cannot log in
        /// </summary>
        [TestMethod]
        public async void SecurityPolicy3()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new SecurityPolicy(null, 1));
            var admin = _proj.login("admin", "admin1234", forum);
            var subforum = _proj.createSubForum(admin, "name", "description", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum("a", "a", "a.b@c.d", "a", forum);
            var check = _proj.addModerator(admin, mem, subforum, DateTime.MaxValue);
            Assert.IsTrue(check >= 0);
            mem = _proj.login(mem.UserName, mem.Password, forum);
            Assert.IsNotNull(mem);

            await wait(2); //wait for password to expire
            mem = _proj.login(mem.UserName, mem.Password, forum);
            Assert.IsNull(mem);
             admin = _proj.login("admin", "admin1234", forum);
            Assert.IsNull(admin);
        }

        private static async Task<int> wait(int duration)
        {
            Task.Delay(duration);
            return duration;
        }



    }
}
