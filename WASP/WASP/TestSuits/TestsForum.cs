using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.TestSuits
{
    [TestClass]
    public class TestsForum
    {
        [TestMethod]
        public void memberTests()
        {
            // arrange
            Forum forum = new Forum("name", "desc", new PasswordPolicy());
            Member member = new Member("edan", "userName", "email@email.com", "123", forum);
            int numbOfMembers, numOfMembersAfterDelete;
            Member fake = new Member("user", "name", "email", "pass", null);
            bool succDelete;
            // act
            forum.AddMember(member);
            numbOfMembers = forum.GetMembers().Count;
            // assert
            Assert.AreEqual(1, numbOfMembers, "checking if member added successfully");
            Assert.AreEqual(member, forum.GetMember(member.UserName), "checking if member added successfully");
            // act
            forum.RemoveMember(member);
            numOfMembersAfterDelete = forum.GetMembers().Count;
            succDelete = forum.RemoveMember(fake);
            // assert
            Assert.AreEqual(0, numOfMembersAfterDelete, "checking if member removed successfully");
            Assert.AreEqual(false, succDelete, "Checking if it is possible to remove a non-existent user.");
        }
        [TestMethod]
        public void adminTest()
        {
            // arrange
            Forum forum = new Forum("stackOverFlow", "description", new PasswordPolicy());
            Member admin = new Member("edan", "userName", "email@email.com", "123", forum);
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
            Forum forum = new Forum("stackOverFlow", "description", new PasswordPolicy());
            Member author = new Member("edan", "habler", "mail@mail.com", "123", forum);
            Subforum sf = new Subforum("subForum", "someDescription", author, DateTime.Now);
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
