using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using System;
using WASP.DataClasses.Policies;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CreateForumTests
    {

        private WASPBridge _proj;
        private SuperUser _supervisor;

        [AssemblyInitialize]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [ClassInitialize]
        public void setUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
        }

        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that there is only one admin
        /// </summary>
        [TestMethod]
        public void CreateForumTest1()
        {
            Forum forum = Functions.CreateSpecForum(_proj, _supervisor).Item1;
            Assert.IsNotNull(forum); //checks that a forum is created
            List<Member> admins = _proj.getAdmins(_supervisor, forum);

            // checks that there is only one admin
            Assert.Equals(admins.Count, 1); 
            Assert.Equals(forum.GetAdmins().Count, 1);
        }


        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that the user which should be a admin, is it
        /// </summary>
        [TestMethod]
        public void CreateForumTest2()
        {
            Tuple<Forum, Member> forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);
            Forum forum = forumAndMember.Item1;
            Member admin = forumAndMember.Item2;
            Assert.IsNotNull(forum); //checks that a forum is created
            
            Assert.Equals(admin.Email, "david@post.bgu.ac.il");
            Assert.Equals(admin.UserName, "admin");
            Assert.Equals(admin.Password, "david123");
            Assert.Equals(admin.Name, "david");

        }

        /// <summary>
        /// Positive Test, NF - load Test
        /// </summary>
        [TestMethod]
        public void CreateForumTest3()
        {
            int N = 500;
            for (int i = 1; i <= N; i++)
            {
                _proj.createForum(_supervisor, "subject" + i.ToString(),
                   "----", "admin" + i.ToString(), "admin" + i.ToString(),
                   "admin" + i.ToString() + "@post.bgu.ac.il", "admin" + i.ToString(), new PasswordPolicy()); 
                   
            }
            Assert.Equals(_proj.getAllForums(_supervisor).Count, N);
        }

        /// <summary>
        /// Nagative Test, NF - secure Test
        /// </summary>
        [TestMethod]
        public void CreateForumTest4()
        {
            Forum forum = _proj.createForum(null, "subject",
                   "----", "admin", "admin", "admin@post.bgu.ac.il", "admin", new PasswordPolicy()); 
            Assert.IsNull(forum);
        }

    }
}
