using System;
using System.Threading.Tasks;
using CluelessNetwork.FrontendNetworkInterfaces;
using CluelessNetwork.TransmittedTypes;

namespace CluelessFrontend
{
    public class ClientInitializer
    {
        // ReSharper disable once UnusedParameter.Local
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a hostname/IP address to connect to:");
            var hostname = Console.ReadLine();
            Console.WriteLine("Choose a name:");
            var name = Console.ReadLine();
            Console.WriteLine("Connect as host? [true/false]");
            var isHost = bool.Parse(Console.ReadLine()!);
            using var client = new CluelessNetworkClient(hostname!, isHost, name!);

            // Just for demonstration purposes
            StartChatting(client);

            // TODO: Use client in playing the game
        }

        private static void StartChatting(CluelessNetworkClient client)
        {
            // Listen for and print messages when they arrive
            client.ChatMessageReceived += message => { Console.WriteLine($"{message.SenderName}: {message.Content}"); };
            Task.Run(client.ListenForUpdatesContinuously);

            Console.WriteLine("Write chat messages, or \"quit\" to quit");

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "quit")
                    break;

                client.SendChatMessage(new ChatMessage {Content = message ?? string.Empty});
            }
        }
    }
}