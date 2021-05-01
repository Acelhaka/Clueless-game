using CluelessNetwork.BackendNetworkInterfaces;
using CluelessBackend.Core;
using System;
using System.Collections.Generic;

namespace CluelessBackend
{
    public static class BackendInitializer
    {
        public static void InitializeBackend()
        {
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
        }
    }
}