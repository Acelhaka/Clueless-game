﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class CardDeck : Card
    {
        /// <summary>
        /// Number of cards = 6 weapons + 6 suspects + 9 rooms 
        /// </summary>
        const int CARD_NUMBER = 21;

        /// <summary>
        /// list of cards that will store all the cards (weapons, suspects and rooms)
        /// </summary>
        List<Card> deckOfCards_ = new List<Card>(CARD_NUMBER);

        /// <summary>
        /// Size of the deck 
        /// </summary>
        int deckSize_;

        /// <summary>
        /// Default card deck constructor
        /// </summary>
        public CardDeck()
        {
            
        }

        /// <summary>
        /// Function that creates the deck of cards, made of 6 suspects, 9 rooms and 6 weapons
        /// </summary>
        public void CreateDeckOfCards()
        {
            deckSize_ = CARD_NUMBER;

            int cardIndex = 0;
            // Add weapon cards in the deck
            foreach (WEAPON_CARDS weaponIndex in Enum.GetValues(typeof(WEAPON_CARDS)))
            {
                Card weaponCard = new Card { Card_Type = CARD_TYPE.WEAPON, Weapon_Cards = weaponIndex };
                deckOfCards_.Add(weaponCard);
                cardIndex++;
            }

            // Add suspect cards in the deck
            foreach (SUSPECT_CARDS suspectIndex in Enum.GetValues(typeof(SUSPECT_CARDS)))
            {
                deckOfCards_.Add(new Card { Card_Type = CARD_TYPE.SUSPECT, Suspect_Cards = suspectIndex });
                cardIndex++;
            }

            // Add room cards in the deck
            foreach (ROOM_CARDS roomIndex in Enum.GetValues(typeof(ROOM_CARDS)))
            {
                deckOfCards_.Add(new Card { Card_Type = CARD_TYPE.ROOM, Room_Cards = roomIndex });
                cardIndex++;
            }
        }

        /// <summary>
        /// Shuffles the cards in the deck using a random number generator
        /// </summary>
        public void ShuffleCards()
        {
            Random randomGenerator = new Random();
            Card tempCard;

            // TODO we probably could use a shuffle call on the List and remove the need for a double nested loop. Might make our Code complexity score a little lower too...
            for (int shuffleTimes = 0; shuffleTimes < 200; shuffleTimes++)
            {
                for (int cardCount = 0; cardCount < deckSize_; cardCount++)
                {
                    //swap the cards
                    int secondCardIndex = randomGenerator.Next(deckSize_);
                    tempCard = deckOfCards_[cardCount];
                    deckOfCards_[cardCount] = deckOfCards_[secondCardIndex];
                    deckOfCards_[secondCardIndex] = tempCard;
                }
            }
        }

        /// <summary>
        /// Prints the deck of cards, helpful for debugging 
        /// </summary>
        public void PrintDeckOfCards()
        {
            for (int i = 0; i < deckSize_; i++)
            {
                if (deckOfCards_[i].Card_Type == CARD_TYPE.WEAPON)
                {
                    Console.WriteLine(i + "-" + deckOfCards_[i].Weapon_Cards);
                }
                else if (deckOfCards_[i].Card_Type == CARD_TYPE.SUSPECT)
                {
                    Console.WriteLine(i + "-" + deckOfCards_[i].Suspect_Cards);
                }
                else
                {
                    Console.WriteLine(i + "-" + deckOfCards_[i].Room_Cards);
                }
            }
        }

        /// <summary>
        /// Selects randomly 3 cards for the envelope, 1 cards for each card-type
        /// Updates the deck of cards by removing the selected cards
        /// </summary>
        /// <returns> An array of cards, made of 3 cards </returns>
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

            // remove the selected weapon card from the deck
            deckOfCards_.Remove(weaponCard);
            deckSize_ -= 1;

            // remove the selected suspect card from the deck
            deckOfCards_.Remove(suspectCard);
            deckSize_ -= 1;

            // remove the selected room card from the deck
            deckOfCards_.Remove(roomCard);
            deckSize_ -= 1;

            return envelopeCards;
        }

        public int GetDeckSize()
        {
            return deckSize_;
        }

        public Card GetCardFromDeck(int index)
        {
            return deckOfCards_[index];
        }

        public List<Card> GetCardDeck()
        {
            return deckOfCards_;
        }
    }
}
