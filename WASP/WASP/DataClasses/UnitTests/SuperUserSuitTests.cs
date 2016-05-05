
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using WASP.DataClasses;
using WASP.DataClasses.DAL_EXCEPTIONS;
namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class SuperUserSuitTests
    {
        private DAL dal = new DALSQL();

        

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
        public void AddSuperUserTest1()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                SuperUser superUser = dal.GetSuperUser(315470047);
                Assert.IsTrue(superUser.Id == 315470047);
                Assert.IsTrue(superUser.Username.Equals("matansar"));
                Assert.IsTrue(superUser.Password.Equals("123"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddSuperUserTest2()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                SuperUser superuser1 = dal.GetSuperUser(315470047);
                dal.CreateSuperUser(new SuperUser(205857121, "matansar1", "1233"));
                SuperUser superuser2 = dal.GetSuperUser(205857121);

                Assert.IsTrue(superuser1.Id == 315470047);
                Assert.IsTrue(superuser1.Username.Equals("matansar"));
                Assert.IsTrue(superuser1.Password.Equals("123"));

                Assert.IsTrue(superuser2.Id == 205857121);
                Assert.IsTrue(superuser2.Username.Equals("matansar1"));
                Assert.IsTrue(superuser2.Password.Equals("1233"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void AddSuperUserTest3()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                Assert.Fail();
            }
            catch (ExistException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void UpdateSuperUserTest4()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                dal.UpdateSuperUser(new SuperUser(315470047, "matansar1", "1232"));
                SuperUser superUser = dal.GetSuperUser(315470047);

                Assert.IsTrue(superUser.Id == 315470047);
                Assert.IsTrue(superUser.Username.Equals("matansar1"));
                Assert.IsTrue(superUser.Password.Equals("1232"));

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetUsersTest5()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                dal.CreateSuperUser(new SuperUser(205857121, "matansar1", "1233"));
                SuperUser[] superusers = dal.GetSuperUsers( null);
                Assert.IsTrue(superusers.Length == 2);
                Assert.IsTrue(superusers[0].Username.Equals("matansar" ) || superusers[1].Username.Equals("matansar"));
                Assert.IsTrue(superusers[0].Username.Equals("matansar1") || superusers[1].Username.Equals("matansar1"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetSuperUsersTest6()
        {
            try
            {
                dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
                dal.CreateSuperUser(new SuperUser(205857121, "matansar1", "1233"));
                SuperUser[] superusers = dal.GetSuperUsers(new int [] { 315470047 });
                
                Assert.IsTrue(superusers.Length == 1);
                Assert.IsTrue(superusers[0].Username.Equals("matansar"));
                Assert.IsTrue(superusers[0].Id.Equals(315470047));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteSuperUserTest7()
        {
            dal.CreateSuperUser(new SuperUser(315470047, "matansar", "123"));
            dal.CreateSuperUser(new SuperUser(315470048, "matansar1", "1233"));
            int superUser1 = dal.GetSuperUser(315470047).Id;
            int superUser2 = dal.GetSuperUser(315470048).Id;

            dal.DeleteSuperUser(superUser1);
            Assert.IsTrue(dal.GetSuperUsers(null).Length == 1);
            dal.DeleteSuperUser(superUser2);
            Assert.IsTrue(dal.GetSuperUsers(null).Length == 0);
        }

    }


}