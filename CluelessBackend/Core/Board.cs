using System;
using System.Collections.Generic;

namespace CluelessBackend.Core
{
    public class Board
    {
        const int MAX_NUM_PLAYERS = 6;

        List<Player> players_;

        Room[,] rooms_;

        //_______________________________**Board Setup**______________________________
        //               |             |                |            |               |
        //    Study      |  Hallway-1  |      Hall      |  Hallway-2 |   Lounge      |
        //_______________|_____________|________________|____________|_______________|
        //               |             |                |            |               |   
        //    Hallway-3  |             |   Hallway-4    |            |   Hallway-5   |
        //_______________|_____________|________________|____________|_______________|
        //               |             |                |            |               |   
        //     Library   | Hallway-6   | Billiard Room  |  Hallway-7 |  Dining Room  |
        //_______________|_____________|________________|____________|_______________|
        //               |             |                |            |               |   
        //    Hallway-8  |             |   Hallway-9    |            |   Hallway-10  |
        //_______________|_____________|________________|____________|_______________|
        //               |             |                |            |               |   
        //  Conservatory | Hallway-11  |   BallRoom     | Hallway-12 |   Kitchen     |
        //_______________|_____________|________________|____________|_______________|                                  

        public Board()
        {
            rooms_ = new Room[5, 5]
           {
            {new Room(Room.ROOM.STUDY, true), new Hallway(1), new Room(Room.ROOM.HALL, false),
                    new Hallway(2), new Room(Room.ROOM.LOUNGE, true)},
            {new Hallway(3), new Hallway(), new Hallway(4), new Hallway(), new Hallway(5)},
            {new Room(Room.ROOM.LIBRARY, false), new Hallway(6), new Room(Room.ROOM.BILLIARD_ROOM, false),
                    new Hallway(7), new Room(Room.ROOM.DINNING_ROOM, false)},
             {new Hallway(8), new Hallway(), new Hallway(9), new Hallway(), new Hallway(10)},
            {new Room(Room.ROOM.CONSERVATORY, true), new Hallway(11),new Room(Room.ROOM.BALLROOM, false),
                    new Hallway(12), new Room(Room.ROOM.KITCHEN, true) }
            };
        }

        public Room GetRoomByIndex(int rowIndex, int columnIndex)
        {
            return rooms_[rowIndex, columnIndex];
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(!rooms_[i,j].Gethallway())
                    {
                        rooms_[i, j].PrintRoom(rooms_[i, j]);
                    }
                    else
                    {
                        Console.Write(" Hallway ");
                    }
                    
                }
                Console.WriteLine();
            }

        }

        public void SetPlayers(List<Player> players)
        {
            players_ = players;
        }
    }
}
