using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WASP.TestSuits
{
    class SubForumTests
    {


        [TestMethod]

        public void subForumTest()
        {
            
            // arrange
            Subforum sf = new Subforum(0, "subForum", "someDescription");
            User author = new User(0, false, "edan", "habler", "mail@mail.com", "123");
            int id = 0, id2 = 1;
            Post post = new Post("title", "content",id, author, DateTime.Now,null, sf, DateTime.Today);
            Post tempPost2 = new Post("title", "content", id2, author, DateTime.Today, null, sf, DateTime.Today);

            // act
            sf.AddThread(tempPost2);
            sf.RemoveThread(tempPost2.Id); // check if remove works
            sf.AddThread(post);
           
            // assert
            Assert.AreEqual(1, sf.GetThreads().Length, "check if subforum was added");
            Assert.AreEqual(sf, sf.GetThread(sf.Id), "check if subforum has right key ");


        }

        public void moderatorTest()
        {

            // arrange
            Subforum sf = new Subforum(0, "subForum", "someDescription");
            DateTime dateNow = DateTime.Now;
            DateTime future = DateTime.Today;
         
            User author = new User(0, false, "edan", "habler", "mail@mail.com", "123");
            User tempUser = new User(1, false, "userForDelete", "ToRemove", "mail@mail.com", "123");

         
            // act
            sf.AddModerator(author, future);
            sf.AddModerator(tempUser, dateNow);
            sf.RemoveModerator(tempUser.Id);
            // assert
            Assert.AreEqual(1, sf.GetModerators().Length, "checking if moderator added successfully");
            Assert.AreEqual(author, sf.GetModerator(author.Id), "check if right moderaitor deleted");



        }











    }
}
