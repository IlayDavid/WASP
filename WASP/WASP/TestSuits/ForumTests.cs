using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WASP.TestSuits
{
    class ForumTests
    {

        [TestMethod]
        public void memberTests()
        {
            // arrange
            User member = new User(0,false, "edan", "userName", "email@email.com", "123");
            Forum forum = new Forum(0, "stackOverFlow", "description");
            User[] members;
            User[] membersAfterDelete;
            int fakeId = 100;
            bool succDelete;

            // act
      
            forum.AddMember(member);
            forum.GetMember(member.Id);
            members = forum.GetMembers();
            forum.RemoveMember(member.Id);
            membersAfterDelete = forum.GetMembers();
            succDelete = forum.RemoveMember(fakeId);
            
            // assert
            Assert.AreEqual(member, forum.GetMember(member.Id), "checking if member added successfully");
            Assert.AreEqual(1, members.Length, "checking if member added successfully");
            Assert.AreEqual(member, members[0], "checking if member added successfully");
            Assert.AreEqual(0, membersAfterDelete.Length, "checking if member removed successfully");
            Assert.AreEqual(null,forum.GetMember(member.Id), "checking if member removed successfully");
            Assert.AreEqual(null, forum.GetMember(member.Id), "checking if member removed successfully");
            Assert.AreEqual(false, succDelete, "Checking if it is possible to remove a non-existent user.");




        }
    }
}
