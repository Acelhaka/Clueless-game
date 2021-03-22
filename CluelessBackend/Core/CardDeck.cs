using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class CardDeck : Card
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
            // Add weapon cards in the deck
            foreach (WEAPON_CARDS weaponIndex in Enum.GetValues(typeof(WEAPON_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.WEAPON, Weapon_Cards = weaponIndex };
                cardIndex++;
            }

            // Add suspect cards in the deck
            foreach (SUSPECT_CARDS suspectIndex in Enum.GetValues(typeof(SUSPECT_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.SUSPECT, Suspect_Cards = suspectIndex };
                cardIndex++;
            }

            // Add room cards in the deck
            foreach (ROOM_CARDS roomIndex in Enum.GetValues(typeof(ROOM_CARDS)))
            {
                deckOfCards_[cardIndex] = new Card { Card_Type = CARD_TYPE.ROOM, Room_Cards = roomIndex };
                cardIndex++;
            }
        }
        public void ShuffleCards()
        {
            Random randomGenerator = new Random();
            Card temp;

            for (int shuffleTimes = 0; shuffleTimes < 200; shuffleTimes++)
            {
                for (int cardCount = 0; cardCount < CARD_NUMBER; cardCount++)
                {
                    //swap the cards
                    int secondCardIndex = randomGenerator.Next(20);
                    temp = deckOfCards_[cardCount];
                    deckOfCards_[cardCount] = deckOfCards_[secondCardIndex];
                    deckOfCards_[secondCardIndex] = temp;
                }
            }

            PrintDeckOfCards();
            }
        public void PrintDeckOfCards()
        {
            for (int i = 0; i < CARD_NUMBER; i++)
            {
                if (deckOfCards_[i].Card_Type == CARD_TYPE.WEAPON)
                {
                    Console.WriteLine(deckOfCards_[i].Weapon_Cards);
                }
                else if (deckOfCards_[i].Card_Type == CARD_TYPE.SUSPECT)
                {
                    Console.WriteLine(deckOfCards_[i].Suspect_Cards);
                }
                else
                {
                    Console.WriteLine(deckOfCards_[i].Room_Cards);
                }
            }
        }
        public CardDeck cardDeck { get; set; }
    }
}
