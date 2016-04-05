using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class ForumProperties
    {
        static public bool Permission=true;
        public User currUser=new User();
        private List<Func<bool>> _generalPolicies=new List<Func<bool>>();
        private List<Func<User, bool>> _userPolicies=new List<Func<User,bool>>();
        /// <summary>
        /// checks all of the general policies that do not require a parameter.
        /// for example: is there an admin?
        /// </summary>
        /// <returns>true if all policies return true</returns>
        public bool CheckGeneralPolicies()
        {
            return _generalPolicies.All(func => func() != false);
        }
        /// <summary>
        /// checks if all policies that affect users pass.
        /// for example, is the user's password contains a number?
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if all policies return true</returns>
        public bool CheckUserPolicies(User user)
        {
            return _userPolicies.All(func => func(user) != false);
        }


        /// <summary>
        /// Default constructor. called by CreateForumProperties
        /// </summary>
        private ForumProperties()
        {
            
        }
        /// <summary>
        /// Checks if the caller has permission to create a forum, and returns a new ForumProperty on success.
        /// </summary>
        /// <returns>new ForumProperties</returns>
        static public ForumProperties CreateForumProperties()
        {
            if (HasPermission())
            {
                return new ForumProperties();
            }
            return null;
        }

        /// <summary>
        /// checks if the user has permission to generate or update a new ForumProperties
        /// </summary>
        /// <returns>true if the user may generate or update the Forum's properties</returns>
        static public bool HasPermission()
        {
            return Permission;
         
        }
        /// <summary>
        /// adds the test to check if all sub forums atleast n moderators
        /// runs the GeneralPolicy checks to make sure they are still updates
        /// </summary>
        /// <param name="num"></param>
        public void AddPolicyMinNumberOfModerators(int n)
        {
            HasPermission();
            _generalPolicies.Add(()=>testPolicyMinNumberOfModerators(n));
            CheckGeneralPolicies();
        }
        /// <summary>
        /// tests wether all sub forums has a minimum of n moderators
        /// </summary>
        /// <returns>true if all sub forums has at-least n moderators</returns>
        private bool testPolicyMinNumberOfModerators(int n)
        {
            return currUser.NumberOfModerators >= n;
        }
        /// <summary>
        /// adds the test to check if the user has a number in his password
        /// </summary>
        public void AddPolicyPasswordRequiresNumber()
        {
            HasPermission();
            _userPolicies.Add(testPolicyPasswordRequiresNumber);
        }
        /// <summary>
        /// tests wether a user has a number in his password
        /// </summary>
        /// <returns>true the user has a number in his password</returns>
        private bool testPolicyPasswordRequiresNumber(User user)
        {
            return user.Pass.Any((x) => x >= '0' && x <= '9');
        }


    }
}
