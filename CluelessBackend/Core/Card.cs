using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Card
    {
        public enum CARD_TYPE
        {
            SUSPECT,
            WEAPON,
            ROOM
        }

        public enum WEAPON_CARDS
        {
            ROPE = 0,
            LEAD_PIPE,
            KNIFE,
            WRENCH,
            CANDLESTICK,
            REVOLVER
        }

        public enum SUSPECT_CARDS
        {
            COLONEL_MUSTARD = 6,
            MISSS_SCARLET,
            PREOFESSOR_PLUM,
            MR_GREEN,
            MRS_WHITE,
            MRS_PEACOCK
        }

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

        // TODO:: remove these
        string[] weaponCards = { "Rope", "Lead Pipe", "Knife", "Wrench", "Candlestick", "Revolver" };
        string[] suspectCards = { "Colonel Mustard", "Miss Scarlet", "Professor Plum", "Mr. Green",
                "Mrs. White", "Mrs. Peacock"};
        string[] roomCards = {"Study", "Hall", "Lounge", "Library", "Billiard Room",
        "Dining Room", "Conservatory", "Ballroom", "Kitchen"};

        // Getter and setter functions for the card enums 
        public CARD_TYPE Card_Type { get; set; }
        public WEAPON_CARDS Weapon_Cards { get; set; }
        public SUSPECT_CARDS Suspect_Cards { get; set; }
        public ROOM_CARDS Room_Cards { get; set; }

        public override string ToString()
        {
            string cardObject = "";
            if (Card_Type == CARD_TYPE.WEAPON)
            {
                cardObject = "Card: " + Card_Type + ": " + roomCards;
            }
            if (Card_Type == CARD_TYPE.WEAPON)
            {
                cardObject = "Card: " + Card_Type + ": " + weaponCards;
            }
            if (Card_Type == CARD_TYPE.SUSPECT)
            {
                cardObject = "Card: " + Card_Type + ": " + suspectCards;
            }

            return cardObject;
        }

    }


}

