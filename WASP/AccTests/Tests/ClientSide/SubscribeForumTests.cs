using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientSubscribeForumTests
    {

        private WASPClientBridge _proj;
        private Forum _forum;
        private Admin _admin;

        [TestInitialize]     //before each Test
        public void SetUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            SuperUser supervisor = ClientFunctions.InitialSystem(_proj);
            var forumAndMember = ClientFunctions.CreateSpecForum(_proj,supervisor);
            _forum = forumAndMember.Item1;
            _admin = forumAndMember.Item2;

            _proj.login(_admin.user.userName, _admin.user.password, _forum.id, "");
            
        }

        /*
         * Positive Test: checks that there is one member
         */ 
        [TestMethod]
        public void subscribeToForumTest1()
        {
            var isMem = _proj.subscribeToForum(55,"mosheB", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum.id);
            var members = _proj.getMembers(_forum.id);

            Assert.IsNotNull(isMem);
            Assert.AreEqual(members.Count, 2);
            Assert.IsTrue(members[0].id==isMem.id || members[1].id==isMem.id);
            _proj.logout();
            Assert.IsNotNull(_proj.login("mosheB", "moshe123", _forum.id, ""));
            _proj.logout();
        }

        /*
         * Negative Test: lack of information
         */
        [TestMethod]
        public void subscribeToForumTest2()
        {
            var isMem = _proj.subscribeToForum(57,"", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum.id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(58,"mosheB", "", "mosheB@psot.bgu.ac.il", "moshe123", _forum.id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(59,"mosheB", "moshe", "", "moshe123", _forum.id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(60,"mosheB", "moshe", "mosheB@psot.bgu.ac.il", "", -1);
            Assert.IsNull(isMem);
            _proj.logout();
        }
    }
}
