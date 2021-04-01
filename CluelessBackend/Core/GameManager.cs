using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class GameManager : Board
    {
        public void InitWeapons()
        {

        }

        public void AssignWeaponToRooms()
        {

        }

        public void SetStartingPosition()
        {

        }

        public void StartGame()
        {

        }

        public void MovePlayerToRoom(Player player, Room room)
        {
            room.SetPlayerInRoom(player);
        }

        public void SpreadCardsToPlayer(List<Player> players, CardDeck deck)
        {
            int cardsCount = 0;

            while (cardsCount < deck.GetDeckSize())
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].HandOneCard(cardsCount, deck.GetCardFromDeck(cardsCount));
                    cardsCount++;
                }
            }
        }

        public void SetStartingPosition(int numberOfPlayers, List<Player> players)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                // TODO: finish for the rest of the suspects
                if (players[i].GetSuspectType() == Suspect.SUSPECT.MISS_SCARLET)
                {
                    // TODO maybe this should set their starting Position as well in the Player object?
                    MovePlayerToRoom(players[i], GetRoomByIndex(0, 3));
                    Console.WriteLine("MISS_SCARLET - Starting position in cell [0,3], Hallway-2");
                }
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.MR_GREEN)
                {
                    MovePlayerToRoom(players[i], GetRoomByIndex(4, 1));
                    Console.WriteLine("MR_GREEN - Starting position in cell [4,1], Hallway-11");
                }
            }
        }
    }
}
