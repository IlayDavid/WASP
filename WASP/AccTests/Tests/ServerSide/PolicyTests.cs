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
        [TestMethod]
        public void PasswordPolicy()
        {
            var forum = _proj.createForum(_supervisor, "forum", "forum", "admin", "admin", "admin@gmail.com", "admin1234", new PasswordPolicy());
            var admin = _proj.login("admin", "admin1234", forum);
        }
    }
}
