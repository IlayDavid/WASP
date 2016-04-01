using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace AccTests
{

    [TestFixture]
    public class InitializeTests
    {
        private WASPBridge _proj;

        [TestFixtureSetUp]
        public void SystemSetUp()
        {
            _proj = Driver.getBridge();
        }

        [SetUp]     //before each Test
        public void SetUp()
        {
        }

        [TearDown]     //before each Test
        public void TearDown()
        {

        }
        /// <summary>
        /// checks that the initialization return a vaild supervisor
        /// </summary>
        [Test]
        public void initTest1()
        {
            Assert.Equals(_proj.initialize(), null);
        }

        /// <summary>
        /// check that the supervior which return from the initialization, sign-in system
        /// </summary>
        [Test]
        public void initTest2()
        {
            User supervisor = _proj.initialize();
            Assert.AreEqual(_proj.login(supervisor._userName, supervisor._password), 1);
        }

    }
}
