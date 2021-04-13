using CluelessNetwork.BackendNetworkInterfaces;
using CluelessBackend.Core;
using System;
using System.Collections.Generic;

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


            // Start game
            GameManager gameManager = new GameManager();
            gameManager.StartGame();

            // Init players
            List<Player> players = new List<Player>(4);

            players.Add(new Player(Suspect.SUSPECT.MISS_SCARLET));
            players.Add(new Player(Suspect.SUSPECT.MR_GREEN));
            players.Add(new Player(Suspect.SUSPECT.COLONEL_MUSTARD));
            players.Add(new Player(Suspect.SUSPECT.MRS_WHITE));

            // Set players to the board
           gameManager.GetBoard().SetPlayers(players);
          

            gameManager.SpreadCardsToPlayer(players);
            gameManager.CreateUniqueListOfRandomNum(0, 3);
            gameManager.AssignWeaponToRooms();
            gameManager.GetBoard().SePlayerstStartingPosition(players);
  

            // Start network server. Runs until the program is interrupted or terminated
            // TODO: Create a class implementing IGameInstanceService and assign it
            var gameInstanceService = new GameInstanceService();
            var unused = new ChatService(gameInstanceService);
            using var networkServer = new CluelessNetworkServer(gameInstanceService);
            while (true) networkServer.ListenForConnection(listenContinuously: true);

            // Disable warning from static code analysis. We don't expect this function to ever return.
            // ReSharper disable once FunctionNeverReturns
        }
    }
}