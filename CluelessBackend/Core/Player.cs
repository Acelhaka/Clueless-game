using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Player
    {
        int playerID_;
        string playerName_ = "";

        // Player can only be in one cell at a time (room/hallway)
        // Player position marked as a 2-d array of size 1x1
        int[,] playerCurrentPosition_ = new int[1, 1];

        // Stores the cards that player is holding
        Card[] cardsInHand_;

        // Player movements
        bool hasMoved_;

        // Player status
        bool isActive_;

        // Player Suggestions
        int numberOfSuggestions_ = 0;
        bool hasSuggested_ = false;
        bool hasAccused_ = false;


        public Player(int PlayerID, string playerName)
        {
            playerID_ = PlayerID;
            playerName_ = playerName;
        }

        public void MakeSuggestion()
        {
            Console.WriteLine("Player {playerName_} has made a suggestion");

            hasSuggested_ = true;
            numberOfSuggestions_ = 1;
        }

        public void MakeAccusation()
        {
            Console.WriteLine("Player {playerName_} has made an accusation");

            hasAccused_ = true;
            numberOfSuggestions_ = 1;
        }

        public Card[] GetPlayersCards()
        {
            return cardsInHand_;
        }
        public void SetPlayersCards(Card[] cardsInHand)
        {
            cardsInHand_ = cardsInHand;
        }

        // Add one card to the players hand
        public void HandOneCard(int i, Card card)
        {
            cardsInHand_[i] = card;
        }
    }
}
