using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using WASP.Exceptions;
using Client.DataClasses;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientCreateForumTests
    {
        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        
        
        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            _proj.Clean();
            _supervisor = ClientFunctions.InitialSystem(_proj);
        }

        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that there is only one admin
        /// </summary>
        [TestMethod]
        public void CreateForumTest1()
        {
            _proj.loginSU(_supervisor.userName, "moshe123");
            Forum forum = ClientFunctions.CreateSpecForum(_proj, _supervisor).Item1;
            Assert.IsNotNull(forum); //checks that a forum is created
            var admins = _proj.getAdmins( forum.id);

            // checks that there is only one admin
            Assert.IsTrue(admins.Count == 1); 
            Assert.IsTrue(_proj.getAdmins(forum.id).Count == 1);
        }


        /// <summary>
        /// Positive Test:  checks that a forum is created
        ///                 checks that the user which should be a admin, is it
        /// </summary>
        [TestMethod]
        public void CreateForumTest2()
        {
            _proj.loginSU(_supervisor.userName, "moshe123");
            Tuple<Forum, Admin> forumAndMember = ClientFunctions.CreateSpecForum(_proj, _supervisor);
            Forum forum = forumAndMember.Item1;
            Admin admin = forumAndMember.Item2;
            Assert.IsNotNull(forum); //checks that a forum is created

            Assert.IsTrue(admin.user.email.Equals("david@post.bgu.ac.il"));
            Assert.IsTrue(admin.user.userName.Equals("david"));
            //Assert.IsTrue(admin.user.password.Equals("david123"));
            Assert.IsTrue(admin.user.name.Equals("david"));

        }

        /// <summary>
        /// Positive Test, NF - load Test
        /// </summary>
        [TestMethod]
        public void CreateForumTest3()
        {
            int N = 500;
            _proj.loginSU(_supervisor.userName, "moshe123");
            for (int i = 1; i <= N; i++)
            {
                _proj.createForum("subject" + i.ToString(),
                   "----", 9,"admin" + i.ToString(), "admin" + i.ToString(),
                   "admin" + i.ToString() + "@post.bgu.ac.il", "admin" + i.ToString(), new Policy(5, 5, false, 5, 500)); 
                   
            }
            Assert.IsTrue(_proj.getAllForums().Count == N);
        }
        

    }
}
