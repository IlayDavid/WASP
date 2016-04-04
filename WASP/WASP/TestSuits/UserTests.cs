using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.TestSuits
{
    class UserTests
    {


        [TestMethod]

        

        public void UserReplies()
        {
            // arrange
            Member user1 = new Member("edan", "habler", "email", "123",null);
            Post post1 = new Post("title", "content", user1, DateTime.Now, null, null, DateTime.Now);
            int id = post1.Id;
            // act
            user1.AddPost(post1);
            // assert
            Assert.AreEqual(post1, user1.GetPost(post1.Id), "checking if user1 added post1 to his posts");
            Assert.AreEqual(1, user1.GetAllPosts().Count, "check if added post1 to his list of posts");

        }




    }
}
