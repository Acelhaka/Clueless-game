using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Hallway : Room   // Child of Room class
    {
        int hallwayID_;

        Player player_;

        public Hallway(int hallwayID)
        {
            hallwayID_ = hallwayID;
        }
        public Hallway()
        {
        }
    }
}
