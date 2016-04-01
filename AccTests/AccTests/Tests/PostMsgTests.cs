using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class PostMsgTests
    {

        private WASPBridge _proj;
        private int _forumId;
        private int _subforumId;
        private User _supervisor;
        private User _admin;
        private int _threadId;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
            _supervisor = _proj.initialize();

            _admin = new User("matansar", "123456", "matan", "matansar@post.bgu.ac.il");
            Forum forum = new Forum("Start-Up", _admin);
            Subforum subforum = new Subforum("Calander-Start-Up");
            Post openPost = new Post("Someone know a good web service for Calander?");
            UserThread thread = new UserThread("webService for calander", openPost);
            _forumId = _proj.createForum(_supervisor._userName, forum);
            _subforumId = _proj.createSubForum(_supervisor._userName, _forumId, subforum);
            _threadId = _proj.createThread(_supervisor._userName, _subforumId, thread);
        }


        /// <summary>
        /// checks that supervisor can add a post
        /// </summary>
        [Test]
        public void OpenThreadTest1()
        {
            Post replayPost = new Post("seach at google");
            Assert.Greater(_proj.createPost(_supervisor._userName, _threadId, replayPost) , 0);
        }

        /// <summary>
        /// checks that admin can add a post
        /// </summary>
        [Test]
        public void OpenThreadTest2()
        {
            Post replayPost = new Post("seach at google");
            Assert.Greater(_proj.createPost(_admin._userName, _threadId, replayPost), 0);
        }

        /// <summary>
        /// checks that member can a post
        /// </summary>
        [Test]
        public void OpenThreadTest3()
        {
            User member = new User("amitayaSh", "123456", "amitay", "amitayaSh@post.bgu.ac.il");
            _proj.subscribeToForum(member, _forumId);
            Post replayPost = new Post("seach at google");
            Assert.Greater(_proj.createPost(member._userName, _threadId, replayPost), 0);
        }
    }
}
