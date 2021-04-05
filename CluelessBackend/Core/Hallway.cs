using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Hallway : Room   // Child of Room class
    {
        /// <summary>
        /// ID of each hallway in the board
        /// </summary>
        int hallwayID_;

        /// <summary>
        /// Player that is located in the hallway
        /// </summary>
        Player player_;
        
        public Hallway(int hallwayID)
        {
            hallwayID_ = hallwayID;
        }
        public Hallway()
        {
        }

        void MovePlayerToHallway(Player player)
        {
            player_ = player;
        }
    }
}
