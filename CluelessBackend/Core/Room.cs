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

        Weapon weapon_;

        /// <summary>
        /// Players that are currently in the room
        /// </summary>
        List<Player> playersInRoom_ = new List<Player>();

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

        /// <summary>
        /// Sets the secret passage for the room 
        /// </summary>
        /// <param name="roomType"> The room type that has a secret passage </param>
        public void SetSecretPassage(ROOM roomType)
        {
            if(roomType == ROOM.STUDY)
            {
                secretPassage_ = ROOM.KITCHEN;
            }
            else if(roomType == ROOM.LOUNGE)
            {
                secretPassage_ = ROOM.CONSERVATORY;
            }
            else if(roomType == ROOM.CONSERVATORY)
            {
                secretPassage_ = ROOM.LOUNGE;
            }
            else if(roomType == ROOM.KITCHEN)
            {
                secretPassage_ = ROOM.STUDY;
            }
        }

        public bool HasSecretPassage()
        {
            return hasSecretPassage_;
        }
        public ROOM RoomEnum { get; set; }


        public List<Player> GetPlayersInRoom()
        {
           return playersInRoom_;
        }
        public void SetPlayerInRoom(Player player)
        {
            playersInRoom_.Add(player);
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

        public void SetWeaponinRoom(Weapon weapon)
        {
            weapon_ = weapon;
        }

        public Weapon GetWeaponInRoom()
        {
            return weapon_;
        }
    }
}
