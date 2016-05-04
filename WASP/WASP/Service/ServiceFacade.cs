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
        private static Dictionary<string, int> loggedIn = null;

        private static string GenerateRandomHash()
        {
            return Guid.NewGuid().ToString();
        }
        public static string Initialize(Dictionary<string, dynamic> data)
        {
            //SuperUser initialize(string name, string userName, int ID, string email, string pass);
            ServiceFacade.bl = new BLFacade();
            SuperUser su = ServiceFacade.bl.initialize(data["name"], data["username"], data["id"], data["email"], data["pass"]);
            ServiceFacade.loggedIn = new Dictionary<string, int>();
            string key = GenerateRandomHash();
            ServiceFacade.loggedIn.Add(key, su.Id);
            var jss = new JavaScriptSerializer();
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("auth", key);
            result.Add("id", su.Id);
            result.Add("username", su.Username);
            result.Add("pass", su.Password);
            return jss.Serialize(result);
        }

        public static string isInitialize()
        {
            int initialized = 1;
            if (ServiceFacade.bl == null || ServiceFacade.loggedIn == null || ServiceFacade.bl.isInitialize() == 0)
                initialized = 0;
            return initialized.ToString();
        }
        

    }
}
