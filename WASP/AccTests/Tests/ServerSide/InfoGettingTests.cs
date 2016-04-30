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
    internal class InfoGettingTests
    {
        private WASPBridge _proj;
        private SuperUser _supervisor;
        private Dictionary<Forum, List<Member>> _admins = new Dictionary<Forum, List<Member>>();
        private List<Forum> _forums = new List<Forum>();
        private Dictionary<Forum, List<Subforum>> _subforums = new Dictionary<Forum, List<Subforum>>();
        private Dictionary<Subforum, List<Member>> _moderators = new Dictionary<Subforum, List<Member>>();
        private Dictionary<Forum, List<Member>> _members = new Dictionary<Forum, List<Member>>();
        private Dictionary<Subforum, List<Post>> _posts = new Dictionary<Subforum, List<Post>>();
        private const int LOOPS = 10;

        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            _supervisor = _proj.login(_supervisor.UserName, _supervisor.Password);
            for (int i = 0; i < LOOPS; i++)
            {
                var forum = _proj.createForum(_supervisor, "forum_" + i, "the " + i + "th forum", "admin_" + i,
                    "admin" + i,
                    "admin" + i + "@gmail.com", "admin1234", new PasswordPolicy());
                var admin = _proj.login("admin_" + i, "admin1234", forum);
                _admins[forum] = new List<Member>();
                _admins[forum].Add(admin);
                _forums.Add(forum);
                _subforums[forum] = new List<Subforum>();
                _members[forum] = new List<Member>();
                var member = _proj.subscribeToForum("mod_" + i + "_1", "mod" + i + "_1", "mod" + i + "_1@gmail.com",
                    "mod1234",
                    forum);
                _members[forum].Add(member);
                var subforum = _proj.createSubForum(admin, "subforum_" + i, "the " + i + "th subforum", member,
                    DateTime.MaxValue);
                _moderators[subforum] = new List<Member>();
                _posts[subforum] = new List<Post>();
                _subforums[forum].Add(subforum);
                _moderators[subforum].Add(member);
                member = _proj.subscribeToForum("mod_" + i + "_2", "mod" + i + "_2", "mod" + i + "_2@gmail.com",
                    "mod1234",
                    forum);
                _members[forum].Add(member);
                _proj.addModerator(admin, member, subforum, DateTime.MaxValue);
                _moderators[subforum].Add(member);
                var prevMember = member;
                var post = _proj.createThread(prevMember, "title", "first message of forum_" + i, DateTime.Now, subforum);
                _posts[subforum].Add(post);
                for (int j = 0; j < LOOPS; j++)
                {
                    prevMember = member;
                    post = _proj.createReplyPost(prevMember, "this is reply number " + i, DateTime.Now, post);
                    _posts[subforum].Add(post);
                    member = _proj.subscribeToForum("user_" + i + "_" + j, "user" + i + "_" + j,
                        "user" + i + "_" + j + "@gmail.com", "user1234",
                        forum);
                    _members[forum].Add(member);
                }
            }
        }

        /// <summary>
        /// check if we get the correct number of posts
        /// </summary>
        [TestMethod]
        public void numberOfPosts()
        {
            var numPosts = _proj.getNumberOfPosts(_admins[_forums[0]][0], _forums[0]);
            Assert.IsTrue(numPosts == LOOPS*LOOPS);
        }

        /// <summary>
        /// cheks that we can correctly retrieve posts made by a user
        /// </summary>
        [TestMethod]
        public void numberOfPostsByUser()
        {
            for (int i = 0; i < LOOPS; i++)
            {
                var postsByMember = _proj.postsByMember(_admins[_forums[0]][0], _forums[0], _members[_forums[0]][i]);
                var posts =
                    _posts[_subforums[_forums[0]][0]].Where((x) => x.GetAuthor.Name == _members[_forums[0]][i].Name);
                foreach (var post in posts)
                {
                    Assert.IsTrue(postsByMember.Any((x) => (x).Content.Equals(post.Content)));
                }
            }
        }


    }
}
