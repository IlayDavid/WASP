using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;

namespace WASP.TestSuits
{
    [TestClass]
    public class UserTests
    {
        private DAL2 dal = new DALSQL();
        [TestMethod]
        public void UserReplies()
        {
            // arrange
            User user1 = new User(0,"edan", "habler", "email", "123",null,dal);
            Post post1 = new Post(0, "title", "content", user1, DateTime.Now,null,null,DateTime.Today, null);
            int id = post1.Id;
            // act
            user1.AddPost(post1);
            // assert
            Assert.AreEqual(post1, user1.GetPost(post1.Id), "checking if user1 added post1 to his posts");
            Assert.AreEqual(1, user1.GetAllPosts().Length , "check if added post1 to his list of posts");
        }
    }
}
