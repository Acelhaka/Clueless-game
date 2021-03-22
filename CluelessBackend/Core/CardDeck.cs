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

        int deckSize_;

        public CardDeck()
        {
            deckOfCards_ = new Card[CARD_NUMBER];
        }

        public void CreateDeckOfCards()
        {
            deckSize_ = CARD_NUMBER;

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
            Card tempCard;

            for (int shuffleTimes = 0; shuffleTimes < 200; shuffleTimes++)
            {
                for (int cardCount = 0; cardCount < CARD_NUMBER - 3; cardCount++)
                {
                    //swap the cards
                    int secondCardIndex = randomGenerator.Next(20);
                    tempCard = deckOfCards_[cardCount];
                    deckOfCards_[cardCount] = deckOfCards_[secondCardIndex];
                    deckOfCards_[secondCardIndex] = tempCard;
                }
            }

            Console.WriteLine("AFTER SHUFFLING CARDS");

            PrintDeckOfCards();
        }
        public void PrintDeckOfCards()
        {
            for (int i = 0; i < deckSize_; i++)
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

        public Card[] SelectCardsForEnvelope()
        {
            Random randomGenerator = new Random();

            Card[] envelopeCards = new Card[3];
            Card weaponCard;
            Card roomCard;
            Card suspectCard;

            int weaponRandomIndex = randomGenerator.Next(6);
            weaponCard = deckOfCards_[weaponRandomIndex];
            envelopeCards[0] = weaponCard;

            int suspectRandomIndex = randomGenerator.Next(6, 11);
            suspectCard = deckOfCards_[suspectRandomIndex];
            envelopeCards[1] = suspectCard;

            int roomRandomIndex = randomGenerator.Next(12, 20);
            roomCard = deckOfCards_[roomRandomIndex];
            envelopeCards[2] = roomCard;

            Console.WriteLine("BEFORE REMOVING CARDS" );
            PrintDeckOfCards();

            // remove the selected weapon card from the deck
            RemoveSelectedCardFromDeck(weaponRandomIndex);

            // remove the selected suspect card from the deck
            RemoveSelectedCardFromDeck(suspectRandomIndex);

            // remove the selected room card from the deck
            RemoveSelectedCardFromDeck(roomRandomIndex);

            Console.WriteLine("\n\nAFTER REMOVING CARDS");
            PrintDeckOfCards();

            return envelopeCards;
        }

        public void RemoveSelectedCardFromDeck(int index)
        {
            for (int i = index; i < deckSize_ - 1; i++)
            {
                deckOfCards_[i] = deckOfCards_[i + 1];
            }
            deckSize_ -= 1;

        }
        public int GetDeckSize()
        {
            return deckSize_;
        }

        public Card GetCardFromDeck(int index)
        {
            return deckOfCards_[index];
        }
    }
}
