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

        // Player movements
        bool hasMoved_;

        // Player status
        bool isActive_;

        // Player Suggestions
        int numberOfSuggestions_;
        bool hasSuggested_;
        bool hasAccused_;


        public Player(int PlayerID, string playerName)
        {
            playerID_ = PlayerID;
            playerName_ = playerName;
        }

        public void MakeSuggestion()
        {

        }

        public void MakeAccusation()
        {

        }
    }
}
