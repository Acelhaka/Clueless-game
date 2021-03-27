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

        public Player CurrentTurn()
        {
            int attempts = 0;
            while (attempts < players_.Count())
            {
                if (index_ == players_.Count())
                {
                    Console.WriteLine("calling reset to go back to any players that might have been missed");
                    Reset();
                }

                Console.WriteLine("attempts = " + attempts);
                // if the player is not active, skip their turn
                if (!players_.ElementAt(index_).IsActive())
                {
                    Console.WriteLine("player at index " + index_ + " is not active, move to next player..");
                    NextTurn();
                }
                else
                {
                    Console.WriteLine("player at index " + index_ + " was found to be active, their turn!");
                    return players_.ElementAt(index_);
                }

                
                attempts++;
                Console.WriteLine("attempts was updated now is at " + attempts);
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
