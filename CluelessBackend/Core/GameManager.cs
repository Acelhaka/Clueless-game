using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class GameManager : Board
    {
        public void InitWeapons()
        {

        }

        public void AssignWeaponToRooms()
        {

        }

        public void StartGame()
        {

        }

        public void MovePlayerToRoom(Player player, Room room)
        {
            room.SetPlayerInRoom(player);
        }

        /// <summary>
        /// Hands the cards to the players in the game
        /// </summary>
        /// <param name="players"> All players in the game </param>
        /// <param name="deck"> Deck of cards that will be handed out to the players </param>
        public void SpreadCardsToPlayer(List<Player> players, CardDeck deck)
        {
            int cardsCount = 0;

            while (cardsCount < deck.GetDeckSize())
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    // TODO in this inner loop, you'll need to make sure to break if cardsCount == deck.GetDeckSize()
                    // otherwise you'll run into an array out of bounds error for cases where one player gets more cards than the other one
                    // i.e when just 4 players are in the game, two of the players will get 5 cards, and the other players will get 4 cards
                    players[i].HandOneCard(cardsCount, deck.GetCardFromDeck(cardsCount));
                    cardsCount++;
                }
            }
        }

    }
}
