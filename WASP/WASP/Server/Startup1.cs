using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WASP.Server.Startup1))]

namespace WASP.Server
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR<NotificationConnection>("/signalr");
        }
    }
}
