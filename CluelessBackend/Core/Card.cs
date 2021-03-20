using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Card
    {
        enum CardType
        {
            Suspect,
            Weapon,
            Room
        }
        
        string[] weaponCards = { "Rope", "Lead Pipe", "Knife", "Wrench", "Candlestick", "Revolver" };
        string[] suspectCards = { "Colonel Mustard", "Miss Scarlet", "Professor Plum", "Mr.Green",
                "Mrs.White", "Mrs.Peacock"};
        string[] roomCards = {"Study", "Hall", "Lounge", "Library", "Billiard Room",
        "Dining Room", "Conservatory", "Ballroom", "Kitchen"};

    }
}
