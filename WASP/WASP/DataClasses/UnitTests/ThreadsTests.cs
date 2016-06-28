
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses.DAL_EXCEPTIONS;
using System.Threading;
using System.Threading.Tasks;

namespace WASP.DataClasses.UnitTests
{
    [TestClass]
    public class ThreadsTests
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
        public void treadstests1()
        {
            Task[] tasks = new Task[100];
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                 {

                     Forum f1 = new Forum(-1, "mo", "blah", null);
                     Forum f2 = new Forum(-1, "bo", "blah", null);
                     dal.CreateForum(f1);
                     dal.CreateForum(f2);

                 });
            }
            Task.WaitAll(tasks[0], tasks[1], tasks[2], tasks[3], tasks[4]);

        }

    }


}