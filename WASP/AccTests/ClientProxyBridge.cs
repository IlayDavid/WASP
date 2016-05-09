using System;
using System.Collections.Generic;
using Client.DataClasses;
using WASP.DataClasses;
using Forum = Client.DataClasses.Forum;
using Notifications = Client.DataClasses.Notifications;
using Policy = Client.DataClasses.Policy;
using Post = Client.DataClasses.Post;
using Subforum = Client.DataClasses.Subforum;
using SuperUser = Client.DataClasses.SuperUser;
using User = Client.DataClasses.User;

namespace AccTests
{
    public class ClientProxyBridge : WASPClientBridge
    {
        private ClientRealBridge proj;

        public ClientProxyBridge(ClientRealBridge bridge)
        {
            proj = bridge;
        }
        
    }
}
