﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            SuperUser supervisor = _proj.initialize("Moshe", "SuperUser", "moshe@post.bgu.ac.il", "moshe123");
            Assert.AreEqual(supervisor.Name, "Moshe");
            Assert.AreEqual(supervisor.UserName , "SuperUser");
            Assert.AreEqual(supervisor.Email, "moshe@post.bgu.ac.il");
            Assert.AreEqual(supervisor.Password, "moshe123");
        }

        /// <summary>
        /// Nagative Test: lack of information
        /// </summary>
        [TestMethod]
        public void initTest2()
        {
            Assert.IsNull(_proj.initialize("", "SuperUser", "moshe@post.bgu.ac.il", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "", "moshe@post.bgu.ac.il", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "SuperUser", "", "moshe123"));
            Assert.IsNull(_proj.initialize("Moshe", "SuperUser", "moshe@post.bgu.ac.il", ""));
        }

    }
}