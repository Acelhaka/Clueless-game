using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessBackend;
using CluelessBackend.Core;
using FluentAssertions;
using Xunit;

namespace CluelessTests.BackEndTests
{
    public class CardTests
    {

        public void CreateDeckOfCardsTest()
        {
            CardDeck deck = new CardDeck();
            deck.CreateDeckOfCards();
          //  Assert.Equal(deck.cardDeck )
        }

    }
}
