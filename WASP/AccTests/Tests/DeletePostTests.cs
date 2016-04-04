using System;
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
    public class DeletePostTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Member _admin;
        private Post _openPost;

        [OneTimeSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
            _supervisor = Functions.InitialSystem(_proj);
            Tuple<Forum, Member> forumAndMember = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndMember.Item1;
            _admin = forumAndMember.Item2;
            Member moderator = _proj.subscribeToForum("admin", "moshe", "admin@post.bgu.ac.il", 
                                     "admin123", _forum);
            Subforum subforum = _proj.createSubForum(_admin, "Calander-Start-Up", "blah", moderator);
            Post openPost = _proj.createThread(moderator,"calander web servic", "Someone know a good web service for Calander?",
                DateTime.Now, null, subforum,
                        )
            Post openPost = new Po
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
