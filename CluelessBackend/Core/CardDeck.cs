using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class CardDeck : Card
    {
        // Number of cards = 6 weapons + 6 suspects + 9 rooms
        const int CARD_NUMBER = 21;

        // array that will store all the cards (weapons, suspects and rooms)
        private Card[] deckOfCards_;

        public CardDeck()
        {
            deckOfCards_ = new Card[CARD_NUMBER];
        }

        public void CreateDeckOfCards()
        {
            int cardIndex = 0;
            foreach (WEAPON_CARDS weaponIndex in Enum.GetValues(typeof(WEAPON_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.WEAPON, Weapon_Cards = weaponIndex };
                cardIndex++;
            }

            foreach (SUSPECT_CARDS suspectIndex in Enum.GetValues(typeof(SUSPECT_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.SUSPECT, Suspect_Cards = suspectIndex };
                cardIndex++;
            }

            foreach (ROOM_CARDS roomIndex in Enum.GetValues(typeof(ROOM_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.ROOM, Room_Cards = roomIndex };
                cardIndex++;
            }
        }
    }
}
