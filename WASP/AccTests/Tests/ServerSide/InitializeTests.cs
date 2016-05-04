using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;

namespace AccTests
{

    [TestClass]
    public class InitializeTests
    {
        private WASPBridge _proj = Driver.getBridge();

        
        /// <summary>
        /// Positive Test: checks that the initialization return a vaild supervisor
        /// </summary>
        [TestMethod]
        public void initTest1()
        {
            SuperUser supervisor = _proj.initialize("Moshe", "SuperUser",90, "moshe@post.bgu.ac.il", "moshe123");
            Assert.AreEqual(supervisor.name, "Moshe");
            Assert.AreEqual(supervisor.userName , "SuperUser");
            Assert.AreEqual(supervisor.email, "moshe@post.bgu.ac.il");
            Assert.AreEqual(supervisor.password, "moshe123");
        }

        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void initTest2()
        {
            Assert.IsNull(_proj.initialize("", "SuperUser",99, "moshe@post.bgu.ac.il", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "",98, "moshe@post.bgu.ac.il", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "SuperUser",97, "", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "SuperUser",96, "moshe@post.bgu.ac.il", ""));
        }

    }
}