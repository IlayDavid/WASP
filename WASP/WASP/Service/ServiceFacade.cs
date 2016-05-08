using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Domain;
using WASP.DataClasses;
using System.Web.Script.Serialization;

namespace WASP.Service
{
    public static class ServiceFacade
    {
        private static IBL bl = null;
        private static Dictionary<string, LoginPair> loggedIn = null;
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        private static string GenerateRandomHash()
        {
            return Guid.NewGuid().ToString();
        }
        public static string initialize(Dictionary<string, dynamic> data)
        {
            //SuperUser initialize(string name, string userName, int ID, string email, string pass);
            ServiceFacade.bl = new BLFacade();
            SuperUser su = ServiceFacade.bl.initialize(data["name"], data["username"], data["id"], data["email"], data["password"]);
            ServiceFacade.loggedIn = new Dictionary<string, LoginPair>();
            string key = GenerateRandomHash();
            ServiceFacade.loggedIn.Add(key, new LoginPair(su.Id));
            
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("auth", key);
            result.Add("id", su.Id);
            result.Add("username", su.Username);
            result.Add("password", su.Password);
            return jss.Serialize(result);
        }

        public static string isInitialize(Dictionary<string, dynamic> data)
        {
            int initialized = 1;
            if (ServiceFacade.bl == null || ServiceFacade.loggedIn == null || ServiceFacade.bl.isInitialize() == 0)
                initialized = 0;
            return initialized.ToString();
        }

        public static string createForum(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Policy policy = new Policy();
            Forum forum = bl.createForum(pair.UserId, data["forumname"], data["description"], data["adminid"], data["adminusername"], data["adminname"], data["email"], data["password"], policy);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("title", forum.Name);
            result.Add("description", forum.Description);
            result.Add("adminid", forum.GetAdmins()[0].Id);
            return jss.Serialize(result);
        }

        public static string defineForumPolicy(Dictionary<string, dynamic> data)
        {
            LoginPair pair = loggedIn[data["auth"]];
            Policy policy = new Policy();
            bl.defineForumPolicy(pair.UserId, pair.ForumId, policy);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
    
            return jss.Serialize(result);
        }


    }
}
