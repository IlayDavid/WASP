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
        private DAL2 dal = new DALSQL();
        [TestMethod]
        public void memberTests()
        {
            // arrange
            Forum forum = new Forum(0,"name", "desc",null,null);
            User member = new User(0,"edan", "userName", "email@email.com", "123", forum,dal);
            forum.AddMember(member);
            int numbOfMembers, numOfMembersAfterDelete;
            User fake = new User(1,"user", "name", "email", "pass", null,dal);
            bool succDelete;
            // act
            forum.AddMember(member);
            numbOfMembers = forum.GetMembers().Length;
            // assert
            Assert.AreEqual(1, numbOfMembers, "checking if member added successfully");
            Assert.AreEqual(member, forum.GetMember(member.Id), "checking if member added successfully");
            // act
            forum.RemoveMember(member);
            numOfMembersAfterDelete = forum.GetMembers().Length;
            succDelete = forum.RemoveMember(fake);
            // assert
            Assert.AreEqual(0, numOfMembersAfterDelete, "checking if member removed successfully");
            Assert.AreEqual(false, succDelete, "Checking if it is possible to remove a non-existent user.");
            //act
            forum.AddMember(fake);
            succDelete = forum.RemoveMember(fake);
            Assert.AreEqual(true, succDelete, "checking if fake was delted after inserting him to forum.");
            
        }
        [TestMethod]
        public void adminTest()
        {
            // arrange
            Forum forum = new Forum(0,"stackOverFlow", "description",null,null);
            User user = new User(0, "edanAdmin", "admin", "email", "!23123", forum,dal);
            Admin admin = new Admin(user, forum, null);
            bool isAdmin;
            // act
            forum.AddAdmin(admin);
            isAdmin = forum.IsAdmin(admin.Id);
            // assert
            Assert.AreEqual(true, isAdmin, "checking if admin added successfully");
            Assert.AreEqual(1, forum.GetAdmins().Length, "check if addmin was added");
            // remove admin
            forum.RemoveAdmin(admin);
            Assert.AreEqual(0, forum.GetAdmins().Length, "check if addmin was deleted");
        }
        [TestMethod]
        public void subForumTest()
        {
            // arrange
            Forum forum = new Forum(0,"stackOverFlow", "description", null, null);
            User user = new User(0, "edanAdmin", "admin", "email", "!23123", forum,dal);
            forum.AddMember(user);
            Subforum sf = new Subforum(0,"subForum", "someDescription",forum, null);
            bool isSf = false;
            // act
            forum.AddSubForum(sf);
            isSf = forum.IsSubForum(sf.Id);
            // assert
            Assert.AreEqual(true, isSf, "checking if subForum added successfully");
            Assert.AreEqual(1, forum.GetSubForum().Length, "check if subforum was added");
        }
    }
}
