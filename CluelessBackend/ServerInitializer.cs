using CluelessNetwork.BackendNetworkInterfaces;
using CluelessBackend.Core;
using System;

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

            //-----------------------------------------------------
            // Run any backend initialization logic here
            //-----------------------------------------------------

            // TODO:: Print statements will be removed, using to showcase the working backend logic

            // Create deck of cards
            CardDeck deck = new CardDeck();
            Console.WriteLine("Creating deck of cards...");
            Console.WriteLine(" - 6 weapons - 6 suspects - 9 rooms - ");
            deck.CreateDeckOfCards();
            deck.PrintDeckOfCards();
            
            // Init scenario file
            ScenarioFile scenarioFile = new ScenarioFile();

            // Place 3 random cards in the secret envelope
            scenarioFile.SetEnvelopeCards(deck.SelectCardsForEnvelope());
            scenarioFile.PrintEnvelopeCards();

            Console.WriteLine("\nUpdated deck after selecting 3 cards for the envelope..");
            deck.PrintDeckOfCards();


            Console.WriteLine("\nShuffling the cards before handing over to the players..");
            deck.ShuffleCards();
            deck.PrintDeckOfCards();
           


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