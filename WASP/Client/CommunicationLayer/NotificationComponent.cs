using System;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;
using Client.GUI;

namespace Client.CommunicationLayer
{
    class NotificationComponent
    {
        private static Connection connection;
        private static CL _cl;


        public static void DoSomething(string data)
        {
            if(!data.Contains("ntf")) // Not a notification!
                Console.WriteLine(data);
            else
            {
                Session.AddNewNotifications();
            }
        }


        public static void Initialize(string loginHash, CL cl)
        {
            _cl = cl;

            // Connect to the service
            connection = new Connection("http://localhost:5000/signalr");

            // Print the message when it comes in
            connection.Received += data => DoSomething(data);

            // Start the connection
            connection.Start().Wait();

            connection.Send(loginHash).Wait();
        }

        public static void close()
        {
            connection.Send("logout").Wait();
            //connection.Stop();
        }
    }
}
