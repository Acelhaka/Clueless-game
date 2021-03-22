using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class GameManager
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

        public void MovePlayer()
        {

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
    }
}
