using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WASP.TestSuits
{
    [TestClass]
    public class serverTests
    {
        private ServerAPI server=new Server.Server();
        private SuperUser _supervisor=null;
        private Forum forum = null;
        [TestMethod]
        public void initializeTest1()
        {
            //asserts that we cannot receive empty inputs
            var chek=server.initialize("", "", "", "");
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
            var check=server.login("a", "b");
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
            var check=server.createForum(_supervisor, "", "", "", "", "", "");
            Assert.IsNull(check);
        }

        [TestMethod]
        public void createForum2()
        {
            //asserts that we cannot give an e-mail without '.' and '@'
            var check = server.createForum(_supervisor, "a", "a", "a", "a", "a", "a");
            Assert.IsNull(check);
        }

        [TestMethod]
        public void createForum3()
        {
            //asserts that we can create a forum
            var check = server.createForum(_supervisor, "forum", "description", "admin", "admin", "e-mail@e.mail", "admin");
            forum = check;
            Assert.IsNotNull(check);
        }

        //TODO: check if this requirement is really required
        [TestMethod]
        public void createForum4()
        {
            //asserts that we cannot create two exact same forum
            var check = server.createForum(_supervisor, "forum", "description", "admin", "admin", "e-mail@e.mail", "admin");
            Assert.IsNull(check);
        }

        

        [TestMethod]
        public void login1()
        {
            //assert that admin can login
            var admin = server.login("admin", "admin", forum);
            Assert.IsNotNull(admin);
        }


        [TestMethod]
        public void createSubForum1()
        {
            //assert we cannot create a subforum with no description or name
            var admin = server.login("admin", "admin", forum);
            server.createSubForum(admin, "", "", admin);
            Assert.IsNotNull(admin);
        }

        [TestMethod]
        public void createSubForum2()
        {
            //assert an admin can create a subforum with himself as moderator
            var admin = server.login("admin", "admin", forum);
            server.createSubForum(admin, "sub", "forum", admin);
            Assert.IsNotNull(admin);
        }

    }
}
