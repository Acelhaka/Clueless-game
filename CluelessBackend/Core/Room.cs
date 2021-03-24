using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Room
    {
        public enum ROOM
        {
            STUDY,
            HALL,
            LOUNGE,
            LIBRARY,
            BILLIARD_ROOM,
            DINNING_ROOM,
            CONSERVATORY,
            BALLROOM,
            KITCHEN
        }

        int roomID_; 
        string roomName_;
        ROOM roomEnum_;
        bool hasSecretPassage_;
        ROOM secretPassage_;
        bool hallway_ = true;

        Player playerInRoom_;

        public Room(int roomID, string roomName)
        {
            roomID_ = roomID;
            roomName_ = roomName;
        }

        public Room(int roomID, string roomName, bool hasSecretPassage)
        {
            roomID_ = roomID;
            roomName_ = roomName;
            hasSecretPassage_ = hasSecretPassage;
        }

        public Room(ROOM roomType, bool hasSecretPassage)
        {
            roomEnum_ = roomType;
            hasSecretPassage_ = hasSecretPassage;

            if (hasSecretPassage)
            {
                SetSecretPassage(roomType);
            }

            hallway_ = false;
        }

        public Room()
        {

        }

        // TODO:: Check what is the secret passage of each room
        public void SetSecretPassage(ROOM roomType)
        {
            if(roomType == ROOM.STUDY)
            {
                secretPassage_ = ROOM.DINNING_ROOM;
            }
            else if(roomType == ROOM.LOUNGE)
            {
                secretPassage_ = ROOM.LIBRARY;
            }
            else if(roomType == ROOM.CONSERVATORY)
            {
                secretPassage_ = ROOM.BILLIARD_ROOM;
            }
            else if(roomType == ROOM.KITCHEN)
            {
                secretPassage_ = ROOM.DINNING_ROOM;
            }
        }
        public ROOM RoomEnum { get; set; }


        public Player GetPlayerInRoom()
        {
           return playerInRoom_;
        }
        public void SetPlayerInRoom(Player player)
        {
            playerInRoom_ = player;
        }

        public override string ToString()
        {
            return "+ " + roomEnum_ + " Has secret passage = " + hasSecretPassage_ + " ";
        }

        public void PrintRoom(Room room)
        {
            Console.Write(roomEnum_ + " Secret passage = " + hasSecretPassage_ + " ");
        }

        public bool Gethallway()
        {
            return hallway_;
        }
    }
}
