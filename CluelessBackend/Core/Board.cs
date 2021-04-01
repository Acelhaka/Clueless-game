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

        public void SetStartingPosition(int numberOfPlayers, List<Player> players)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                // TODO: finish for the rest of the suspects
                if (players[i].GetSuspectType() == Suspect.SUSPECT.MISS_SCARLET)
                {
                    rooms_[0, 3].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(0, 3);
                    Console.WriteLine("MISS_SCARLET - Starting position in cell [0,3], Hallway-2");
                }
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.MR_GREEN)
                {
                    rooms_[4, 1].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(4, 1);
                    Console.WriteLine("MR_GREEN - Starting position in cell [4,1], Hallway-11");
                }
            }
        }

        public void MovePlayerToRoom(Player player, Room.ROOM roomType)
        {
            if (roomType == Room.ROOM.STUDY)
            {
                rooms_[0, 0].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.HALL)
            {
                rooms_[0, 2].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.LOUNGE)
            {
                rooms_[0, 4].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.LIBRARY)
            {
                rooms_[2, 0].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.BILLIARD_ROOM)
            {
                rooms_[2, 2].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.DINNING_ROOM)
            {
                rooms_[2, 4].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.CONSERVATORY)
            {
                rooms_[4, 0].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.BALLROOM)
            {
                rooms_[4, 2].SetPlayerInRoom(player);
            }
            else if (roomType == Room.ROOM.KITCHEN)
            {
                rooms_[4, 4].SetPlayerInRoom(player);
            }
        }

        public void SetPlayers(List<Player> players)
        {
            players_ = players;
        }

        public void PrintBoardConstant()
        {
            Console.WriteLine("|_______________________________**Board Setup**______________________________");
            Console.WriteLine("|               |             |                |            |               |");
            Console.WriteLine("|    Study      |  Hallway-1  |      Hall      |  Hallway-2 |   Lounge      |");
            Console.WriteLine("|_______________|_____________|________________|____________|_______________|");
            Console.WriteLine("|               |             |                |            |               |  ");
            Console.WriteLine("|    Hallway-3  |             |   Hallway-4    |            |   Hallway-5   |");
            Console.WriteLine("|_______________|_____________|________________|____________|_______________|");
            Console.WriteLine("|               |             |                |            |               |");
            Console.WriteLine("|     Library   | Hallway-6   | Billiard Room  |  Hallway-7 |  Dining Room  |");
            Console.WriteLine("|_______________|_____________|________________|____________|_______________|");
            Console.WriteLine("|               |             |                |            |               | ");
            Console.WriteLine("|    Hallway-8  |             |   Hallway-9    |            |   Hallway-10  |");
            Console.WriteLine("|_______________|_____________|________________|____________|_______________|");
            Console.WriteLine("|               |             |                |            |               |  ");
            Console.WriteLine("|  Conservatory | Hallway-11  |   BallRoom     | Hallway-12 |   Kitchen     |");
            Console.WriteLine("|_______________|_____________|________________|____________|_______________| ");                              

        }
    }
}
