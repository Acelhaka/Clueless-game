using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class TurnManager
    {
        public List<Player> players_;
        private int index_ = 0;
        public TurnManager(List<Player> players) 
        {
            players_ = players;
            index_ = 0;
        }

        // TODO thoughts on refactoring this method to GetCurrentPlayer() and GetNextPlayer()?
        public Player CurrentTurn()
        {
            int attempts = 0;
            while (attempts < players_.Count())
            {
                if (index_ == players_.Count())
                {
                    Reset();
                }

                Console.WriteLine("attempts = " + attempts);
                // if the player is not active, skip their turn
                if (!players_.ElementAt(index_).IsActive())
                {
                    NextTurn();
                }
                else
                {
                    return players_.ElementAt(index_);
                }
                attempts++;
            }

            // null represents there's no more active players left to take a turn..is there a better way to return this in C#?
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
