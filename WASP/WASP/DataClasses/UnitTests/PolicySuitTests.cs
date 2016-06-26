using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses.DAL_EXCEPTIONS;

namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class PolicySuitTests

    {
        private DAL2 dal = new DALSQL();

        [TestCleanup]
        public void CleanUp()
        {
            ((DALSQL)dal).Clean();
            DALSQL.GetBackUp();
        }
        [TestInitialize]
        public void SetUp()
        {
            DALSQL.BackUpAll();
            ((DALSQL)dal).Clean();

        }



        [TestMethod]
        public void AddPolicyTest1()
        {
            string[] questions1 = { "Hi", "Bye" };
            Policy pol = new Policy(-1, (Policy.PostDeletePolicy)1, TimeSpan.FromTicks(1000), true, TimeSpan.FromTicks(100), 10, questions1, true);
            pol = dal.CreatePolicy(pol);
            pol = dal.GetPolicy(pol.Id);
            Assert.IsTrue(pol.MinimumSeniority == TimeSpan.FromTicks(100));
            Assert.IsTrue(pol.PasswordTimeSpan == TimeSpan.FromTicks(1000));
            Assert.IsTrue((int) pol.SelectedPostDeletePolicy == 1);
            Assert.IsTrue(pol.UsersLoad == 10);
            Assert.IsTrue(pol.Questions[0].Equals(questions1[0]));
            Assert.IsTrue(pol.Questions[1].Equals(questions1[1]));
            Assert.IsTrue(pol.NotifyOffline == true);
        }
        [TestMethod]
        public void AddPolicyTest2()
        {
            string[] questions1 = { "Hi", "Bye" };
            string[] questions2 = { "Bye", "Hi" };
            Policy pol1 = new Policy(-1, (Policy.PostDeletePolicy)1, TimeSpan.FromTicks(1000), true, TimeSpan.FromTicks(100), 10,questions1, true);
            pol1 = dal.CreatePolicy(pol1);
            pol1 = dal.GetPolicy(pol1.Id);
            Policy pol2 = new Policy(-1, (Policy.PostDeletePolicy)6, TimeSpan.FromTicks(10000), true, TimeSpan.FromTicks(1000), 100, questions2, false);
            pol2 = dal.CreatePolicy(pol2);
            pol2 = dal.GetPolicy(pol2.Id);
            Assert.IsTrue(pol1.MinimumSeniority == TimeSpan.FromTicks(100));
            Assert.IsTrue(pol1.PasswordTimeSpan == TimeSpan.FromTicks(1000));
            Assert.IsTrue((int)pol1.SelectedPostDeletePolicy == 1);
            Assert.IsTrue(pol1.UsersLoad == 10);
            Assert.IsTrue(pol1.Questions[0].Equals(questions1[0]));
            Assert.IsTrue(pol1.Questions[1].Equals(questions1[1]));
            Assert.IsTrue(pol2.Questions[0].Equals(questions2[0]));
            Assert.IsTrue(pol2.Questions[1].Equals(questions2[1]));
            Assert.IsTrue(pol1.NotifyOffline == true);
            Assert.IsTrue(pol2.NotifyOffline == false);

        }

        [TestMethod]
        public void UpdatePolicyTest3()
        {
                        string[] questions1 = { "Hi", "Bye" };
            string[] questions2 = { "Bye", "Hi" };
            Policy pol = dal.CreatePolicy(new Policy(-1, (Policy.PostDeletePolicy)2, TimeSpan.FromTicks(1000), true, TimeSpan.FromTicks(100), 10, questions1, true));
            pol = dal.UpdatePolicy(new Policy(pol.Id, (Policy.PostDeletePolicy)1, TimeSpan.FromTicks(9), false, TimeSpan.FromTicks(8), 6, questions2, false));
            Assert.IsTrue(pol.MinimumSeniority == TimeSpan.FromTicks(8));
            Assert.IsTrue(pol.PasswordTimeSpan == TimeSpan.FromTicks(9));
            Assert.IsTrue((int)pol.SelectedPostDeletePolicy == 1);
            Assert.IsTrue(pol.EmailVerfication == false);
            Assert.IsTrue(pol.UsersLoad == 6);
            Assert.IsTrue(pol.Questions[0].Equals(questions2[0]));
            Assert.IsTrue(pol.Questions[1].Equals(questions2[1]));
            Assert.IsTrue(pol.NotifyOffline == false);

        }

        [TestMethod]
        public void GetForumPolicyTest4()
        {

        }
    }
}
