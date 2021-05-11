using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessNetwork.TransmittedTypes;

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
        List<Card> cardsInHand_ = new List<Card>();

        // Player movements
        bool hasMoved_;

        // Player status
        bool isActive_ = true;

        // Player Suggestions
        int numberOfSuggestions_ = 0;
        bool hasSuggested_ = false;
        bool hasAccused_ = false;

        public List<Card> GetPlayersCards()
        {
            return cardsInHand_;
        }
        public void SetPlayersCards(List<Card> cardsInHand)
        {
            cardsInHand_ = cardsInHand;
        }

        /// <summary>
        /// Hand one card to the players when spreading cards 
        /// </summary> 
        /// <param name="card"> Card that is handed to the player </param>
        public void HandOneCard(Card card)
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

        /// <summary>
        /// Updating the player current position, row and col in the board
        /// </summary>
        /// <param name="row"> Row in the board that is moving at, 0-4</param>
        /// <param name="col"> Column  in the board that is moving at, 0-4</param>
        public void SetPlayerPosition(int row, int col)
        {
            playerCurrentPositionRow_ = row;
            playerCurrentPositionCol_ = col;
        }
      
        public bool IsActive()
        {
            return isActive_;
        }

        public void SetIsActive(bool isActive)
        {
            isActive_ = isActive;
        }

        public bool HasActions()
        {
            return !hasMoved_ || !hasSuggested_ || !hasAccused_;

        }

        public void HasSuggested()
        {
            hasSuggested_ = true;
            numberOfSuggestions_ += 1;
        }

        public void HasAccused()
        {
            if(hasAccused_ == true)
            {
                Console.WriteLine("Player {playerName_} cannot make accusations");
                return;
            }
            hasAccused_ = true;
        }
    }
}
