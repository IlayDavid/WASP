using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class ServerInitializationTests
    {
        private ServerAPI server = new Server.Server();
        private SuperUser _supervisor = null;
        private Forum forum = null;
        private int _subforumId = 0;
        private int _threadId = 0;

        [TestMethod]
        public void initializeTest1()
        {
            //asserts that we cannot receive empty inputs
            var chek = server.initialize("", "", "", "");
            Assert.IsNull(chek);
        }
        [TestMethod]
        public void initializeTest2()
        {
            //assert that email requires '.' and '@'
            var chek = server.initialize("a", "b", "c", "d");
            Assert.IsNull(chek);
        }
        [TestMethod]
        public void initializeTest3()
        {
            //asserts that we managed to initialize the system propperly
            var chek = server.initialize("a", "b", "a.b@g.c", "e");
            _supervisor = null;
            Assert.IsNotNull(chek);
        }
        [TestMethod]
        public void initializeTest4()
        {
            //asserts that we fail to create a second superuser
            var chek = server.initialize("abc", "def", "gef@bad.com", "sda");
            Assert.IsNull(chek);
        }

        [TestMethod]
        public void createForum1()
        {
            //asserts that we cannot give empty strings
            var check = server.createForum(_supervisor, "", "", "", "", "", "", new PasswordPolicy());
            Assert.IsNull(check);
        }

        [TestMethod]
        public void createForum2()
        {
            //asserts that we cannot give an e-mail without '.' and '@'
            var check = server.createForum(_supervisor, "a", "a", "a", "a", "a", "a", new PasswordPolicy());
            Assert.IsNull(check);
        }

        [TestMethod]
        public void createForum3()
        {
            //asserts that we can create a forum
            var check = server.createForum(_supervisor, "forum", "description", "admin", "admin", "e-mail@e.mail", "admin", new PasswordPolicy());
            forum = check;
            Assert.IsNotNull(check);
        }

        //TODO: check if this requirement is really required
        [TestMethod]
        public void createForum4()
        {
            //asserts that we cannot create two exact same forum
            var check = server.createForum(_supervisor, "forum", "description", "admin", "admin", "e-mail@e.mail", "admin", new PasswordPolicy());
            Assert.IsNull(check);
        }

        [TestMethod]
        public void testGetAllForums()
        {
            //setup
            var server = new Server.Server();
            var super=server.initialize("super", "super", "s.a@b", "super");
            var forums=new List<Forum>();
            //populate forums
            for (int i = 0; i < 10; i++)
            {
                var s = i.ToString();
                var forum = server.createForum(super, s, s, s, s, "a.b@c", s, new PasswordPolicy());
                forums.Add(forum);
            }
            //asserts that the list of forums we get from the server is the same as the one we created
            var serverForums = server.getAllForums(super);
            Assert.IsTrue(Enumerable.SequenceEqual(forums.OrderBy(fList => fList),
                         serverForums.OrderBy(sList => sList)));
        }
    }
}