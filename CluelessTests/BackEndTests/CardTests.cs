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
            List<Card> remainingCards = deck.GetCardDeck();
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
            
            List<Card> remainingCards = deck.GetCardDeck();
            result = scenarioFile.CheckScenarioFile(remainingCards.ElementAt(0), remainingCards.ElementAt(1), remainingCards.ElementAt(2));
            Assert.True(result.Equals(false));


        }

        [Fact]
        public void TestSpreadCardsToPlayer()
        {
            Player p1 = new Player(Suspect.SUSPECT.MISS_SCARLET);
            Player p2 = new Player(Suspect.SUSPECT.COLONEL_MUSTARD);
            Player p3 = new Player(Suspect.SUSPECT.MRS_PEACOCK);
            Player p4 = new Player(Suspect.SUSPECT.MRS_WHITE);
            Player p5 = new Player(Suspect.SUSPECT.MR_GREEN);
            Player p6 = new Player(Suspect.SUSPECT.PROFESSOR_PLUM);

            List<Player> players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            players.Add(p5);
            players.Add(p6);

            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();

            // first generate the case file 
            ScenarioFile scenarioFile = new ScenarioFile();
            Card[] caseFile = deck.SelectCardsForEnvelope();
            scenarioFile.SetEnvelopeCards(caseFile);

            // players need to be delt a hand first, so ensure that their hand to start is 0
            Assert.True(0.Equals(p1.GetPlayersCards().Count), "player's card count was expected to be 0, but it is not");

            // now assign a game manager instance and spread the remaining cards to players
            GameManager gm = new GameManager();
            gm.SpreadCardsToPlayer(players, deck);

            // test to ensure each player gets 3 cards because the case file was generated, that would leave 18 remaining cards between 6 players
            Assert.True(3.Equals(p1.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");
            Assert.True(3.Equals(p2.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");
            Assert.True(3.Equals(p3.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");
            Assert.True(3.Equals(p4.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");
            Assert.True(3.Equals(p5.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");
            Assert.True(3.Equals(p6.GetPlayersCards().Count), "player's card count was expected to be 3, but it is not");

            // now test again, but an odd number of players where one player gets more than the rest.
            p1 = new Player(Suspect.SUSPECT.MISS_SCARLET);
            p2 = new Player(Suspect.SUSPECT.COLONEL_MUSTARD);
            p3 = new Player(Suspect.SUSPECT.MRS_PEACOCK);
            p4 = new Player(Suspect.SUSPECT.MRS_WHITE);

            players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            deck = new CardDeck();
            deck.CreateDeckOfCards();

            // generate the case file again to remove three cards from the deck
            scenarioFile = new ScenarioFile();
            caseFile = deck.SelectCardsForEnvelope();
            scenarioFile.SetEnvelopeCards(caseFile);
            gm = new GameManager();
            gm.SpreadCardsToPlayer(players, deck);
                      

            // test to ensure each player gets 3 cards because the case file was generated, that would leave 18 remaining cards between 4 players
            // so two players would get 5 cards, while the other two players would get 4 cards
            Assert.True(5.Equals(p1.GetPlayersCards().Count), "player's card count was expected to be 5, but it is not");
            Assert.True(5.Equals(p2.GetPlayersCards().Count), "player's card count was expected to be 5, but it is not");
            Assert.True(4.Equals(p3.GetPlayersCards().Count), "player's card count was expected to be 4, but it is not");
            Assert.True(4.Equals(p4.GetPlayersCards().Count), "player's card count was expected to be 4, but it is not");
            
        }

    }
}
