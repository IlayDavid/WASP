using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace AccTests.Tests.ServerSide
{
    /// <summary>
    /// tests the info requests described in assignment 3.
    /// </summary>
    [TestClass]
    class InfoGettingTests
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Dictionary<Forum,Member> _admins;
        private List<Forum> _forums;
        private Dictionary<Forum,Subforum> _subforums;
        private Dictionary<Subforum, Member> _moderators;
        private Dictionary<Forum, Member> _members;
        private Dictionary<Subforum, Post> _posts;
        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            _supervisor=_proj.login(_supervisor.UserName, _supervisor.Password);
            for (int i = 0; i < 10; i++)
            {
                var forum=_proj.createForum(_supervisor, "forum_" + i, "the " + i + "th forum", "admin_" + i,"admin"+i,
                    "admin" + i + "@gmail.com", "admin1234", new PasswordPolicy());
                var admin = _proj.login("admin_" + i, "admin1234",forum);
                _admins.Add(forum,admin);
                _forums.Add(forum);
                var member = _proj.subscribeToForum("mod_" + i+"_1", "mod" + i + "_1", "mod" + i + "_1@gmail.com", "mod1234",
                    forum);
                _members.Add(forum,member);

                var subforum = _proj.createSubForum(admin, "subforum_" + i, "the " + i + "th subforum", member,
                    DateTime.MaxValue);
                _subforums.Add(forum,subforum);
                _moderators.Add(subforum,member);
                member = _proj.subscribeToForum("mod_" + i + "_2", "mod" + i + "_2", "mod" + i + "_2@gmail.com", "mod1234",
                    forum);
                _members.Add(forum, member);
                _proj.addModerator(admin, member, subforum, DateTime.MaxValue);
                _moderators.Add(subforum,member);
                var prevMember = member;
                var post=_proj.createThread(prevMember, "title", "first message of forum_" + i, DateTime.Now, subforum);
                _posts.Add(subforum,post);
                for (int j = 0; j < 10; j++)
                {
                    post=_proj.createReplyPost(prevMember, "this is reply number " + i, DateTime.Now, post);
                    _posts.Add(subforum,post);
                    member = _proj.subscribeToForum("user_" + i + "_"+j, "user" + i + "_"+j, "user" + i + "_"+j+"@gmail.com", "user1234",
                    forum);
                    _members.Add(forum, member);
                }
            }
        }
        /// <summary>
        /// checks if a user who posted no posts returns 0
        /// </summary>
        [TestMethod]
        public void numberOfPosts1()
        {
            Assert.IsTrue(false);
        }

    }
}
