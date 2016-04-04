﻿using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using WASP;
using WASP.DataClasses;
namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class CreateForumTests
    {

        private WASPBridge _proj;
        private SuperUser _supervisor;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]
        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
        }

        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that there is only one admin
        /// </summary>
        [Test]
        public void CreateForumTest1()
        {
            Forum forum = Functions.CreateSpecForum(_proj, _supervisor);
            Assert.NotNull(forum); //checks that a forum is created
            List<Member> admins = _proj.getAdmins(_supervisor, forum);

            // checks that there is only one admin
            Assert.Equals(admins.Count, 1); 
            Assert.Equals(forum.GetAdmins().Count, 1);
        }


        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that the user which should be a admin, is it
        /// </summary>
        [Test]
        public void CreateForumTest2()
        {
            Forum forum = Functions.CreateSpecForum(_proj, _supervisor);
            Assert.NotNull(forum); //checks that a forum is created
            
            List<Member> admins = _proj.getAdmins(_supervisor,forum);
            Member temp = forum.GetAdmins()[0];
            Assert.Equals(admins.Contains(temp), true); // checks that the user added as admin         
        }

        /// <summary>
        /// Positive Test, NF - load Test
        /// </summary>
        public void CreateForumTest3()
        {
            int N = 500;
            for (int i = 1; i <= N; i++)
            {
                _proj.createForum(_supervisor, "subject" + i.ToString(),
                   "----", "admin" + i.ToString(), "admin" + i.ToString(),
                   "admin" + i.ToString() + "@post.bgu.ac.il", "admin" + i.ToString()); 
                   
            }
            Assert.Equals(_proj.getAllForums(_supervisor).Count, N);
        }

        /// <summary>
        /// Nagative Test, NF - secure Test
        /// </summary>
        public void CreateForumTest4()
        {
            Forum forum = _proj.createForum(null, "subject",
                   "----", "admin", "admin", "admin@post.bgu.ac.il", "admin"); 
            Assert.Null(forum);
        }

    }
}