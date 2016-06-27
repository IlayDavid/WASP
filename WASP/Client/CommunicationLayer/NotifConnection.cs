using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    class NotifConnection
    {
        public string loginHash;
        public bool stop;
        public CL _cl;

        public NotifConnection(string hash, CL cl)
        {
            _cl = cl;
            loginHash = hash;
            stop = false;

        }

        public void Run()
        {
            NotificationComponent.Initialize(loginHash, _cl);
        }

    }
}
