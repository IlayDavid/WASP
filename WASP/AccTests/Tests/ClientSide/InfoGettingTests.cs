using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests.ServerSide
{
    /// <summary>
    /// tests the info requests described in assignment 3.
    /// </summary>
    [TestClass]
    internal class ClientInfoGettingTests
    {
        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        private Dictionary<Forum, List<User>> _admins = new Dictionary<Forum, List<User>>();
        private List<Forum> _forums = new List<Forum>();
        private Dictionary<Forum, List<Subforum>> _subforums = new Dictionary<Forum, List<Subforum>>();
        private Dictionary<Subforum, List<User>> _moderators = new Dictionary<Subforum, List<User>>();
        private Dictionary<Forum, List<User>> _members = new Dictionary<Forum, List<User>>();
        private Dictionary<Subforum, List<Post>> _posts = new Dictionary<Subforum, List<Post>>();
        private Dictionary<int, List<Post>> _postsByUser = new Dictionary<int, List<Post>>();
        private const int LOOPS = 10;

        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            _supervisor = ClientFunctions.InitialSystem(_proj);
            _supervisor = _proj.loginSU(_supervisor.userName, _supervisor.password);
            for (int i = 0; i < LOOPS; i++)
            {
                var forum = _proj.createForum("forum_" + i, "the " + i + "th forum", 100, "admin_" + i,
                    "admin" + i,
                    "admin" + i + "@gmail.com", "admin1234", new Policy(5, 5, false, 5, 500));
                var admin = _proj.login("admin_" + i, "admin1234", forum.id, "");
                _admins[forum] = new List<User>();
                _admins[forum].Add(admin);
                _forums.Add(forum);
                _subforums[forum] = new List<Subforum>();
                _members[forum] = new List<User>();
                var member = _proj.subscribeToForum(110 + i, "mod_" + i + "_1", "mod" + i + "_1", "mod" + i + "_1@gmail.com",
                    "mod1234",
                    forum.id, ClientFunctions.GetAnswers(), false);
                _members[forum].Add(member);
                var subforum = _proj.createSubForum("subforum_" + i, "the " + i + "th subforum", member.id,
                    DateTime.MaxValue);
                _moderators[subforum] = new List<User>();
                _posts[subforum] = new List<Post>();
                _subforums[forum].Add(subforum);
                _moderators[subforum].Add(member);
                member = _proj.subscribeToForum(120 + i, "mod_" + i + "_2", "mod" + i + "_2", "mod" + i + "_2@gmail.com",
                    "mod1234",
                    forum.id, ClientFunctions.GetAnswers(), false);
                _members[forum].Add(member);
                _proj.addModerator(member.id, subforum.id, DateTime.MaxValue);
                _moderators[subforum].Add(member);
                var prevMember = member;
                var post = _proj.createThread("title", "first message of forum_" + i, subforum.id);
                _posts[subforum].Add(post);
                _postsByUser[member.id] = new List<Post>();
                _postsByUser[member.id].Add(post);
                for (int j = 0; j < LOOPS; j++)
                {
                    prevMember = member;
                    member = _proj.subscribeToForum(130 + j + i * 10, "user_" + i + "_" + j, "user" + i + "_" + j,
                        "user" + i + "_" + j + "@gmail.com", "user1234",
                        forum.id, ClientFunctions.GetAnswers(), false);
                    _members[forum].Add(member);
                    post = _proj.createReplyPost("this is reply number " + i, post.id);
                    _postsByUser[member.id] = new List<Post>();
                    _postsByUser[member.id].Add(post);

                    _posts[subforum].Add(post);
                }
            }
        }
        /// <summary>
        /// checks if we get the correct number of same user
        /// </summary>
        public void sameUser()
        {
            Assert.IsTrue(_proj.membersInDifferentForums().Count == LOOPS);
        }
        /// <summary>
        /// check if we get the correct number of posts
        /// </summary>
        [TestMethod]
        public void numberOfPosts()
        {
            var numPosts = _proj.subForumTotalMessages(_subforums[_forums[0]][0].id);
            Assert.IsTrue(numPosts == LOOPS * LOOPS);
        }

        /// <summary>
        /// cheks that we can correctly retrieve posts made by a user
        /// </summary>
        [TestMethod]
        public void postsByUser()
        {
            for (int i = 0; i < LOOPS; i++)
            {
                var postsByMember = _proj.postsByMember(_members[_forums[0]][i].id);
                var posts =
                    _posts[_subforums[_forums[0]][0]].Where((x) => x.author.id == _members[_forums[0]][i].id);
                foreach (var post in posts)
                {
                    Assert.IsTrue(postsByMember.Any((x) => (x).id == post.id));
                }
            }
        }

        [TestMethod]
        public void moderatorReport()
        {
            var reports = _proj.moderatorReport();

            foreach (var mod in reports.ModeratorInsubForum)
            {
                Assert.IsTrue(_moderators[_subforums[_forums[0]][mod.Key]].Any((x) => x.id == mod.Value));
            }
            var subforums = _subforums[_forums[0]];
            IEnumerable<User> modlist = new List<User>();
            modlist = subforums.Aggregate(modlist, (current, subforum) => current.Concat(_moderators[subforum]));
            foreach (var moderator in reports.ModeratorInsubForum)
            {
                Assert.IsTrue(modlist.Any((x) => x.id == moderator.Key));
            }

            foreach (var mod in reports.moderatorsPosts)
            {
                var posts = _proj.postsByMember(mod.Key);
                foreach (var post in posts)
                {
                    Assert.IsTrue(_postsByUser[mod.Key].Any((x) => x.id == post.id));
                }
            }
        }


    }
}
