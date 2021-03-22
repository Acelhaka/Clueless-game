using CluelessNetwork.BackendNetworkInterfaces;
using CluelessBackend.Core;

namespace CluelessBackend
{
    public static class ServerInitializer
    {
        // ReSharper disable once UnusedParameter.Global
        public static void Main(string[] args)
        {
            // TODO: Allow args to specify port number? (not for skeletal)
            // TODO: Load server configuration file (not for skeletal)
            // TODO: Start logging (not for skeletal)

            // Run any backend initialization logic here
            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();
            deck.ShuffleCards();

            // Start network server. Runs until the program is interrupted or terminated
            // TODO: Create a class implementing IGameInstanceService and assign it
            IGameInstanceService gameInstanceService = null!;
            using var networkServer = new CluelessNetworkServer(gameInstanceService);
            while (true) networkServer.ListenForConnection();

            // Disable warning from static code analysis. We don't expect this function to ever return.
            // ReSharper disable once FunctionNeverReturns
        }
    }
}