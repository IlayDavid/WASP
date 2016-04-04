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
            Forum forum = new Forum("stack", "desc");
            Subforum sf = new Subforum("subForum", "someDescription");
            Member author = new Member("edan", "habler", "mail@mail.com", "123",forum);
            Post post = new Post("title", "content", author, DateTime.Now,null, sf, DateTime.Today);
            Post tempPost2 = new Post("title", "content",author, DateTime.Today, null, sf, DateTime.Today);

            // act
            sf.AddThread(tempPost2);
            sf.RemoveThread(tempPost2); // check if remove works
            sf.AddThread(post);
           
            // assert
            Assert.AreEqual(1, sf.GetThreads().Count, "check if subforum was added");
            Assert.AreEqual(sf, sf.GetThread(sf.Id), "check if subforum has right key ");


        }

        public void moderatorTest()
        {

            // arrange
            Forum forum = new Forum("stack", "desc");
            Subforum sf = new Subforum("subForum", "someDescription");
            forum.AddSubForum(sf);
            DateTime dateNow = DateTime.Now;
            DateTime future = DateTime.Today;
         
            Member author = new Member( "edan", "habler", "mail@mail.com", "123",forum);
            Member tempUser = new Member( "userForDelete", "ToRemove", "mail@mail.com", "123",forum);

         
            // act
            sf.AddModerator(author, future);
            sf.AddModerator(tempUser, dateNow);
            sf.RemoveModerator(tempUser);
            // assert
            Assert.AreEqual(1, sf.GetModerators().Count, "checking if moderator added successfully");
            // *to check why getModerator recives author**//
            Assert.AreEqual(author, sf.GetModerator(author), "check if right moderaitor deleted");



        }











    }
}
