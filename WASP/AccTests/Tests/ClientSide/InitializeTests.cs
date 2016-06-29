using Client.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WASP.Exceptions;
namespace AccTests
{

    [TestClass]
    public class ClientInitializeTests
    {
        private WASPClientBridge _proj = ClientDriver.getBridge();

        
        /// <summary>
        /// Positive Test: checks that the initialization return a vaild supervisor
        /// </summary>
        [TestMethod]
        public void initTest1()
        {
            Driver.getBridge().Clean();
            SuperUser supervisor = _proj.initialize("Moshe", "SuperUser",90, "moshe@post.bgu.ac.il", "moshe123");
            //Assert.AreEqual(supervisor.name, "Moshe");
            Assert.AreEqual(supervisor.userName , "SuperUser");
            //Assert.AreEqual(supervisor.email, "moshe@post.bgu.ac.il");
            //Assert.AreEqual(supervisor.password, "moshe123");
        }
        
        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void initTest2()
        {
            Driver.getBridge().Clean();
            try
            {
                _proj.initialize("", "SuperUser", 99, "moshe@post.bgu.ac.il", "moshe123");
            }
            catch (System.Exception e)
            {
                Assert.IsTrue(e.Message.Contains("ERROR:"));
            }
        }
        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void initTest3()
        {
            Driver.getBridge().Clean();
            try
            {
                _proj.initialize("Moshe", "", 98, "moshe@post.bgu.ac.il", "moshe123");
            }
            catch (System.Exception e)
            {
                Assert.IsTrue(e.Message.Contains("ERROR:"));
            }
        }
    }
}