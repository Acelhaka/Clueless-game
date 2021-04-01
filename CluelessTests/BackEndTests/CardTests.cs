using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessBackend;
using CluelessBackend.Core;
using FluentAssertions;
using Xunit;
using System.IO;
using CluelessNetwork;
using CluelessNetwork.NetworkSerialization;



namespace CluelessTests.BackEndTests
{
    public class CardTests
    {

        [Fact]
        public void TestDeckSize()
        {
            CardDeck deck = new CardDeck();

            deck.CreateDeckOfCards();
            int deckSize = deck.GetDeckSize();
            // there should be a total of 21 cards in the deck (9 rooms + 6 weapons + 6 suspects)
            Assert.True(deckSize.Equals(21), "The deck size should be 21 but is not");
        }

        [Fact]
        public void TestGenerationOfCaseFile()
        {
            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();
            // this test will ensure the case file generated pulls one suspect, one weapon and one room only
            Card[] caseFile = deck.SelectCardsForEnvelope();

            bool weaponFound = false;
            bool suspectFound = false;
            bool roomFound = false;

            // first test to make sure the case file has only three cards in it
            Assert.True(caseFile.Length.Equals(3));

            for (int i = 0; i < caseFile.Length; i++) {
                Card c = caseFile[i];
                if (c.Card_Type.Equals(Card.CARD_TYPE.WEAPON)) {
                    weaponFound = true;
                } else if (c.Card_Type.Equals(Card.CARD_TYPE.ROOM))
                {
                    roomFound = true;
                } else if (c.Card_Type.Equals(Card.CARD_TYPE.SUSPECT))
                {
                    suspectFound = true;
                }
            }

            // test that each card type flag is now true after going through the case file
            Assert.True(weaponFound.Equals(true), "The Weapon Type card should be found but is not");
            Assert.True(suspectFound.Equals(true), "The Suspect Type card should be found but is not");
            Assert.True(roomFound.Equals(true), "The Room Type card should be found but is not");

            // now test that the cards from the case file are not in the regular deck that will be dealt to the players
            List<Card> remainingCards = deck.getCardDeck();
            for (int i = 0; i < remainingCards.Count; i++)
            {
                for (int j = 0; j < caseFile.Length; j++)
                {
                    Card card = caseFile[j];
                    // the card from the case file should never be contained in the remaining set of cards in the deck
                    Assert.True(remainingCards.Contains(card).Equals(false), "The case file card was found in the remaining deck, and should not be there");                    
                }
            }
        }

        [Fact]
        public void TestAccusationsOfCaseFile()
        {
            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();
            
            // this test will ensure the case file generated pulls one suspect, one weapon and one room only
            ScenarioFile scenarioFile = new ScenarioFile();
            Card[] caseFile = deck.SelectCardsForEnvelope();
            scenarioFile.SetEnvelopeCards(caseFile);

            bool result = scenarioFile.CheckScenarioFile(caseFile[0], caseFile[1], caseFile[2]);
            Assert.True(result.Equals(true));
            
            List<Card> remainingCards = deck.getCardDeck();
            result = scenarioFile.CheckScenarioFile(remainingCards.ElementAt(0), remainingCards.ElementAt(1), remainingCards.ElementAt(2));
            Assert.True(result.Equals(false));


        }

    }
}
