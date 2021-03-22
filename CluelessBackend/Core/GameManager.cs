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
        public void SpreadCardsToPlayer(int numberOfPlayers, Player[] player, CardDeck deck)
        {
            int cardsCount = 0;

            while (cardsCount <= deck.GetDeckSize())
            {
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    player[i].HandOneCard(cardsCount, deck.GetCardFromDeck(cardsCount));
                    cardsCount++;
                }
            }
        }

        public void SetStartingPosition(int numberOfPlayers, Player[] player)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                // TODO: finish for the rest of the suspects
                if (player[i].GetSuspectType() == Suspect.SUSPECT.MISS_SCARLET)
                {
                    MovePlayerToRoom(player[i], GetRoomByIndex(0, 3));
                }
            }
        }
    }
}
