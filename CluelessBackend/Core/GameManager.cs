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

        /// <summary>
        /// Function to move the player into a room
        /// </summary>
        /// <param name="player">Player that moves to a another room </param>
        /// <param name="room"> Room that the player should move </param>
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
                    // if cardsCount = deckSize_ all cards have been spread out to the players
                    if (cardsCount == deck.GetDeckSize())
                    {
                        break;
                    }

                    players[i].HandOneCard(deck.GetCardFromDeck(cardsCount));
                    cardsCount++;
                }
            }
        }

    }
}
