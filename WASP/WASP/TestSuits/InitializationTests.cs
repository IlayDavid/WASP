using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.Server;

namespace WASP.TestSuits
{
    [TestClass]
    public class InitializationTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            DataBaseManager.Instance.clear();
            SuperUser.clear();
        }
        [TestMethod]
        public void AttemptInitialize()
        {
            var server = new Server.Server();
            var s = server.initialize();
            Assert.IsTrue(s.Equals("system initialized"),"server failed to initialize");
        }
        [TestMethod]
        public void AttemptInitializeTwice()
        {
            var server = new Server.Server();
            server.initialize();
            var s = server.initialize();
            Assert.IsTrue(s.Equals("already initialized. action failed."), "server initialized twice. this should not happen!");
        }
        [TestMethod]
        public void SuperUserInDBM()
        {
            var server = new Server.Server();
            server.initialize();
            const string SUPERUSERNAME = "admin";
            const string SUPERPASSWORD = "wasp1234Sting";
            Assert.IsTrue((DataBaseManager.Instance.GetUser(SUPERPASSWORD,SUPERUSERNAME) is SuperUser), "initialize failed to add the superuser to the database");
        }

    }
}