using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessBackend.Core;

namespace CluelessBackend.Core
{
    public class ScenarioFile : CardDeck
    {
        const int NUM_CARDS_IN_ENVELOPE = 3;

        Card[] envelopeCards_;

        public ScenarioFile()
        {
            envelopeCards_ = new Card[NUM_CARDS_IN_ENVELOPE];
        }

        public void PlaceCardsInEnvelope(CardDeck deck)
        {
            envelopeCards_ = SelectCardsForEnvelope();
        }

        // Return true if scenario file matches the envelope
        public bool CheckScenarioFile(Card weapon, Card suspect, Card room)
        {
            if ((envelopeCards_[0].Weapon_Cards == weapon.Weapon_Cards)
              && (envelopeCards_[1].Suspect_Cards == suspect.Suspect_Cards)
              && (envelopeCards_[2].Room_Cards == room.Room_Cards))
            {
                return true;
            }

            return false;
        }

        public void PrintEnvelopeCards()
        {
            Console.WriteLine("\nCards selected for the envelope:");

            for (int i = 0; i < NUM_CARDS_IN_ENVELOPE; i++)
            {
                if (envelopeCards_[i].Card_Type == CARD_TYPE.WEAPON)
                {
                    Console.WriteLine("Weapon Card: " + envelopeCards_[i].Weapon_Cards);
                }
                else if (envelopeCards_[i].Card_Type == CARD_TYPE.SUSPECT)
                {
                    Console.WriteLine("Suspect Card: " + envelopeCards_[i].Suspect_Cards);
                }
                else
                {
                    Console.WriteLine("Room Card: " + envelopeCards_[i].Room_Cards);
                }
            }
        }

        public void SetEnvelopeCards(Card[] envelopeCards)
        {
            envelopeCards_ = envelopeCards;
        }
    }
}
