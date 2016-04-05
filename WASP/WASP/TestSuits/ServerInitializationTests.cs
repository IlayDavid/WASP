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
        public void superuserLogin()
        {
            //asserts the we get the superuser on login with good input
            var check = server.login("a", "b");
            Assert.Equals(check, _supervisor);
        }
        [TestMethod]
        public void superuserLogin2()
        {
            //asserts the we don't get the superuser on login with bad input
            var check = server.login("b", "b");
            Assert.AreNotEqual(check, _supervisor);
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
    }
}