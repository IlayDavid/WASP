using System;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

namespace WASP.Server
{
    public class NotificationServer
    {
        public static void Run()
        {
            const string baseUrl = "http://localhost:5000/";

            using (WebApp.Start<Startup1>(url: baseUrl))
            {
                Console.WriteLine("Press Enter to quit.");
                Console.ReadKey();
            }
        }

        public static void GroupNotify(string group, string message)
        {
            var context = GlobalHost.ConnectionManager.GetConnectionContext<NotificationConnection>();
            context.Groups.Send(group, message);
        }

        public static string GetGroup(string hash)
        {
            Service.LoginPair pair = Service.ServiceFacade.GetPair(hash);
            return GetGroup(pair.UserId, pair.ForumId);
        }

        public static string GetGroup(int uId, int fId)
        {
            return String.Format("forum: {0}|id: {1}", fId, uId);
        }
    }
}