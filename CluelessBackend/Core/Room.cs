using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Room
    {
        int roomID_; 
        string roomName_;
        Room secretPassage_ = new Room();

        public Room()
        {
        }

        public Room(int roomID, string roomName)
        {
            roomID_ = roomID;
            roomName_ = roomName;
        }

        public Room(int roomID, string roomName, Room secretPassage)
        {
            roomID_ = roomID;
            roomName_ = roomName;
            secretPassage_ = secretPassage;
        }
    }
}
