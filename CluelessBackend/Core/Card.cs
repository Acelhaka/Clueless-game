using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Card
    {
        public enum CARD_TYPE
        {
            SUSPECT,
            WEAPON,
            ROOM
        }

        public enum WEAPON_CARDS
        {
            ROPE,
            LEAD_PIPE,
            KNIFE,
            WRENCH,
            CANDLESTICK,
            REVOLVER
        }

        public enum SUSPECT_CARDS
        {
            COLONEL_MUSTARD,
            MISSS_SCARLET,
            PREOFESSOR_PLUM,
            MR_GREEN,
            MRS_WHITE,
            MRS_PEACOCK
        }

        public enum ROOM_CARDS
        {
            STUDY,
            HALL,
            LOUNGE,
            LIBRARY,
            BILLIARD_ROOM,
            DINNING_ROOM,
            CONSERVATORY,
            BALLROOM,
            KITHCEN
        }

        // TODO:: Maybe remove this
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

        //
        // Another implementation for card I was thinking would be :
        // class Card {
        //  int cardID_
        //  string cardName_
        //  enum type 
        //  
        //  public Card (int cardID, string cardName, enum Type) {
        //  ....
        //  
        //  }
        //  
        //  thoughts?
        //


    }


}

