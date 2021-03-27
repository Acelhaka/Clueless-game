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

        SUSPECT suspectType_;

        public Player(SUSPECT suspectType)
        {
            suspectType_ = suspectType;
        }

        // Player starting position is in one of the hallways
        int[,] startingPosition_ = new int[1,1];

        // Player can only be in one cell at a time (room/hallway)
        // Player position marked as a 2-d array of size 1x1
        int[,] playerCurrentPosition_ = new int[1, 1];

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

        public SUSPECT GetSuspectType()
        {
            return suspectType_;
        }

        public void SetSuspectType(SUSPECT suspectType)
        {
            suspectType_ = suspectType;
        }

        public bool IsActive()
        {
            return isActive_;
        }

        public void SetIsActive(bool isActive)
        {
            isActive_ = isActive;
        }
    }
}
