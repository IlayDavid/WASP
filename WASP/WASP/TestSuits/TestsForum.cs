using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WASP.TestSuits
{
    class TestsForum
    {

        [TestMethod]
        public void memberTests()
        {

            // arrange
            Forum forum = new Forum("name", "desc");
            Member member = new Member("edan", "userName", "email@email.com", "123", forum);
            List<Member> members;
            List<Member> membersAfterDelete;
            Member fake = new Member("user", "name", "email", "pass", null);

            bool succDelete;

            // act

            forum.AddMember(member);
            members = forum.GetMembers();
            forum.RemoveMember(member);
            membersAfterDelete = forum.GetMembers();
            succDelete = forum.RemoveMember(fake);
        

            // assert
           Assert.AreEqual(1, members.Count, "checking if member added successfully");
            Assert.AreEqual(true, members.Contains(member), "checking if member added successfully");
            Assert.AreEqual(0, membersAfterDelete.Count, "checking if member removed successfully");
            Assert.AreEqual(false, succDelete, "Checking if it is possible to remove a non-existent user.");
        }
        [TestMethod]

        public void adminTest()
        {
            // arrange
            Forum forum = new Forum("stackOverFlow", "description");
            Member admin = new Member( "edan", "userName", "email@email.com", "123",forum);
            bool isAdmin;
            // act
            forum.AddAdmin(admin);
            isAdmin = forum.IsAdmin(admin);
            
            // assert
            Assert.AreEqual(true, isAdmin, "checking if admin added successfully");
            Assert.AreEqual(1, forum.GetAdmins().Count, "check if addmin was added");
            // remove admin
            forum.RemoveAdmin(admin);
            Assert.AreEqual(0, forum.GetAdmins().Count, "check if addmin was deleted");

        }


        [TestMethod]

        public void subForumTest()
        {
            // arrange
            Subforum sf = new Subforum("subForum", "someDescription");
            Forum forum = new Forum("stackOverFlow", "description");
            bool isSf = false;
            // act
            forum.AddSubForum(sf);
            isSf = forum.IsSubForum(sf.Id);
            // assert
            Assert.AreEqual(true, isSf, "checking if subForum added successfully");
            Assert.AreEqual(1, forum.GetSubForum().Count, "check if subforum was added");

        }














    }
}
