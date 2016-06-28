using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

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
        private Dictionary<Forum, List<User>> _admins = new Dictionary<Forum, List<User>>();
        private List<Forum> _forums = new List<Forum>();
        private Dictionary<Forum, List<Subforum>> _subforums = new Dictionary<Forum, List<Subforum>>();
        private Dictionary<Subforum, List<User>> _moderators = new Dictionary<Subforum, List<User>>();
        private Dictionary<Forum, List<User>> _members = new Dictionary<Forum, List<User>>();
        private Dictionary<Subforum, List<Post>> _posts = new Dictionary<Subforum, List<Post>>();
        private Dictionary<int, List<Post>> _postsByUser = new Dictionary<int, List<Post>>();
        private const int LOOPS = 10;
        private string[] arr;

        [TestInitialize]
        public void setUp()
        {
            _proj = Driver.getBridge();
            _supervisor = Functions.InitialSystem(_proj);
            _supervisor = _proj.loginSU(_supervisor.Username, _supervisor.Password);
            for (int i = 0; i < LOOPS; i++)
            {
                var forum = _proj.createForum(_supervisor.Id, "forum_" + i, "the " + i + "th forum",100, "admin_" + i,
                    "admin" + i,
                    "admin" + i + "@gmail.com", "admin1234", new Policy());
                var admin = _proj.login("admin_" + i, "admin1234", forum.Id);
                _admins[forum] = new List<User>();
                _admins[forum].Add(admin);
                _forums.Add(forum);
                _subforums[forum] = new List<Subforum>();
                _members[forum] = new List<User>();
                var member = _proj.subscribeToForum(110+i,"mod_" + i + "_1", "mod" + i + "_1", "mod" + i + "_1@gmail.com",
                    "mod1234",
                    forum.Id, arr, false);
                _members[forum].Add(member);
                var subforum = _proj.createSubForum(admin.Id,forum.Id, "subforum_" + i, "the " + i + "th subforum", member.Id,
                    DateTime.MaxValue);
                _moderators[subforum] = new List<User>();
                _posts[subforum] = new List<Post>();
                _subforums[forum].Add(subforum);
                _moderators[subforum].Add(member);
                member = _proj.subscribeToForum(120+i,"mod_" + i + "_2", "mod" + i + "_2", "mod" + i + "_2@gmail.com",
                    "mod1234",
                    forum.Id, arr, false);
                _members[forum].Add(member);
                _proj.addModerator(admin.Id,forum.Id, member.Id, subforum.Id, DateTime.MaxValue);
                _moderators[subforum].Add(member);
                var prevMember = member;
                var post = _proj.createThread(prevMember.Id,forum.Id, "title", "first message of forum_" + i,subforum.Id);
                _posts[subforum].Add(post);
                _postsByUser[member.Id]=new List<Post>();
                _postsByUser[member.Id].Add(post);
                for (int j = 0; j < LOOPS; j++)
                {
                    prevMember = member;
                    member = _proj.subscribeToForum(130 + j + i * 10, "user_" + i + "_" + j, "user" + i + "_" + j,
                        "user" + i + "_" + j + "@gmail.com", "user1234",
                        forum.Id, arr, false);
                    _members[forum].Add(member);
                    post = _proj.createReplyPost(prevMember.Id,forum.Id, "this is reply number " + i, post.Id);
                    _postsByUser[member.Id] = new List<Post>();
                    _postsByUser[member.Id].Add(post);

                    _posts[subforum].Add(post);
                }
            }
        }
        /// <summary>
        /// checks if we get the correct number of same user
        /// </summary>
        public void sameUser()
        {
            Assert.IsTrue(_proj.membersInDifferentForums(100).Length==LOOPS);
        }
        /// <summary>
        /// check if we get the correct number of posts
        /// </summary>
        [TestMethod]
        public void numberOfPosts()
        {
            var numPosts = _proj.subForumTotalMessages(_admins[_forums[0]][0].Id, _forums[0].Id,_subforums[_forums[0]][0].Id);
            Assert.IsTrue(numPosts == LOOPS*LOOPS);
        }

        /// <summary>
        /// cheks that we can correctly retrieve posts made by a user
        /// </summary>
        [TestMethod]
        public void postsByUser()
        {
            for (int i = 0; i < LOOPS; i++)
            {
                var postsByMember = _proj.postsByMember(_admins[_forums[0]][0].Id, _forums[0].Id, _members[_forums[0]][i].Id);
                var posts =
                    _posts[_subforums[_forums[0]][0]].Where((x) => x.GetAuthor.Id == _members[_forums[0]][i].Id);
                foreach (var post in posts)
                {
                    Assert.IsTrue(postsByMember.Any((x) => (x).Id==post.Id));
                }
            }
        }

        [TestMethod]
        public void moderatorReport()
        {
            var reports = _proj.moderatorReport(_admins[_forums[0]][0].Id, _forums[0].Id);
            
            foreach (var mod in reports.Mods)
            {
                Assert.IsTrue(_moderators[_subforums[_forums[0]][mod.SubForum.Id]].Any((x)=>x.Id==mod.Id));
            }
            var subforums = _subforums[_forums[0]];
            IEnumerable<User> modlist=new List<User>();
            modlist = subforums.Aggregate(modlist, (current, subforum) => current.Concat(_moderators[subforum]));
            foreach (var moderator in reports.Mods)
            {
                Assert.IsTrue(modlist.Any((x)=>x.Id==moderator.Id));
            }

            foreach (var mod in reports.Mods)
            {
                var posts = _proj.postsByMember(_admins[_forums[0]][0].Id, _forums[0].Id, mod.Id);
                foreach (var post in posts)
                {
                    Assert.IsTrue(_postsByUser[mod.Id].Any((x)=>x.Id==post.Id));
                }
            }
        }


    }
}
