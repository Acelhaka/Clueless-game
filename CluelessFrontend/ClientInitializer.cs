using System;
using CluelessNetwork.FrontendNetworkInterfaces;

namespace CluelessFrontend
{
    public class ClientInitializer
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter a hostname/IP address to connect to:");
            var hostname = Console.ReadLine();
            Console.WriteLine("Connect as host? [true/false]");
            var isHost = bool.Parse(Console.ReadLine()!);
            using var client = new CluelessNetworkClient(hostname!, isHost);

            // TODO: Use client in playing the game
        }
    }
}