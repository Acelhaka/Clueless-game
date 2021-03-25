using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class TurnManager
    {
        public List<Player> players_;
        private int index_ = 0;
        public TurnManager(List<Player> players) 
        {
            players_ = players;
            index_ = 0;
        }

        public Player CurrentTurn()
        {
            while (index_ < players_.Count())
            {
                // if the player is not active, skip their turn
                if (!players_.ElementAt(index_).IsActive())
                {
                    NextTurn();
                }
                else
                {
                    return players_.ElementAt(index_);
                }
            }

            Reset();

            // TODO implement cleaner logic to handle this
            while (index_ < players_.Count())
            {
                // if the player is not active, skip their turn
                if (!players_.ElementAt(index_).IsActive())
                {
                    NextTurn();
                }
                else
                {
                    return players_.ElementAt(index_);
                }
            }

            // null represents there's no more active players left to take a turn
            return null;


        }

        public void NextTurn()
        {
            index_++;
        }

        public void Reset()
        {

            index_ = 0;
        }
    }
}
