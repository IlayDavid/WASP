using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Domain;

namespace WASP.Service
{
    public static class ServiceFacade
    {
        private static IBL bl;
        private static Dictionary<string, int> loggedIn;
        private static int initialized = 0;
        public static string Initialize(Dictionary<string, dynamic> data)
        {
            string result = "";
            //SuperUser initialize(string name, string userName, int ID, string email, string pass);
            ServiceFacade.bl = new BLFacade();
            ServiceFacade.loggedIn = new Dictionary<string, int>();
            ServiceFacade.initialized = 1;
            return result;
        }

        public static string isInitialize()
        {
            return ServiceFacade.initialized.ToString();
        }
        

    }
}
