using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WASP.Server
{
    class NotificationConnection : PersistentConnection
    { 
        private static Dictionary<string, string> connectionToHash = new Dictionary<string, string>();
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            string hash;
            connectionToHash.TryGetValue(connectionId, out hash);
            if (hash == null)
                hash = "not logged in";
            Groups.Add(connectionId, hash);
            return Connection.Send(connectionId, hash);
        }
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            if (data.ToLower().Equals("logout"))
            {
                string hash;
                connectionToHash.TryGetValue(connectionId, out hash);
                if (hash != null)
                {
                    connectionToHash.Remove(connectionId);
                    Groups.Remove(NotificationServer.GetGroup(hash), hash);
                    return Connection.Send(connectionId, "Logged out");
                }
            }

            if (!Service.ServiceFacade.isLoggedIn(data))
            {
                return Connection.Send(connectionId, "User " + data + " not logged in!");
            }
            connectionToHash.Add(connectionId, data);
            Groups.Add(connectionId, NotificationServer.GetGroup(data));
            return Connection.Send(connectionId, "Logged in!");
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            string hash;
            connectionToHash.TryGetValue(connectionId, out hash);
            if (hash != null)
            {
                return Groups.Remove(connectionId, hash);
            }
            return Connection.Send(connectionId, "Connection " + connectionId + " disconncted");
        }
    }
}
