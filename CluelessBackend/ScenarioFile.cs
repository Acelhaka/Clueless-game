using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessBackend.Core;

namespace CluelessBackend.Core
{
    class ScenarioFile : CardDeck
    {
        Card[] envelopeCards_ = new Card[3];

        public void PlaceCardsInEnvelope(CardDeck deck)
        {

            envelopeCards_ = SelectCardsForEnvelope();
        }

        // Return true if scenario file matches the envelope
        public bool CheckScenarioFile(Card weapon, Card suspect, Card room)
        {
           if((envelopeCards_[0].Weapon_Cards.GetType() == weapon.GetType())
             && (envelopeCards_[1].Suspect_Cards.GetType() == suspect.GetType())
             && (envelopeCards_[2].Room_Cards.GetType() == room.GetType()))
            {
                return true;
            }

            return false;
        }
    }
}
