using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WASP.DataClasses;

namespace WASP.TestSuits
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserReplies()
        {
            // arrange
            User user1 = new User(0,"edan", "habler", "email", "123",null);
            Post post1 = new Post("title", "content",0, user1, DateTime.Now,null,null,DateTime.Today, null);
            int id = post1.Id;
            // act
            user1.AddPost(post1);
            // assert
            Assert.AreEqual(post1, user1.GetPost(post1.Id), "checking if user1 added post1 to his posts");
            Assert.AreEqual(1, user1.GetAllPosts().Length , "check if added post1 to his list of posts");
        }
    }
}
