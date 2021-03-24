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
using FluentAssertions;
using Xunit;


namespace CluelessTests.BackEndTests
{
    public class CardTests
    {

        [Fact]
        public void CreateDeckOfCardsTest()
        {
            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();
            int deckSize = deck.GetDeckSize();
            // there should be a total of 21 cards in the deck (9 rooms + 6 weapons + 6 suspects)
            Assert.True(deckSize.Equals(21));

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
            Assert.True(weaponFound.Equals(true));
            Assert.True(suspectFound.Equals(true));
            Assert.True(roomFound.Equals(true));

            // now test that the cards from the case file are not in the regular deck that will be dealt to the players
            List<Card> remainingCards = deck.getCardDeck();
            for (int i = 0; i < remainingCards.Count; i++)
            {
                for (int j = 0; j < caseFile.Length; j++)
                {
                    Card card = caseFile[j];
                    // the card from the case file should never be contained in the remaining set of cards in the deck
                    Assert.True(remainingCards.Contains(card).Equals(false));                    
                }
            }


        }

    }
}
