using System;
using System.Collections.Generic;

namespace CluelessBackend.Core
{
    public class Board
    {
        /// <summary>
        /// Indicated by the rules, maximum number of player is 6
        /// </summary>
        const int MAX_NUM_PLAYERS = 6;

        /// <summary>
        /// Players that joined the game
        /// </summary>
        List<Player> players_;

        /// <summary>
        /// 2D array of rooms to create the board, 5x5
        /// </summary>
        protected Room[,] rooms_;

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
            // Creating board with rooms and hallways
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

        /// <summary>
        /// Get a room on the board by row and column index
        /// </summary>
        /// <param name="rowIndex"> Row index that the room is positioned at </param>
        /// <param name="columnIndex"> Column inde that the room is positioned at </param>
        /// <returns></returns>
        public Room GetRoomByIndex(int rowIndex, int columnIndex)
        {
            return rooms_[rowIndex, columnIndex];
        }

        /// <summary>
        /// Setting starting position for each player
        /// </summary>
        /// <param name="players"> Set of players that joined the game and needs to be positioned in the board</param>
        public void SePlayerstStartingPosition(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
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
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.COLONEL_MUSTARD)
                {
                    rooms_[1, 4].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(1, 4);
                    Console.WriteLine("COLONEL_MUSTARD - Starting position in cell [1,4], Hallway-5");
                }
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.MRS_PEACOCK)
                {
                    rooms_[3, 0].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(3, 0);
                    Console.WriteLine("MRS_PEACOCK - Starting position in cell [3,0], Hallway-8");
                }
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.PROFESSOR_PLUM)
                {
                    rooms_[1, 0].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(1, 0);
                    Console.WriteLine("PROFESSOR_PLUM - Starting position in cell [1,0], Hallway-3");
                }
                else if (players[i].GetSuspectType() == Suspect.SUSPECT.MRS_WHITE)
                {
                    rooms_[4, 3].SetPlayerInRoom(players[i]);
                    players[i].SetPlayerStartingPosition(4, 3);
                    Console.WriteLine("MRS_WHITE - Starting position in cell [4,3], Hallway-12");
                }
            }
        }

        /// <summary>
        /// Setting players in the board
        /// </summary>
        /// <param name="players"> Set of the players that joined the game </param>
        public void SetPlayers(List<Player> players)
        {
            players_ = players;
        }

    }
}
