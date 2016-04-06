using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class serverTests
    {
        private static ServerAPI server=new Server.Server();
        private static SuperUser _supervisor=null;
        private static Forum forum = null;
        private int _subforumId = 0;
        private int _threadId = 0;
        private Member _admin = null;
        private Member _member = null;

        [TestInitialize]
        public static  void initializeTests(TestContext tc)
        {
            _supervisor=server.initialize("super", "super", "super@user.man", "super");
            forum = server.createForum(_supervisor, "forum", "the forum", "admin", "admin", "e@mail.com", "admin", new PasswordPolicy());
        }
        [TestMethod]
        public void superuserLogin()
        {
            //asserts the we get the superuser on login with good input
            var check = server.login("super", "super");
            Assert.AreEqual(check, _supervisor);
        }
        [TestMethod]
        public void superuserLogin2()
        {
            //asserts the we don't get the superuser on login with bad input
            var check = server.login("b", "b");
            Assert.AreNotEqual(check, _supervisor);
        }
        [TestMethod]
        public void getforum()
        {
            var serverForum = server.getForum(null, forum.Id);
            Assert.IsTrue(forum == serverForum);
        }
        [TestMethod]
        public void login1()
        {
            //assert that admin can login
            var admin = server.login("admin", "admin", forum);
            _admin = admin;
            Assert.IsNotNull(admin);
        }
        [TestMethod]
        public void subscribeUser1()
        {
            //assert that subscribed user requires an input.
            var nothin = server.subscribeToForum("", "", "", "", forum);
            Assert.IsTrue(nothin == null);
        }
        [TestMethod]
        public void subscribeUser2()
        {
            //assert that we can create a user
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            _member = member;
            Assert.IsNotNull(member);
        }
        [TestMethod]
        public void createSubForum1()
        {
            //assert we cannot create a subforum with no description or name
            var subforum= server.createSubForum(_admin, "", "", _admin, DateTime.Now);
            Assert.IsNull(subforum);
        }
        [TestMethod]
        public void createSubForum2()
        {
            //assert an admin can create a subforum with himself as moderator
            var admin = server.login("admin", "admin", forum);
            var subforum=server.createSubForum(admin, "sub", "forum", admin, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
        }
        [TestMethod]
        public void createSubForum3()
        {
            //assert an admin can not create a second identicle subforum
            var subforum= server.createSubForum(_admin, "sub", "forum", _admin, DateTime.Now);
            Assert.IsNull(subforum);
        }
        [TestMethod]
        public void createSubForum4()
        {
            //assert an admin can create a subforum with user as the moderator
            var admin = server.login("admin", "admin", forum);
            var subforum=server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            _subforumId = subforum.Id;
            Assert.IsNotNull(subforum);
        }
        [TestMethod]
        public void checkGetSubforum()
        {
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub", "forum", admin, DateTime.MaxValue);
            var inserverSubforum = server.getSubforum(admin, subforum.Id);
            Assert.IsTrue(subforum==inserverSubforum);
        }
        [TestMethod]
        public void createThread1()
        {
            //asserts thread cannot accept an empty body and content
            var thread = server.createThread(_member, "", "", DateTime.Now, server.getSubforum(_member, _subforumId));

            Assert.IsNull(thread);
        }
        [TestMethod]
        public void createThread2()
        {
            //asserts thread can be created
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var thread = server.createThread(member, "title", "body", time, subforum);
            _threadId = thread.Id;
            Assert.IsNotNull(thread);
        }
        [TestMethod]
        public void createReply1()
        {
            //asserts that replies requires a body
            var reply = server.createReplyPost(_member, "", DateTime.Now, server.getThread(_member, _threadId));
            Assert.IsNull(reply);
        }
        [TestMethod]
        public void createReply2()
        {
            //asserts that we can reply to a thread
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var thread = server.createThread(member, "title", "body", time, subforum);
            var reply = server.createReplyPost(member, "reply!", time, thread);
            Assert.IsNotNull(reply);
        }
        [TestMethod]
        public void createReply3()
        {

            //asserts we can reply to a post
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var thread = server.createThread(member, "title", "body", time, subforum);
            var reply = server.createReplyPost(member, "reply!", time, thread);
            var reply2 = server.createReplyPost(member, "reply to reply", time, reply);
            Assert.IsNotNull(reply);
        }
        [TestMethod]
        public void addModerator()
        {
            //checks that we can add a moderator
            var member = server.subscribeToForum("user", "user", "us@e.r", "user", forum);
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var check=server.addModerator(admin, member, subforum, time);
            Assert.IsTrue(check>=0);
        }
        [TestMethod]
        public void getModeratorTermTime()
        {
            //assert that we can get the correct moderator term time
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var check = server.getModeratorTermTime(admin, admin, subforum);
            Assert.IsTrue(DateTime.MaxValue==check);
        }

        [TestMethod]
        public void changeModeratorTermTime()
        {
            var admin = server.login("admin", "admin", forum);
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var check = server.updateModeratorTerm(admin, admin, subforum, time);
            Assert.IsTrue(check>=0);
        }

        [TestMethod]
        public void testGetAllModerators1()
        {
            //sets up the forum
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                new PasswordPolicy());
            var time = DateTime.Now.Add(new TimeSpan(6, 6, 6));
            var admin = server.login("admin", "admin", forum);
            //create the subforum and populate it
            var subforum = server.createSubForum(admin, "sub2", "forum2", admin, DateTime.MaxValue);
            for (int i = 0; i < 10; i++)
            {
                var s = i.ToString();
                var mod = server.subscribeToForum(s, s, "a.b@c", s, forum);
                server.addModerator(admin, mod, subforum, time);
            }
            var subforumsModerators = server.getModerators(admin, subforum);
            //asserts that both the people added to the list and the actual list of moderators returned are the same
            Assert.AreEqual(11, subforumsModerators.Count);
        }
        [TestMethod]
        public void getAllSubforums()
        {
            //sets up the forum
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                new PasswordPolicy());
            var subforums=new List<Subforum>();
            for (int i = 0; i < 10; i++)
            {
                var s = i.ToString();
                var subforum = server.createSubForum(_admin, s, s, _admin, DateTime.Now);
                subforums.Add(subforum);
            }
            var serverSubforums = server.getSubforums(_admin, forum);
            //asserts that both the people added to the list and the actual list of moderators returned are the same
            Assert.IsTrue(Enumerable.SequenceEqual(subforums.OrderBy(fList => fList),
              serverSubforums.OrderBy(sList => sList)), "failed to add and retrieve the same number of subforums");

        }

        [TestMethod]
        public void confirmMailTest1()
        {
            //assert that we can confirm an email
            _admin = forum.GetAdmins()[0];
            Assert.IsTrue(server.confirmEmail(_admin)>=0);
        }
        [TestMethod]
        public void confirmMailTest2()
        {
            //assert that we can confirm an email multiple times
            server.confirmEmail(_admin);
            server.confirmEmail(_admin);
            server.confirmEmail(_admin);
            Assert.IsTrue(server.confirmEmail(_admin) >= 0);
        }
        [TestMethod]
        public void updateForumPolicy()
        {
            //assert that we can change the forum's policy
            //currently doesn't work. built for code coverage and for compiler to shout on update.
            //TODO: FIX!
            server.defineForumPolicy(_supervisor, forum);
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void delPost1()
        {
            //asserts that we can delete an opening post we created
            var subforum= forum.GetSubForum(_subforumId);
            var post = server.createThread(_admin, "title", "content", DateTime.Now, subforum);
            var check=server.deletePost(_admin, post);
            var checkPostExists = server.getThread(_admin, post.Id);
            Assert.IsTrue(check>=0&&checkPostExists==null);
        }

        [TestMethod]
        public void delPost2()
        {
            //asserts that we can delete an opening post with replies
            var subforum = forum.GetSubForum(_subforumId);
            var post = server.createThread(_admin, "title", "content", DateTime.Now, subforum);
            var reply = server.createReplyPost(_admin, "sd", DateTime.Now, post);
            var reply2 = server.createReplyPost(_admin, "sd2", DateTime.Now, post);
            var replyreply = server.createReplyPost(_admin, "gasd", DateTime.Now, reply);
            var check = server.deletePost(_admin, post);
            var checkPostExists = server.getThread(_admin, post.Id);
            Assert.IsTrue(check >= 0&&checkPostExists==null);
        }
        [TestMethod]
        public void delPost3()
        {
            //asserts that we can delete a reply post with replies
            var subforum = forum.GetSubForum(_subforumId);
            var post = server.createThread(_admin, "title", "content", DateTime.Now, subforum);
            var reply = server.createReplyPost(_admin, "sd", DateTime.Now, post);
            var reply2 = server.createReplyPost(_admin, "sd2", DateTime.Now, post);
            var replyreply = server.createReplyPost(_admin, "gasd", DateTime.Now, reply);
            var check = server.deletePost(_admin, reply);
            var checkPostExists = server.getThread(_admin, reply.Id);
            Assert.IsTrue(check >= 0 && checkPostExists == null);
        }

        [TestMethod]
        public void delPost4()
        {
            //asserts that we can delete a reply to a reply
            var subforum = forum.GetSubForum(_subforumId);
            var post = server.createThread(_admin, "title", "content", DateTime.Now, subforum);
            var reply = server.createReplyPost(_admin, "sd", DateTime.Now, post);
            var reply2 = server.createReplyPost(_admin, "sd2", DateTime.Now, post);
            var replyreply = server.createReplyPost(_admin, "gasd", DateTime.Now, reply);
            var check = server.deletePost(_admin, replyreply);
            var checkPostExists = server.getThread(_admin, replyreply.Id);
            Assert.IsTrue(check >= 0 && checkPostExists == null);
        }
        [TestMethod]
        public void getAdminTest()
        {
            //asserts that the get returns the forum's admin
            var admin = server.getAdmin(_supervisor, forum, "admin");
            Assert.AreSame(admin, _admin);
        }

        [TestMethod]
        public void testGetAdmins()
        {
            var admins=server.getAdmins(_supervisor, forum);
            Assert.IsTrue(admins.Count==1&&admins.First((x)=>true)==_admin);
        }

        [TestMethod]
        public void getForumTest1()
        {
            //assert that we recieve the correct forum
            var f = server.getForum(_admin, forum.Id);
            Assert.Equals(f, forum);
        }

        [TestMethod]
        public void getForumTest2()
        {
            //assert that we recieve null on bad request
            var f = server.getForum(_admin, int.MinValue);
            Assert.IsNull(f);
        }

        [TestMethod]
        public void testGetAllMembers()
        {
            //sets up the forum
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                new PasswordPolicy());
            var members = new List<Member>();
            //create the subforum and populate it
            members.Add(_admin);
            var subforum = server.createSubForum(_admin, "subforum", "descp", _admin, DateTime.Now);
            for (int i = 0; i < 10; i++)
            {
                var s = i.ToString();
                var mod = server.subscribeToForum(s, s, "a.b@c", s, forum);
            }
            var serverMembers = server.getMembers(_admin,forum);
            //asserts that both the people added to the list and the actual list of moderators returned are the same
            Assert.IsTrue(Enumerable.SequenceEqual(members.OrderBy(fList => fList),
                         serverMembers.OrderBy(sList => sList)), "failed to add and retrieve the same number of moderators");
        }

        [TestMethod]
        public void testPolicy1()
        {
            //asserts that default policy works
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy());
            var pass = "a";
            var member=server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNotNull(member);
        }
        [TestMethod]
        public void testPolicy2()
        {
            //asserts that diversity doesnt affect a mix of number and letter
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null,true));
            var pass = "a9";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNotNull(member);
        }
        [TestMethod]
        public void testPolicy3()
        {
            //assert that diversity blocks a letter only password
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, true));
            var pass = "a";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNull(member);
        }
        [TestMethod]

        public void testPolicy4()
        {
            //assert that length policy blocks short passwords
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, false,5));
            var pass = "a";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNull(member);
        }
        [TestMethod]

        public void testPolicy5()
        {
            //assert that length policy doesn't block long passwords
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, false, 5));
            var pass = "abcdegdsa";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNotNull(member);
        }
        [TestMethod]

        public void testPolicy6()
        {
            //assert that diverse && length policy doesn't block long diverse passwords
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, true, 5));
            var pass = "abcdegdsa5";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNotNull(member);
        }
        [TestMethod]

        public void testPolicy7()
        {
            //assert that diverse && length policy blocks short diverse passwords
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, true, 5));
            var pass = "sa5";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNull(member);
        }
        [TestMethod]

        public void testPolicy8()
        {
            //assert that diverse && length policy blocks long non-diverse passwords
            var forum = server.createForum(_supervisor, "forum", "forum mods", "admin", "admin", "a@b.c", "admin",
                            new PasswordPolicy(null, true, 5));
            var pass = "abcdegdsa";
            var member = server.subscribeToForum("a", "a", "a@d.s", pass, forum);
            Assert.IsNull(member);
        }

    }
}
