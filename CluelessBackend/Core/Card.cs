using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Card
    {
        /// <summary>
        /// Card type enum, stores all the card types that the deck is made of
        /// </summary>
        public enum CARD_TYPE
        {
            SUSPECT,
            WEAPON,
            ROOM
        }

        /// <summary>
        /// Weapon cards enum, made of 6 different cards types 
        /// </summary>
        public enum WEAPON_CARDS
        {
            ROPE = 0,
            LEAD_PIPE,
            KNIFE,
            WRENCH,
            CANDLESTICK,
            REVOLVER
        }

        /// <summary>
        /// Suspect cards enum, made of 6 suspect cards
        /// </summary>
        public enum SUSPECT_CARDS
        {
            COLONEL_MUSTARD = 6,
            MISS_SCARLET,
            PROFESSOR_PLUM,
            MR_GREEN,
            MRS_WHITE,
            MRS_PEACOCK
        }

        /// <summary>
        /// Room cards type, made of 9 room cards
        /// </summary>
        public enum ROOM_CARDS
        {
            STUDY = 12,
            HALL,
            LOUNGE,
            LIBRARY,
            BILLIARD_ROOM,
            DINNING_ROOM,
            CONSERVATORY,
            BALLROOM,
            KITCHEN
        }

        // Getter and setter functions for the card enums 
        public CARD_TYPE Card_Type { get; set; }
        public WEAPON_CARDS Weapon_Cards { get; set; }
        public SUSPECT_CARDS Suspect_Cards { get; set; }
        public ROOM_CARDS Room_Cards { get; set; }

    }


}

