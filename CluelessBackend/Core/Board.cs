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


        //Room[,] rooms = new Room[3, 3]
        //{
        //{new Room(1, "Study"), new Room(2, "Hall"), new Room(3,"Lounge")},
        //{new Room(4, "Library"), new Room(5, "Billiard Room"), new Room(6, "Dining Room")},
        //{new Room(7, "Conservatory"), new Room(8, "BallRoom"), new Room(9, "Kitchen")}
        //};


        Room[,] rooms = new Room[3, 3]
        {
        {new Room(Room.ROOM.STUDY, true), new Room(Room.ROOM.HALL, false), new Room(Room.ROOM.LOUNGE, true)},
        {new Room(Room.ROOM.LIBRARY, false), new Room(Room.ROOM.BILLIARD_ROOM, false), new Room(Room.ROOM.DINNING_ROOM, false)},
        {new Room(Room.ROOM.CONSERVATORY, true), new Room(Room.ROOM.BALLROOM, false), new Room(Room.ROOM.KITCHEN, true) } 
        };
    }
}
