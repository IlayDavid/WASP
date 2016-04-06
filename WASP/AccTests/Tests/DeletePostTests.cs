using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;


namespace AccTests.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DeletePostTests
    {

        private WASPBridge _proj;
        private Forum _forum;
        private Subforum _subforum;
        private SuperUser _supervisor;
        private Member _admin;
        private Member _moderator;
        private Post _thread;
        private Member _member;
        private Post _threadModerator;
        private Post _threadMember;

       
        [TestInitialize]     //before each Test
        public void SetUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);

            Tuple<Forum, Member> forumAndAdmin = Functions.CreateSpecForum(_proj, _supervisor);
            _forum = forumAndAdmin.Item1;
            _admin = forumAndAdmin.Item2;
            _proj.login(_admin.UserName, _admin.Password, _forum);

            Tuple<Subforum, Member> subforumAndModerator = Functions.CreateSpecSubForum(_proj, _admin, _forum);
            _subforum = subforumAndModerator.Item1;
            _moderator = subforumAndModerator.Item2;
            _proj.login(_moderator.UserName, _moderator.Password, _forum);

            _member = Functions.SubscribeSpecMember(_proj, _forum);
            _proj.login(_member.UserName, _member.Password, _forum);

            _threadModerator = _proj.createThread(_moderator, "webService for calander",
                                    "Someone know a good web service for Calander?", DateTime.Now, _subforum);

            _threadMember = _proj.createThread(_moderator, "webService for calander",
                        "Someone know a good web service for Calander?", DateTime.Now, _subforum);



        }

        /// <summary>
        /// Positive Test: checks that moderator and member can delete their own thread
        /// </summary>
        [TestMethod]
        public void deletePostTest1()
        {
            int isDelete = _proj.deletePost(_member, _threadMember);
            Assert.IsTrue(isDelete >= 0);
            isDelete = _proj.deletePost(_moderator, _threadModerator);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void deletePostTest2()
        {
            Post p1 = _proj.createReplyPost(_moderator, "Hi", DateTime.Now, _threadMember);
            Post p2 = _proj.createReplyPost(_member, "Hi", DateTime.Now, _threadModerator);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);
            

            int isDelete = _proj.deletePost(_member, p2);
            Assert.IsTrue(isDelete >= 0);
            isDelete = _proj.deletePost(_moderator, p1);
            Assert.IsTrue(isDelete >= 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete thread which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest3()
        {
            int isDelete = _proj.deletePost(_moderator, _threadMember);
            Assert.IsTrue(isDelete < 0);
            isDelete = _proj.deletePost(_member, _threadModerator);
            Assert.IsTrue(isDelete < 0);
        }

        /// <summary>
        /// Negative Test: secure NF: user cant delete post which is not own
        /// </summary>
        [TestMethod]
        public void deletePostTest4()
        {
            Post p1 = _proj.createReplyPost(_moderator, "Hi", DateTime.Now, _threadMember);
            Post p2 = _proj.createReplyPost(_member, "Hi", DateTime.Now, _threadModerator);
            Assert.IsNotNull(p2);
            Assert.IsNotNull(p1);

            int isDelete = _proj.deletePost(_moderator, p2);
            Assert.IsTrue(isDelete < 0);
            isDelete = _proj.deletePost(_member, p1);
            Assert.IsTrue(isDelete< 0);
        }
    }
}
