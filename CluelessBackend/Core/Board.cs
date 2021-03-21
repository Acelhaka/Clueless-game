using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Board
    {
        Player[] player = new Player[6];
        int[] hallway;

      
        // TODO this looks great! One question is whether we'll need to represent the hallways as nulls/nones here...
        //__________________________Board Setup________________________________
        //                  |                     |                            |
        //    Study         |        Hall         |            Lounge          |
        //__________________|_____________________|____________________________|                                    |                            | 
        //                  |                     |                            |
        //    Library       |   Billiard Room     |         Dining Room        |
        //__________________|_____________________|____________________________|                                    
        //                  |                     |                            |
        //  Conservatory    |      BallRoom       |            Kitchen         |
        //__________________|_____________________|____________________________|                                    


        Room[,] rooms = new Room[3, 3]
        {
        {new Room(1, "Study", "Kitchen"), new Room(2, "Hall"), new Room(3,"Lounge", "Conservatory")},
        {new Room(4, "Library"), new Room(5, "Billiard Room"), new Room(6, "Dining Room")},
        {new Room(7, "Conservatory", "Lounge"), new Room(8, "BallRoom"), new Room(9, "Kitchen", "Study")}
        };

    }
}
