using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.DataClasses;

namespace AccTests.Tests.ServerSide
{
    [TestClass]
    public class ClientPolicyTests
    {
        private WASPClientBridge _proj;
        private SuperUser _supervisor;
        [TestInitialize]
        public void setUp()
        {
            Driver.getBridge().Clean();
            _proj = ClientDriver.getBridge();
            
            _supervisor = ClientFunctions.InitialSystem(_proj);
            _supervisor = _proj.loginSU(_supervisor.userName, _supervisor.password);

        }
        /*
        /// <summary>
        /// password test
        /// Positive Test: tests that policy doesn't fail a good registery 
        /// </summary>
        [TestMethod]
        public void Policy1()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum1", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy(null, true, 5));
            var mem=_proj.subscribeToForum(23,"mem1", "mem", "mem1@post.bgu.ac.il", "mem123", forum.Id);
            Assert.IsNotNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (lack of number in password)
        /// </summary>
        [TestMethod]
        public void Policy2()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum2", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy(null, true, 5));
            var mem = _proj.subscribeToForum(23,"mem1", "mem", "mem1@post.bgu.ac.il", "member", forum.Id);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (password too short)
        /// </summary>
        [TestMethod]
        public void Policy3()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum3", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy(null, true, 5));
            var mem = _proj.subscribeToForum(23,"mem1", "mem", "mem1@post.bgu.ac.il", "mem1", forum.Id);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (password too short and lack of number)
        /// </summary>
        [TestMethod]
        public void Policy4()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum4", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy(null, true, 5));
            var mem = _proj.subscribeToForum(23,"mem1", "mem", "mem1@post.bgu.ac.il", "mem", forum.Id);
            Assert.IsNull(mem);
        }
        /// <summary>
        /// password test
        /// Negative Test: tests that the policy fails a bad registery (admin doesn't follow policy)
        /// </summary>
        [TestMethod]
        public void Policy5()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum5", "forum",22, "admin", "admin", "admin@gmail.com", "admi", new Policy(null, true, 5));
            Assert.IsNull(forum);
        }
        /// <summary>
        /// changing policy test
        /// Positive Test: attempts to change the policy to no policy and add a member
        /// </summary>
        [TestMethod]
        public void ChangePolicy()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy(null, true, 5));
            forum.AddPolicy(new Policy());
            _proj.defineForumPolicy(_supervisor.Id, forum.Id);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            Assert.IsNotNull(mem);
        }
        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checking that the normal policy causes no issue
        /// </summary>
        [TestMethod]
        public void NewModeratorPolicy1()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy());
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum=_proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check=_proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }
        /// <summary>
        /// adding moderator policy tests
        /// Negative Test: a: checking that the adding member as moderator fails if he lacks seniority,
        ///                b: that admin can appoint himself as the first moderator regardless of seniority
        /// </summary>
        [TestMethod]
        public void NewModeratorPolicy2()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, 1000));
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }

        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>

        [TestMethod]
        public void NewModeratorPolicy3()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, -5));
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }


        /// <summary>
        /// adding moderator policy tests
        /// Positive Test: checks that after duration you can creat the moderator
        /// </summary>
        [TestMethod]
        public async void NewModeratorPolicy4()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new NewModeratorPolicy(null, 1));
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            await wait(2); //wait for seniority
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }


        /// <summary>
        /// security policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>

        [TestMethod]
        public void SecurityPolicy1()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com",
                "admin1234", new SecurityPolicy());
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }


        /// <summary>
        /// security policy tests
        /// Positive Test: checking that negative input causes no issue
        /// </summary>
        [TestMethod]
        public void SecurityPolicy2()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com",
                "admin1234", new SecurityPolicy(null, -5));
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
        }
        /// <summary>
        /// security policy tests
        /// Negative Test: checks that after the duration, the user cannot log in
        /// </summary>
        [TestMethod]
        public async void SecurityPolicy3()
        {
            var forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new SecurityPolicy(null, 1));
            var admin = _proj.login("admin", "admin1234", forum.Id);
            var subforum = _proj.createSubForum(admin.id,forum.Id, "name", "description", admin.id, DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            var mem = _proj.subscribeToForum(23,"a", "a", "a.b@c.d", "a", forum.Id);
            var check = _proj.addModerator(admin.id,forum.Id, mem.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(check);
            mem = _proj.login(mem.userName, mem.password, forum.Id);
            Assert.IsNotNull(mem);

            await wait(2); //wait for password to expire
            mem = _proj.login(mem.userName, mem.password, forum.Id);
            Assert.IsNull(mem);
             admin = _proj.login("admin", "admin1234", forum.Id);
            Assert.IsNull(admin);
        }
        */
        /// <summary>
        /// positive Test: creates a number of users, and checks that they can create posts and read them correctly
        /// </summary>
        /*
        private Forum _forum;
        private User _admin;
        private Subforum _subforum;
        private int counter = 25;
        [TestMethod]
        
        public void StressTest1()
        {
            _forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy());
            _admin = _proj.login("admin", "admin1234", _forum.Id);
            _subforum = _proj.createSubForum(_admin.Id,_forum.Id, "name", "description", _admin.Id, DateTime.MaxValue);
            Assert.IsNotNull(_subforum);

            Thread[] threads = new Thread[50];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(postsLoopSuccess);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            
        }
        /// <summary>
        /// Negative Test: creates a too many users and checks that some of them drop
        /// </summary>
        [TestMethod]
        public void StressTest2()
        {
            _forum = _proj.createForum(_supervisor.Id, "forum", "forum",22, "admin", "admin", "admin@gmail.com", "admin1234", new Policy());
            _admin = _proj.login("admin", "admin1234", _forum.Id);
            _subforum = _proj.createSubForum(_admin.Id,_forum.Id, "name", "description", _admin.Id, DateTime.MaxValue);
            Assert.IsNotNull(_subforum);

            Thread[] threads = new Thread[50];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(postsLoopSuccess);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            Thread[] badThreads= new Thread[500];

            for (int i = 0; i < threads.Length; i++)
            {
                badThreads[i] = new Thread(postsLoopFail);
            }

            foreach (Thread thread in badThreads)
            {
                thread.Start();
            }

            foreach (Thread thread in badThreads)
            {
                thread.Join();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

        }
        private async void postsLoopSuccess()
        {
            var mem = _proj.subscribeToForum(counter++,"a", "a", "a.b@c.d", "a", _forum.Id);
            mem = _proj.login(mem.Username, mem.Password, _forum.Id);
            Assert.IsNotNull(mem);
            var rnd=new Random();
            var post = _proj.createThread(mem.Id,_forum.Id, "title" + rnd.Next(), ""+rnd.Next(), _subforum.Id);
            var prevPost = post;
            for (int i = 0; i < 60; i++)
            {
                prevPost = post;
                post = _proj.createReplyPost(mem.Id,_forum.Id, "" + rnd.Next(), prevPost.Id);
                Assert.IsTrue(post.InReplyTo.Content.Equals(prevPost.Content));
                await wait(1); //wait 1 second to simulate a person waiting
            }
        }

        private async void postsLoopFail()
        {
            var mem = _proj.subscribeToForum(counter++,"a", "a", "a.b@c.d", "a", _forum.Id);
            Assert.IsNotNull(mem);//register shouldn't fail, only login (change return value?)
            mem = _proj.login(mem.Username, mem.Password, _forum.Id);
            Assert.IsNull(mem);
        }

        private static async Task<int> wait(int duration)
        {
            Task.Delay(duration);
            return duration;
        }
        */

    
    }
}
