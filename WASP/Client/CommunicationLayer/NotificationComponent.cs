using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
namespace Client.CommunicationLayer
{
    class NotificationComponent
    {
        private static Connection connection;
        public static void DoSomething(string data)
        {
            if(!data.Contains("ntf")) // Not a notification!
                Console.WriteLine(data);
            else
            {
                // This is a notification. Your code here!
            }
        }



        public static void Initialize(string loginHash)
        {
            // Connect to the service
            connection = new Connection("http://localhost:5000/signalr");

            // Print the message when it comes in
            connection.Received += data => DoSomething(data);

            // Start the connection
            connection.Start().Wait();


            connection.Send(loginHash).Wait();
        }
    }
}
