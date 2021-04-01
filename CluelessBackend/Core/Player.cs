using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Player : Suspect
    {
        int playerID_;
        string playerName_ = "";


        public Player(SUSPECT suspectType)
        {
            SetSuspectType(suspectType);
        }

        // Player starting position is in one of the hallways
        int startingPositionRow_;
        int startingPositionCol_;

        // Player can only be in one cell at a time (room/hallway)
        int playerCurrentPositionRow_;
        int playerCurrentPositionCol_;

        // Stores the cards that player is holding
        List<Card> cardsInHand_;

        // Player movements
        bool hasMoved_;

        // Player status
        bool isActive_;

        // Player Suggestions
        int numberOfSuggestions_ = 0;
        bool hasSuggested_ = false;
        bool hasAccused_ = false;

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

        public List<Card> GetPlayersCards()
        {
            return cardsInHand_;
        }
        public void SetPlayersCards(List<Card> cardsInHand)
        {
            cardsInHand_ = cardsInHand;
        }

        // Add one card to the players hand
        public void HandOneCard(int i, Card card)
        {
            cardsInHand_.Add(card);
        }

        public void SetPlayerStartingPosition(int row, int col)
        {
            startingPositionRow_ = row;
            startingPositionCol_ = col;

            playerCurrentPositionRow_ = row;
            playerCurrentPositionCol_ = col;
        }

        public int GetPlayerPositionRow()
        {
            return playerCurrentPositionRow_;   
        }

        public int GetPlayerPositionCol()
        {
            return playerCurrentPositionRow_;
        }

        public void SetPlayerPosition(int row, int col)
        {
            playerCurrentPositionRow_ = row;
            playerCurrentPositionCol_ = col;
        }
   

        //public SUSPECT GetSuspectType()
        //{
        //    return suspectType_;
        //}

        //public void SetSuspectType(SUSPECT suspectType)
        //{
        //    suspectType_ = suspectType;
        //}
    }
}
