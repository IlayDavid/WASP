using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Dictionary<Forum, Member> _admins;
        private List<Forum> _forums;
        private Dictionary<Forum, Subforum> _subforums;
        private Dictionary<Subforum, Member> _moderators;
        private Dictionary<Forum, Member> _members;
        private Dictionary<Subforum, Post> _posts;
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
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy());
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
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy());
            var mem = _proj.subscribeToForum("mem1", "mem", "mem1@post.bgu.ac.il", "mem", forum);
            Assert.IsNull(mem);
        }
    }
}
