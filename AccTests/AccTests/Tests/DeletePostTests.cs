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
    public class DeletePostTests
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
        /// checks that user can delete his own post
        /// </summary>
        [Test]
        public void OpenThreadTest1()
        {
            Post replayPost = new Post("seach at google");
            int postId = _proj.createPost(_supervisor._userName, _threadId, replayPost);
            Assert.Greater(_proj.deletePost(_supervisor._userName, _threadId, postId), 0);
        }
    }
}
