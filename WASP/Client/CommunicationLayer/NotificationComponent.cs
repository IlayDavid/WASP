using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace Client.CommunicationLayer
{
    class NotificationComponent
    {
        private static Connection connection;
        public static void DoSomething(string data, Thread n)
        {
            if(!data.Contains("ntf")) // Not a notification!
                Console.WriteLine(data);
            else
            {
                n.Start();
            }
        }


        public static void Initialize(Object cl)
        {
            CL thiscl = (CL)cl;
            Initialize(thiscl._auth, thiscl.notif);
        }
        public static void Initialize(string loginHash, Thread n)
        {
            // Connect to the service
            connection = new Connection("http://localhost:5000/signalr");

            // Print the message when it comes in
            connection.Received += data => DoSomething(data, n);

            // Start the connection
            connection.Start().Wait();


            connection.Send(loginHash).Wait();
        }
    }
}
