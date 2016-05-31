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
        public static void DoSomething(string data)
        {
            Console.WriteLine(data);

        }

        public static void Mainn()
        {
            // Connect to the service
            var connection = new Connection("http://localhost:5000/signalr");

            // Print the message when it comes in
            connection.Received += data => DoSomething(data);

            // Start the connection
            connection.Start().Wait();

            string line = null;
            while ((line = Console.ReadLine()) != null)
            {
                // Send a message to the server
                connection.Send(line).Wait();
            }
        }
    }
}
