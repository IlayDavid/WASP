using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SubscribeForumTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Admin _admin;

        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            SuperUser supervisor = Functions.InitialSystem(_proj);
            var forumAndMember = Functions.CreateSpecForum(_proj,supervisor);
            _forum = forumAndMember.Item1;
            _admin = forumAndMember.Item2;

            _proj.login(_admin.User.Username, _admin.User.Password, _forum.Id);
        }

        /*
         * Positive Test: checks that there is one member
         */ 
        [TestMethod]
        public void subscribeToForumTest1()
        {

            var isMem = _proj.subscribeToForum(55,"mosheB", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum.Id);
            var members = _proj.getMembers(_admin.User.Id, _forum.Id);

            Assert.IsNotNull(isMem);
            Assert.AreEqual(members.Length, 2);
            Assert.IsTrue(members.ToList().Contains(isMem));
            Assert.IsNotNull(_proj.login("mosheB", "moshe123", _forum.Id));
        }

        /*
         * Negative Test: lack of information
         */
        [TestMethod]
        public void subscribeToForumTest2()
        {
            var isMem = _proj.subscribeToForum(57,"", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum.Id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(58,"mosheB", "", "mosheB@psot.bgu.ac.il", "moshe123", _forum.Id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(59,"mosheB", "moshe", "", "moshe123", _forum.Id);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum(60,"mosheB", "moshe", "mosheB@psot.bgu.ac.il", "", -1);
            Assert.IsNull(isMem);
        }
    }
}
