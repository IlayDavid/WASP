﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using WASP;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class SubscribeForumTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Member _admin;

        [OneTimeSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
            SuperUser supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Member> forumAndMember = Functions.CreateSpecForum(_proj,supervisor);
            _forum = forumAndMember.Item1;
            _admin = forumAndMember.Item2;

            _proj.login(_admin.UserName, _admin.Password, _forum);
        }

        /*
         * Positive Test: checks that there is one member
         */ 
        [Test]
        public void subscribeToForumTest1()
        {

            Member isMem = _proj.subscribeToForum("mosheB", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum);
            List<Member> members = _proj.getMembers(_admin, _forum);

            Assert.NotNull(isMem);
            Assert.Equals(members.Count, 1);
            Assert.IsTrue(members.Contains(isMem));
            Assert.NotNull(_proj.login("mosheB", "moshe123", _forum).UserName);
        }

        /*
         * Negative Test: lack of information
         */
        [Test]
        public void subscribeToForumTest2()
        {
            Member isMem;
            isMem = _proj.subscribeToForum("", "moshe", "mosheB@psot.bgu.ac.il", "moshe123", _forum);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum("mosheB", "", "mosheB@psot.bgu.ac.il", "moshe123", _forum);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum("mosheB", "moshe", "", "moshe123", _forum);
            Assert.IsNull(isMem);

            isMem = _proj.subscribeToForum("mosheB", "moshe", "mosheB@psot.bgu.ac.il", "", null);
            Assert.IsNull(isMem);
        }
    }
}
