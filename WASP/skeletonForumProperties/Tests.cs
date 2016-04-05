using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApplication4
{
    [TestClass]
    public class Tests
    {
        //cases that should work propperly
        [TestMethod]
        public void CheckCreation()
        {
            //creates a basic forumproperties
            var properties = ForumProperties.CreateForumProperties();
            ForumProperties.Permission = true;
            Assert.IsTrue(properties != null,"failed to creae a ForumProperties");
        }

        [TestMethod]
        public void CheckPermission()
        {
            //checks if the user has permission to create the forum
            ForumProperties.Permission = true;
            Assert.IsTrue(ForumProperties.HasPermission(),"no permission to create and update forums");
        }

        [TestMethod]
        public void CheckGeneralDefaultProperties()
        {
            //checks if the default properties 
            var defProperties=ForumProperties.CreateForumProperties();
            Assert.IsTrue(defProperties.CheckGeneralPolicies(), "default policy fails on default properties");
        }

        [TestMethod]
        public void CheckUserDefaultProperties()
        {
            //checks if the default user policies works on a default user
            var properties = ForumProperties.CreateForumProperties();
            Assert.IsTrue(properties.CheckUserPolicies(properties.currUser),"default user fail the default policy for users");
        }

        [TestMethod]
        public void TestNumModsPolicy()
        {
            //checks if adding the policy minimum number of mods and running it works correctly
            var properties = ForumProperties.CreateForumProperties();
            ForumProperties.Permission = true;
            properties.currUser.NumberOfModerators = 5;
            properties.AddPolicyMinNumberOfModerators(5);
            Assert.IsTrue(properties.CheckGeneralPolicies(),"failed to add and test the policy: minimum number of moderators");
        }

        [TestMethod]
        public void TestNumberInPassword()
        {
            //checks if adding the policy password requires a number and running it works correctly
            var properties = ForumProperties.CreateForumProperties();
            ForumProperties.Permission = true;
            properties.currUser.Pass= "ariel1";
            properties.AddPolicyPasswordRequiresNumber();
            Assert.IsTrue(properties.CheckUserPolicies(properties.currUser), "failed to add and test the policy: password requires a number");
        }

        //cases that should fail
        [TestMethod]
        public void CheckCreationF()
        {
            //creates a basic forumproperties
            ForumProperties.Permission = false;
            var properties = ForumProperties.CreateForumProperties();
            Assert.IsFalse(properties != null, "should fail to create a forumProperties");
        }

        [TestMethod]
        public void CheckPermissionF()
        {
            //checks if the user has permission to create the forum
            ForumProperties.Permission = false;
            Assert.IsFalse(ForumProperties.HasPermission(), "should not have permission to create and update forums");
        }


        [TestMethod]
        public void TestNumModsPolicyF()
        {
            //checks if adding the policy minimum number of mods and running it works correctly
            ForumProperties.Permission = true;
            var properties = ForumProperties.CreateForumProperties();
            properties.currUser.NumberOfModerators = 5;
            properties.AddPolicyMinNumberOfModerators(10);
            Assert.IsFalse(properties.CheckGeneralPolicies(), "should fail to add and test the policy: minimum number of moderators");
        }

        [TestMethod]
        public void TestNumberInPasswordF()
        {
            //checks if adding the policy password requires a number and running it works correctly
            var properties = ForumProperties.CreateForumProperties();
            ForumProperties.Permission = true;
            properties.currUser.Pass = "pass";
            properties.AddPolicyPasswordRequiresNumber();
            Assert.IsFalse(properties.CheckUserPolicies(properties.currUser), "should fail to add and test the policy: password requires a number");
        }

    }
}
