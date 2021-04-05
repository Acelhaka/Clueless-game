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

        /// <summary>
        /// A player makes a suggestion
        /// </summary>
        /// <param name="player"> Player that makes the suggestion </param>
        /// <param name="proposedCards"> Proposed cards for the suggestion </param>
        public void MakeSuggestion(Player player, List<Card> proposedCards)
        {
            Console.WriteLine("Player {playerName_} has made a suggestion");

            player.HasSuggested(); 
              
        }

        /// <summary>
        /// Player makes an accusation
        /// </summary>
        /// <param name="player"> player that makes the accusation </param>
        /// <param name="proposedCards">Proposed card for the accusation </param>
        public void MakeAccusation(Player player, List<Card> proposedCards)
        {
            Console.WriteLine("Player {playerName_} has made an accusation");
            player.HasAccused();
        }

        /// <summary>
        /// Move Player to room will move the player to the coordinates of the roomType
        /// Player'c current position will also be updated to the coordinates of the room 
        /// </summary>
        /// <param name="player"> Player that will be moved</param>
        /// <param name="roomType"> Room type that the player requested to move at</param>
        public void MovePlayerToRoom(Player player, Room.ROOM roomType)
        {
            if (roomType == Room.ROOM.STUDY)
            {
                rooms_[0, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 0);
            }
            else if (roomType == Room.ROOM.HALL)
            {
                rooms_[0, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 2);
            }
            else if (roomType == Room.ROOM.LOUNGE)
            {
                rooms_[0, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(0, 4);
            }
            else if (roomType == Room.ROOM.LIBRARY)
            {
                rooms_[2, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 0);
            }
            else if (roomType == Room.ROOM.BILLIARD_ROOM)
            {
                rooms_[2, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 2);
            }
            else if (roomType == Room.ROOM.DINNING_ROOM)
            {
                rooms_[2, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(2, 4);
            }
            else if (roomType == Room.ROOM.CONSERVATORY)
            {
                rooms_[4, 0].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 0);
            }
            else if (roomType == Room.ROOM.BALLROOM)
            {
                rooms_[4, 2].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 2);
            }
            else if (roomType == Room.ROOM.KITCHEN)
            {
                rooms_[4, 4].SetPlayerInRoom(player);
                player.SetPlayerPosition(4, 4);
            }
        }

        /// <summary>
        /// Move player to a room or hallway given the next cell coordinates
        /// </summary>
        /// <param name="player"> player that will be moved </param>
        /// <param name="row"> The row that the player will be positioned on the board </param>
        /// <param name="column"> The column that the player will be positioned on the board </param>
        public void MovePlayerToRoom(Player player, int row, int column)
        {
            rooms_[row, column].SetPlayerInRoom(player);
            player.SetPlayerPosition(row, column);
        }
    }
}
