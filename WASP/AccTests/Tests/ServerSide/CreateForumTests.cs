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
        
        
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
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
            List<Admin> admins = _proj.getAdmins(_supervisor.id, forum.Id);

            // checks that there is only one admin
            Assert.IsTrue(admins.Count == 1); 
            Assert.IsTrue(forum.GetAdmins().Count == 1);
        }


        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that the user which should be a admin, is it
        /// </summary>
        [TestMethod]
        public void CreateForumTest2()
        {
            Tuple<Forum, Admin> forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);
            Forum forum = forumAndMember.Item1;
            Admin admin = forumAndMember.Item2;
            Assert.IsNotNull(forum); //checks that a forum is created
            
            Assert.IsTrue(admin.user.email.Equals("david@post.bgu.ac.il"));
            Assert.IsTrue(admin.user.userName.Equals("admin"));
            Assert.IsTrue(admin.user.password.Equals("david123"));
            Assert.IsTrue(admin.user.name.Equals("david"));

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
                _proj.createForum(_supervisor.id, "subject" + i.ToString(),
                   "----", 9,"admin" + i.ToString(), "admin" + i.ToString(),
                   "admin" + i.ToString() + "@post.bgu.ac.il", "admin" + i.ToString(), new PasswordPolicy()); 
                   
            }
            Assert.IsTrue(_proj.getAllForums().Count == N);
        }

        /// <summary>
        /// Nagative Test, NF - secure Test
        /// </summary>
        [TestMethod]
        public void CreateForumTest4()
        {
            Forum forum = _proj.createForum(-1, "subject",
                   "----",10, "admin", "admin", "admin@post.bgu.ac.il", "admin", new PasswordPolicy()); 
            Assert.IsNull(forum);
        }

    }
}
